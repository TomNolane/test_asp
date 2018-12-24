using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VueWebApplication.Infrastructure;
using VueWebApplication.Models;

namespace VueWebApplication
{
    public partial class Startup
    { 
        public void ConfigureAuth(IAppBuilder app)
        {
            MyDbInitializer md = new MyDbInitializer();
            
            app.CreatePerOwinContext(() => new MyDbContext()); 
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);
             
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            


        }
    }
}