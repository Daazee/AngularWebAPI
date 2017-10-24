using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using AngularWebAPI.DataAccess.Models;
using AngularWebAPI.DataAccess.DataAccess;

namespace AngularWebAPI.WEBAPI.Services
{
    internal class DefaultAppUserService : IAppUserService
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;


        internal DefaultAppUserService()
        {
            var identityContext = new AngularWebAPIDataContext();

            var userStore = new UserStore<ApplicationUser>(identityContext);
            _userManager = new UserManager<ApplicationUser>(userStore);


            var roleStore = new RoleStore<IdentityRole>(identityContext);
            _roleManager = new RoleManager<IdentityRole>(roleStore);
        }


        public UserManager<ApplicationUser> GetUserManager()
        {
            return _userManager;
        }

        public RoleManager<AppRole> GetRoleManager()
        {
            return _roleManager;
        }
        public MyResponseStatus CreateUser(CreateAdminUserModel model)
        {
            _logger.Debug("About to create new admin user " + model + " as a " + model.RoleName);

            try
            {
                if (_userManager.FindByNameAsync(model.Email.Trim()).Result != null)
                {
                    _logger.Info("Admin User with username " + model.Email + " already exists in the database.");
                    return
                        new FailedResponseStatus("Admin User with the username " + model.Email +
                                                 " already exists.  Please enter another email address");
                }

                var user = new ApplicationUser()
                {
                    FirstName = model.FirstName.Trim(),
                    LastName = model.LastName.Trim(),
                    PhoneNumber = model.PhoneNumber.Trim(),
                    UserName = model.Email.Trim(),
                    Title = model.Title,
                    Status = AccountStatus.Active

                };
                return CreateNewUser(user, model.Password, model.RoleName);
            }
            catch (Exception ex)
            {
                _logger.Error("There was an issue creating the user", ex);
                return new FailedResponseStatus("An error was encountered trying to create the user.  Please try again");
            }
        }

        public void TryLogin(AdminLoginModel model, OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                ApplicationUser user = _userManager.Find(model.Username, model.Password);
                if (user == null)
                {

                    var userWithPhoneNumber = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == model.Username);
                    if (userWithPhoneNumber != null)
                        user = _userManager.Find(userWithPhoneNumber.UserName, model.Password);
                }
                if (user == null)
                {
                    _logger.Debug("User with Username " + model.Username +
                                 " could not be logged in possibly due to wrong credentials");
                    context.SetError("invalid_grant", "Username or Password incorrect. Please try again.");
                    return;
                }


                using (var tokenService = ServicesManager.GetTokenService())
                {
                    var tempPasswordToken = tokenService.GetToken(user.Id);
                    if (tempPasswordToken != null && tokenService.IsExpired(tempPasswordToken.Id))
                    {
                        _auditLogger.SaveAdminLog(new AuditLog(user.Id, user.FirstName + " " + user.LastName, user.Email,
                            "LOGIN", "Attempt to Login with an expired token failed"));
                        context.SetError("Invalid_grant", "Your temporary Password has expired. Please reset your account to Reactivate");
                        return;
                    }
                }


                if (user.Status == AccountStatus.Disabled)
                {
                    _logger.Debug("User " + user +
                        " could not be logged in because this user has been disabled");
                    _auditLogger.SaveAdminLog(new AuditLog(user.Id, user.FirstName + " " + user.LastName, user.Email,
                            "LOGIN", "Account is disabled : Login attempt Failed"));

                    context.SetError("invalid_grant", "Your account has been disabled.  Please contact the Super Administrator.");
                    return;
                }

                if (_userManager.IsLockedOut(user.Id))
                {
                    _logger.Debug("User " + user +
                       " could not be logged in because this account has been locked out");
                    context.SetError("invalid_grant", "Your account has been locked as you have reached the maximum failed login attempts.  Please contact the Super Administrator.");
                    return;
                }

