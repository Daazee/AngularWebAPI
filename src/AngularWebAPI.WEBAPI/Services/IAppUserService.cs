using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNet.Identity;
using AngularWebAPI.DataAccess.DataAccess;
using AngularWebAPI.WEBAPI.Models.ViewModels;
using AngularWebAPI.DataAccess.Models;

namespace AngularWebAPI.WEBAPI.Services
{
    public interface IAppUserService
    {
        ApplicationUser FindUserByEmail(string email);
        ApplicationUser FindUserById(string userId);
        ApplicationUser FindUserByPhoneOrEmail(string username);


        //MyResponseStatus ResetPassword(string userId);
        IdentityResult ChangePassword(string userId, ChangePasswordVm passwordVm);
        bool ChangeUserRole(string newRole, string userId, string oldRole);
        int AddUserToRole(string userId,string roleName);
        int RemoveUserFromRole(string userId,string roleName);

        IList<string> GetUserRoles(string userId);
        //UserResult Update(ApplicationUser user);
        ApplicationUser Create(ApplicationUser user, string role = "",string password="");

        IQueryable<ApplicationUser> GetUsers();
        IEnumerable<ApplicationUser> GetUsers(UserType userType); 
        IEnumerable<ApplicationUser> GetUsers(string type);
        IEnumerable<ApplicationUser> FilterUsers(Expression<Func<ApplicationUser,bool>> fiterExpression );
        UserManager<ApplicationUser> GetUserManager();
        
    }
}
