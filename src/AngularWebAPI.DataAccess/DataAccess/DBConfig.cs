using System.Configuration;
using System.Text;
using Cousant.NaijaInvest.API.Data;
using Cousant.NaijaInvest.API.Enum;
using Cousant.NaijaInvest.API.Models;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Cousant.NaijaInvest.API
{
    public class DBConfig
    {
        public static void Seed(SimpleInvestContext identityContext)
        {
            var superAdminEmail = ConfigurationManager.AppSettings["SuperAdminEmail"];
            var superAdminPhoneNum = ConfigurationManager.AppSettings["SuperAdminNumber"];
            //var identityContext = new SimpleInvestContext();

            var userStore = new UserStore<ApplicationUser>(identityContext);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<AppRole>(identityContext);
            var roleManager = new RoleManager<AppRole>(roleStore);

            var appRoles = new[]
            {"Super Administrator", "Super Broker", "Broker", "Administrator"};
            foreach (var role in appRoles)
            {
                CreateRole(roleManager,role);
            }
            CreateRole(roleManager, "Customer", false);
            CreateSuperAdminUser(userManager, superAdminEmail, superAdminPhoneNum);
        }

        private static void CreateSuperAdminUser(UserManager<ApplicationUser> userManager, string username, string phoneNumber)
        {
            
            if (userManager.FindByIdAsync(username).Result == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Super",
                    LastName = "Administrator",
                    PhoneNumber = phoneNumber,
                    UserName = username,
                    Title = Title.Mr,
                    IsFirstTimeLogin = true,
                    Status = AccountStatus.Active,
                    Email = username,
                    UserType = UserType.SuperAdmin
                };

                IdentityResult result = userManager.Create(user, "develop001");
                if (result.Succeeded)
                {
                    _logger.Debug("User " + user+ "was added to the database");
                    result = userManager.AddToRole(user.Id, "Super Administrator");
                    if (result.Succeeded)
                    {
                        _logger.Debug("User " + user.FirstName + " " + user.LastName +
                                     " was successfully added as a super Administrator");
                    }
                    _logger.Warn("User " + user.FirstName + " " + user.LastName + " could NOT be added as a super Administrator");
                }
            }
        }

        private static void CreateRole(RoleManager<AppRole> roleManager, string newRole, bool isAdmin = true)
        {
            if (!roleManager.RoleExists(newRole))
            {
                var role = new AppRole(newRole) {IsAdmin = isAdmin};
                roleManager.Create(role);
                _logger.Debug(role+" was added to the database");
            }
        }
    }
}