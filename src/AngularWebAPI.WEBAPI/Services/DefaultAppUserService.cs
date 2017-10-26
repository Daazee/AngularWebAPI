using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using AngularWebAPI.DataAccess.Models;
using AngularWebAPI.DataAccess.DataAccess;
using AngularWebAPI.WEBAPI.Models.ViewModels;
using AngularWebAPI.DataAccess;

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

        public bool ResetPassword(string userId)
        {
            var user = _userManager.FindById(userId);
            if (user == null)
                return false;

            var tempPassword = Guid.NewGuid().ToString("N").Substring(0,11);
            var removePassword = _userManager.RemovePassword(user.Id);

            if (removePassword == null || !removePassword.Succeeded)
                return false;
            var addTempPasswordResult = _userManager.AddPassword(user.Id, tempPassword);
            if (!addTempPasswordResult.Succeeded)
                return false;

            /*
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
            EmailService.SendSimpleMail(mailMsg); */
            return true;
        }
        public IdentityResult ChangePassword(string id, ChangePasswordVm model)
        {
            var result = _userManager.ChangePassword(id, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return result;
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

        public ApplicationUser Create(ApplicationUser user, string role = "", string passWord = "")
        {
            try
            {

                //user.IsFirstTimeLogin = true;
                var password = passWord;
                if (string.IsNullOrEmpty(password))
                {
                    password = Guid.NewGuid().ToString("N").Substring(0, 8);
                }
                var result = _userManager.Create(user, password);
                if (string.IsNullOrEmpty(role) || !result.Succeeded)
                    return null;
                AddUserToRole(user.Id, role);
                //await EmailService.SendEmailToUser(user, password);
                return user;

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

                return null;
            }
        }

        public IQueryable<ApplicationUser> GetUsers()
        {
            return _userManager.Users;
        }

        public IEnumerable<ApplicationUser> GetUsers(UserType userType)
        {
            return _userManager.Users.Where(u => u.AccountType == userType);
        }

        public IEnumerable<ApplicationUser> GetUsers(string type)
        {
            if (string.IsNullOrEmpty(type))
                return new List<ApplicationUser>();
            var userList = _userManager.Users;
            switch (type)
            {
                case "User":
                    return userList.Where(u => u.AccountType == UserType.User);
                case "Admin":
                    return userList.Where(u => u.AccountType == UserType.Admin);
                case "Super Admin":
                    return userList.Where(u => u.AccountType == UserType.SuperAdmin);
                default:
                    return new List<ApplicationUser>();
            }
        }

        public IEnumerable<ApplicationUser> FilterUsers(Expression<Func<ApplicationUser, bool>> fiterExpression)
        {
            var userList = _userManager.Users;
            return userList.Where(fiterExpression);
        }

        /*
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
        */

    }
}