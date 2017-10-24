namespace AngularWebAPI.DataAccess.Migrations
{
    using DataAccess;
    using Domain.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WEBAPI.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<AngularWebAPIDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AngularWebAPIDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var userStore = new UserStore<ApplicationUser>(AngularWebAPIDataContext.Create());
            var roleStore = new RoleStore<IdentityRole>(AngularWebAPIDataContext.Create());
            var userManager = new ApplicationUserManager(userStore);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var applicationRoles = new string[] { "SuperAdmin", "Admin", "User" };
            CreateRoles(roleManager, applicationRoles);
            ApplicationUser user = userManager.FindByEmailAsync("sbassey@eminentTechnology.com").Result;
            if (user == null)
            {
                user = new ApplicationUser { FirstName = "Admin", LastName = "Super Admin", Email = "sbassey@eminenttechnology.com", PhoneNumber = "07019878453", AccountType = UserType.SuperAdmin };
                user.UserName = user.Email;
                var userCreateResult = userManager.Create(user);
                if (userCreateResult.Succeeded)
                {
                    userManager.AddPassword(user.Id, "develop001");
                    var adminRoleExistOnApplication = roleManager.RoleExists(applicationRoles[0]);
                    if (adminRoleExistOnApplication)
                        userManager.AddToRole(user.Id, applicationRoles[0]);
                }
            }
        }

        private void CreateRoles(RoleManager<IdentityRole> roleManager, string[] applicationRoles)
        {
            foreach (var appRole in applicationRoles)
            {
                if (!roleManager.RoleExists(appRole))
                    roleManager.Create(new IdentityRole(appRole));
            }
        }
    }
}