                var claimsidentity = new ClaimsIdentity(context.Options.AuthenticationType);
                //AddClaims(user, claimsidentity);
                claimsidentity.AddClaim(new Claim("sub", context.UserName));
                claimsidentity.AddClaim(new Claim(ClaimTypes.Email, user.UserName));
                claimsidentity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                claimsidentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));


                _logger.Debug("User " + user + " successfully logged in");
                context.Validated(claimsidentity);
                _userManager.ResetAccessFailedCount(user.Id);


                var roles = new StringBuilder();
                foreach (string role in _userManager.GetRoles(user.Id))
                {
                    claimsidentity.AddClaim(new Claim(ClaimTypes.Role, role));
                    roles.Append(role + ';');
                }

                roles = roles.ToString().EndsWith(";") ? roles.Remove(roles.Length - 1, 1) : roles;
                var authProperties = new Dictionary<string, string>
                {
                    {"IsFirstTimeLogin", user.IsFirstTimeLogin.ToString()},
                    {"Roles", roles.ToString()}
                };

                // var rolesn = String.Concat(accountService.GetUserRoles(userInfo));


                var props = new AuthenticationProperties(authProperties);
                var authTicket = new AuthenticationTicket(claimsidentity, props);

                _auditLogger.SaveAdminLog(new AuditLog(user.Id, user.FirstName + " " + user.LastName, user.Email,
                            "LOGIN", "Logged In with valid account details"));
                context.Validated(authTicket);
                return;

            }
            catch (Exception ex)
            {
                _logger.Error("An exception occurred trying to login user with username " + model.Username, ex);
                context.SetError("invalid_grant", "An error occurred trying to log in.");
            }
        }

        public ApplicationUser FindUserByEmail(string email)
        {
            return _userManager.FindByEmail(email);
        }


        public ApplicationUser FindUserById(string userId)
        {
            return _userManager.FindById(userId);
        }

        public ApplicationUser FindUserByPhoneOrEmail(string username)
        {
            return _userManager.Users.FirstOrDefault(u => u.Email == username || u.PhoneNumber == username);
        }

        public MyResponseStatus ResetPassword(string userId)
        {
            var user = _userManager.FindById(userId);
            if (user == null)
                return new FailedResponseStatus("Cannot Find User with");

            var tempPassword = PasswordManager.GeneratePassword(12);
            var removePassword = _userManager.RemovePassword(user.Id);

            if (removePassword == null || !removePassword.Succeeded)
                return new FailedResponseStatus("Reset Pasword Operation Failed");
            var addTempPassword = _userManager.AddPassword(user.Id, tempPassword);
            if (addTempPassword.Succeeded)
            {
                user.IsFirstTimeLogin = true;
                user.Status = AccountStatus.Active;
                _userManager.Update(user);
            }

            var mailMsg = new EmailModel
            {
                Message =
                    string.Format(
                        "Dear {0},<br/><br/>You recently Requested a password Reset for your Account.<br/>Please use the information below to Login your account.<br/>Password :<b> {1}</b>" +
                        "<br/>This is a temporary password that expires after 24 hours. Please remember to change your password once you are succefully Logged In.<br/><br/>" +
                        "You Must reset your password within 24 hours. otherwise you would have to request for another password reset token.",
                        user.FirstName + " " + user.LastName, tempPassword),
                Recipeint = user.Email,
                Subject = "Reset Password: Simple Invest"
            };
            EmailService.SendSimpleMail(mailMsg);
            return new SuccessResponseStatus();
        }
        public IdentityResult ChangePassword(string id, ChangePasswordVm model)
        {
            var result = _userManager.ChangePassword(id, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                var user = _userManager.FindById(id);
                if (user != null)
                {
                    user.IsFirstTimeLogin = false;
                    user.Status = AccountStatus.Active;
                    result = _userManager.Update(user);
                    if (result.Succeeded)
                    {
                        return result;
                    }
                }
            }
            return null;

        }
        public bool ChangeUserRole(string userId, string oldRole, string newRole)
        {
            var user = _userManager.FindById(userId);
            var userRoles = _userManager.GetRoles(user.Id);
            var roleRemoved = RemoveUserFromRole(user.Id, oldRole) == 2;
            if (roleRemoved)
            {
                return AddUserToRole(user.Id, newRole) == 2;
            }
            return false;
        }

        public int AddUserToRole(string userId, string roleName)
        {
            if (!_roleManager.RoleExists(roleName))
                return 0;
            var userRoles = _userManager.GetRoles(userId);
            if (userRoles.Contains(roleName))
                return 1;
            var result = _userManager.AddToRole(userId, roleName);
            return result.Succeeded ? 2 : 3;
        }

        public int RemoveUserFromRole(string userId, string roleName)
        {
            if (_roleManager.RoleExists(roleName))
                return 0;
            var userRoles = _userManager.GetRoles(userId);
            if (userRoles.Contains(roleName))
                return 1;
            var result = _userManager.RemoveFromRole(userId, roleName);
            return result.Succeeded ? 2 : 3;
        }

        public IList<string> GetUserRoles(string userId)
        {
            return _userManager.GetRoles(userId);
        }

        public async Task<UserResult> Create(ApplicationUser user, string role = "", string passWord = "")
        {
            try
            {

                user.IsFirstTimeLogin = true;
                var password = passWord;
                if (string.IsNullOrEmpty(password))
                {
                    password = PasswordManager.GeneratePassword(8);
                }
                var result = _userManager.Create(user, password);
                if (string.IsNullOrEmpty(role) || !result.Succeeded)
                    return new UserResult
                    {
                        Errors = result.Errors.ToList(),
                        Succeeded = result.Succeeded
                    };
                AddUserToRole(user.Id, role);
                await EmailService.SendEmailToUser(user, password);

                return new UserResult
                {
                    Errors = result.Errors.ToList(),
                    Succeeded = result.Succeeded
                };

            }
            catch (DbEntityValidationException e)
            {
                var errors = new List<string>();
                var name = string.Empty;
                var msg = string.Empty;

                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        name = ve.PropertyName;
                        msg = ve.ErrorMessage;
                    }
                }

                return new UserResult
                {
                    ProprtyName = name,
                    Message = msg
                };
            }
        }

        public IQueryable<ApplicationUser> GetUsers()
        {
            return _userManager.Users;
        }

        public IEnumerable<ApplicationUser> GetUsers(UserType userType)
        {
            return _userManager.Users.Where(u => u.UserType == userType);
        }

        public IEnumerable<ApplicationUser> GetUsers(string type)
        {
            if (string.IsNullOrEmpty(type))
                return new List<ApplicationUser>();
            var userList = _userManager.Users;
            switch (type)
            {
                case "Broker":
                    return userList.Where(u => u.UserType == UserType.Broker);
                case "Admin":
                    return userList.Where(u => u.UserType == UserType.Admin);
                case "Super Admin":
                    return userList.Where(u => u.UserType == UserType.SuperAdmin);
                default:
                    return new List<ApplicationUser>();
            }
        }

        public IEnumerable<ApplicationUser> FilterUsers(Expression<Func<ApplicationUser, bool>> fiterExpression)
        {
            var userList = _userManager.Users;
            return userList.Where(fiterExpression);
        }


        public UserResult Update(ApplicationUser updatedUser)
        {

            using (var ctx = _userManager)
            {
                var user = ctx.FindById(updatedUser.Id);
                //Mapper.Map(updatedUser, user);
                user.Status = updatedUser.Status;
                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.PhoneNumber = updatedUser.PhoneNumber;
                if (updatedUser.DateOfBirth.HasValue)
                    user.DateOfBirth = updatedUser.DateOfBirth;
                if (updatedUser.State.HasValue)
                    user.State = updatedUser.State;
                if (updatedUser.Gender.HasValue)
                    user.Gender = updatedUser.Gender;
                var result = ctx.Update(user);

                return new UserResult
                {
                    Succeeded = result.Succeeded,
                    Errors = result.Errors.ToList()
                };
            }

        }
        private void AddClaims(ApplicationUser user, ClaimsIdentity identity)
        {
            var role = _roleManager.Roles.ToList().Single(r => r.Id == user.Roles.First().RoleId);

            /*
             claimsidentity.AddClaim(new Claim("RoleName", role.Name));
            claimsidentity.AddClaim(new Claim("IsAdmin", role.IsAdmin.ToString()));
            claimsidentity.AddClaim(new Claim("Title", user.Title.ToString()));
            claimsidentity.AddClaim(new Claim("FirstName", user.FirstName));
            claimsidentity.AddClaim(new Claim("LastName", user.LastName));
             */

        }

        private MyResponseStatus CreateNewUser(ApplicationUser user, string password, string roleName)
        {
            IdentityResult result = _userManager.Create(user, password);
            if (result.Succeeded)
            {
                _logger.Info("User " + user + " was successfully created");
                result = _userManager.AddToRole(user.Id, roleName);
                if (result.Succeeded)
                {
                    _logger.Info("User " + user + " was successfully added to the " + roleName + " role");
                    _userManager.SetLockoutEnabled(user.Id, true);
                    return new SuccessResponseStatus();
                }
                _logger.Warn("User " + user + " was successfully created but could not be added to the " +
                             roleName + " role because " +
                             ErrorHelpers.GetIdentityResultErrors(result));

                if (roleName.ToLower() == "customer")
                {
                    return
                        new FailedResponseStatus(
                            "There was an error creating your profile.  Please contact our support desk on " +
                            ConfigurationManager.AppSettings["SupportNumber"]);
                }
                return
                    new FailedResponseStatus(
                        "The user was successfully added to the database but could not be assigned the " +
                        roleName + " role.  Please edit the user to reattempt assigning the role.");
            }
            _logger.Info("User " + user + " could not be created because " +
                         ErrorHelpers.GetIdentityResultErrors(result));
            if (roleName.ToLower() == "customer")
            {
                return
                    new FailedResponseStatus(
                        "There was an error creating your profile.  Please try again in five minutes.");
            }
            return new FailedResponseStatus("The user could not be created.  Please try again.");
        }


    }
}