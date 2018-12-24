using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VueWebApplication.Infrastructure;
using VueWebApplication.Models;

namespace VueWebApplication
{
    public class MyDbInitializer : DropCreateDatabaseAlways<MyDbContext>
    {
        protected override void Seed(MyDbContext context)
        { 
            AppUserManager userMgr = new AppUserManager(new UserStore<AppUser>(context));
            AppRoleManager roleMgr = new AppRoleManager(new RoleStore<AppRole>(context));

            string roleName = "Admin";
            string roleName2 = "Users";
            string userName = "Admin";
            string name = "Александр";
            string surname = "Медведев";
            string middlename = "Борисович";
            string notes = "Тут примечание";
            string password = "123456789";
            string email = "tomnolane@mail.ru";

            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new AppRole(roleName));
            }

            if (!roleMgr.RoleExists(roleName2))
            {
                roleMgr.Create(new AppRole(roleName2));
            }

            AppUser user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new AppUser
                {
                    UserName = userName,
                    Email = email,
                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "Ptz",
                            PostalCode = "184015",
                            Type = "home",
                            Streets = new List<Street>
                            {
                                    new Street
                                    {
                                        Street1 = "Древлянка",
                                        Street2 = "Ленина"
                                    }
                            }
                        }
                    },
                    Phones = new List<Phones>
                     {
                         new Phones { Type = "work", Value = "+79777474836" },
                         new Phones { Type = "home", Value = "+74954942123" }
                     },
                    Surname = surname,
                    Middlename = middlename,
                    Name = name,
                    Emails = new List<Emails>
                    {
                        new Emails {  Value = "medvedev.alexandr88@yandex.ru", Type = "personal"},
                        new Emails {  Value = "gotopredestenacia@gmail.com", Type = "work"}
                    },
                    Notes = notes,
                    Changes = new List<ChangesProfile>
                     {
                        new ChangesProfile
                        {
                          Date = DateTime.Now.ToString("Дата: dd.MM.yyyy время: hh:mm:ss")
                        }
                     }
                }, password);

                user = userMgr.FindByName(userName);
            }

            if (!userMgr.IsInRole(user.Id, roleName))
            {
                userMgr.AddToRole(user.Id, roleName);
            }

            base.Seed(context);
        }
    }

    
}