using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using VueWebApplication.Models;

namespace VueWebApplication.Infrastructure
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();  
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if (context.UserName == "Admin" && context.Password == "123456789")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                identity.AddClaim(new Claim("Username", "Admin"));
                context.Validated(identity);
            }
            else
            {
                MyDbContext db = new MyDbContext();
                AppUserManager userMgr = new AppUserManager(new UserStore<AppUser>(db));
                var user = await userMgr.FindAsync(context.UserName, context.Password);
                if (user == null)
                {
                    context.SetError("invalid_grant", "Пароль или логин неверны");
                    return;
                }

                identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
                identity.AddClaim(new Claim("username", context.UserName));

                context.Validated(identity);
            } 
        }
    }
}