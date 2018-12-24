using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VueWebApplication.Infrastructure;

namespace VueWebApplication.Models
{
    public class AppUser : IdentityUser
    {
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        public string Surname { get; set; } // фамилия пользователя

        [Display(Name = "Отчество")]
        public string Middlename { get; set; } // отчество пользователя (может быть пустым)

        public virtual List<Address> Addresses { get; set; }
        public virtual List<Phones> Phones { get; set; }
        public virtual List<Emails> Emails { get; set; }
        public virtual List<ChangesProfile> Changes { get; set; }

        [Display(Name = "Заметки")]
        public string Notes { get; set; } // заметки

        public AppUser()
        {
        }

        public async System.Threading.Tasks.Task<IdentityResult> CreateAsync(CreateModel createModel)
        {
            MyDbContext db = new MyDbContext();
            AppUserManager userMgr = new AppUserManager(new UserStore<AppUser>(db));
            AppRoleManager roleMgr = new AppRoleManager(new RoleStore<AppRole>(db));
            AppUser appUser = new AppUser
            {
                UserName = createModel.Name,
                Email = createModel.Email,
                Addresses = new List<Address>
                {
                    new Address
                    {
                        City = createModel.City,
                        Streets = new List<Street>
                        {
                            new Street
                            {
                                Street1 =  createModel.Street1,
                                Street2 = createModel.Street2
                            }
                        },
                        PostalCode = createModel.PostalCode,
                        Type = "personal"
                    }
                },
                Phones = new List<Phones>
                {
                    new Phones
                    {
                         Type = "personal",
                         Value = createModel.Phone
                    }
                },
                Surname = createModel.Surname,
                Middlename = createModel.Middlename,
                Name = createModel.Name,
                Emails = new List<Emails>
                {
                    new Emails
                    {
                         Value = createModel.Email,
                         Type = "personal"
                    }
                },
                Notes = (!string.IsNullOrWhiteSpace(createModel.Notes) ? createModel.Notes : "новый пользователь")
            };

            IdentityResult result = await userMgr.CreateAsync(appUser, createModel.Password);

            await userMgr.AddToRoleAsync(userMgr.FindByName(createModel.Name).Id, "Users");

            // создаем claim для хранения года рождения
            var identityClaim = new IdentityUserClaim { ClaimType = "Users", ClaimValue = appUser.UserName.ToString() }; 
            appUser.Claims.Add(identityClaim); 
            await userMgr.UpdateAsync(appUser);
             

            return result;
        }
    }

    public class ChangesProfile
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public Guid Identifier { get; set; }
         
        public string Date { get; set; }
        [JsonIgnore]
        public virtual AppUser AppUserId { get; set; }
    }

    public class Address
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public Guid Identifier { get; set; }

        public string Type { get; set; }

        public virtual List<Street> Streets { get; set; }

        public string PostalCode { get; set; }
        public string City { get; set; }
        [JsonIgnore]
        public virtual AppUser AppUserId { get; set; }
    }

    public class Street
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public Guid Identifier { get; set; }

        public string Street1 { get; set; }
        public string Street2 { get; set; }
        [JsonIgnore]
        public virtual Address AddressId { get; set; }
    }

    public class Phones
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public Guid Identifier { get; set; }

        public string Type { get; set; }
        public string Value { get; set; }
        [JsonIgnore]
        public virtual AppUser AppUserId { get; set; }
    }

    public class Emails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public Guid Identifier { get; set; }

        public string Type { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректно указан Email")]
        [Required(ErrorMessage = "Email не может быть пустым")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Value { get; set; }

        public string SystemUserID { get; set; }

        [ForeignKey("SystemUserID")]
        [JsonIgnore]
        public virtual AppUser AppUserId { get; set; }
    }
}