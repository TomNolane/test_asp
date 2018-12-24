using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VueWebApplication.Infrastructure;

namespace VueWebApplication.Models
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base() { }

        public AppRole(string name) : base(name) { }

        public static async System.Threading.Tasks.Task<string[]> FindRoleAsync(string id)
        {
            //AppUser 
            //AppRole role = await RoleManager.FindByIdAsync(id);
            return new string[] { "" };
        }

        private static RoleManager<IdentityRole> RoleManager =
        new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new MyDbContext()));

        public static IdentityRole GetIdentityRole(string roleName)
        {
            return RoleManager.FindByName(roleName);
        }

        public static IdentityRole GetIdentityRoleById(string id)
        {
            return RoleManager.FindById(id);
        }
    }
}