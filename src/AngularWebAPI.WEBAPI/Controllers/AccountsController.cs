using AngularWebAPI.WEBAPI.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AngularWebAPI.WEBAPI.Controllers
{
    [RoutePrefix("api/accounts")]
    [Authorize]
    public class AccountsController : ApiController
    {

        private readonly IAppUserService _userAccountService;
        public AccountsController()
        {
            _userAccountService = new DefaultAppUserService();
        }

        [Route("")]
        public IHttpActionResult GetUserAccounts()
        {
            try
            {
                var userAccounts = _userAccountService.GetUsers().ToList();
                return Ok(userAccounts);
            }
            catch(Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        [Route("profile")]
        public IHttpActionResult GetMyProfile()
        {
            try
            {
                var userId = User.Identity.GetUserId();
                if (string.IsNullOrEmpty(userId))
                    return NotFound();
                var userProfile = _userAccountService.FindUserById(userId);
                return Ok(userProfile);
            }
            catch(Exception exception)
            {
                return InternalServerError(exception);
            }
        }

    }
}
