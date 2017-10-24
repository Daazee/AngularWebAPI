using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using AngularWebAPI.WEBAPI.Models;
using AngularWebAPI.DataAccess.Models;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Text;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;

namespace AngularWebAPI.WEBAPI.Services
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var _userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            try
            {
                ApplicationUser user = _userManager.Find(context.UserName, context.Password);
                if (user == null)
                {
                    context.SetError("invalid_grant", "Username or Password incorrect. Please try again.");
                    return;
                }
                
                var claimsidentity = new ClaimsIdentity(context.Options.AuthenticationType);
                //AddClaims(user, claimsidentity);
                claimsidentity.AddClaim(new Claim("sub", context.UserName));
                claimsidentity.AddClaim(new Claim(ClaimTypes.Email, user.UserName));
                claimsidentity.AddClaim(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));
                claimsidentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                
                context.Validated(claimsidentity);
               // _userManager.ResetAccessFailedCount(user.Id);
                var roles = new StringBuilder();
                foreach (string role in _userManager.GetRoles(user.Id))
                {
                    claimsidentity.AddClaim(new Claim(ClaimTypes.Role, role));
                    roles.Append(role + ';');
                }

                roles = roles.ToString().EndsWith(";") ? roles.Remove(roles.Length - 1, 1) : roles;
                var authProperties = new Dictionary<string, string>
                {
                    {"Roles", roles.ToString()}
                };
                var props = new AuthenticationProperties(authProperties);
                var authTicket = new AuthenticationTicket(claimsidentity, props);
                context.Validated(authTicket);
                return;

            }
            catch (Exception ex)
            {
                context.SetError("invalid_grant", "An error occurred trying to log in.");
            }

        }


        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}