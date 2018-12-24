using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VueWebApplication.Models
{
    public class UserModelView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Middlename { get; set; }
        public string Login { get; set; }
        public List<Address> Address { get; set; } 
        public List<Phones> Phones { get; set; }
        public List<Emails> Emails { get; set; }
        public List<ChangesProfile> Changes { get; set; }
        public string Notes { get; set; }
        public string Roles { get; set; }

        public UserModelView() { }

        public UserModelView(AppUser appUser)
        {
            Id = appUser.Id;
            Login = appUser.UserName;
            Name = appUser.Name;
            Surname = appUser.Surname;
            Middlename = appUser.Middlename;
            Address = appUser.Addresses;
            Phones = appUser.Phones;
            Emails = appUser.Emails;
            Changes = appUser.Changes;
            Notes = appUser.Notes;
            Roles = AppRole.GetIdentityRoleById(appUser.Roles.Select(x => x.RoleId).FirstOrDefault()).Name;
        }
    }

    public class EditUserModel
    { 
        public string Id { get; set; } 
        [StringLength(50, ErrorMessage = "Длина пароля должна быть от 6 до 50 символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректно указан email")] 
        [StringLength(50, ErrorMessage = "Длина email должна быть от 6 до 50 символов", MinimumLength = 6)]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email1 { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректно указан email")]
        [StringLength(50, ErrorMessage = "Длина email должна быть от 6 до 50 символов", MinimumLength = 6)]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email2 { get; set; }

        [RegularExpression(@"[a-zA-ZА-Яа-я]{2,}", ErrorMessage = "Некорректно указано типа email")]
        [StringLength(20, ErrorMessage = "Длина типа email должна быть от 2 до 20 символов", MinimumLength = 2)]
        public string Email1_Type { get; set; }

        [RegularExpression(@"[a-zA-ZА-Яа-я]{2,}", ErrorMessage = "Некорректно указано типа email")]
        [StringLength(20, ErrorMessage = "Длина типа email должна быть от 2 до 20 символов", MinimumLength = 2)]
        public string Email2_Type { get; set; }

        [RegularExpression(@"[a-zA-ZА-Яа-я]{2,}", ErrorMessage = "Некорректно указано имя")] 
        [StringLength(20, ErrorMessage = "Длина имени должна быть от 2 до 20 символов", MinimumLength = 2)]
        public string Name { get; set; }

        [RegularExpression(@"[a-zA-ZА-Яа-я]{2,}", ErrorMessage = "Некорректно указана фамилия")]
        [Display(Name = "Фамилия")] 
        [StringLength(30, ErrorMessage = "Длина фамилии должна быть от 1 до 30 символов", MinimumLength = 1)]
        public string Surname { get; set; }

        [RegularExpression(@"[a-zA-ZА-Яа-я]{2,}", ErrorMessage = "Некорректно указано отчество")]
        [Display(Name = "Отчество")]
        [StringLength(40, ErrorMessage = "Длина отчества должна быть до 40 символов")]
        public string Middlename { get; set; }

        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Некорректно указан номер телефона")]
        [Display(Name = "Номер телефона")] 
        public string Phone1 { get; set; }

        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Некорректно указан номер телефона")]
        [Display(Name = "Номер телефона")]
        public string Phone2 { get; set; }

        [RegularExpression(@"[a-zA-ZА-Яа-я]{2,}", ErrorMessage = "Некорректно указано тип телефона")]
        [StringLength(20, ErrorMessage = "Длина типа телефона должна быть от 2 до 20 символов", MinimumLength = 2)]
        public string Phone1_Type { get; set; }

        [RegularExpression(@"[a-zA-ZА-Яа-я]{2,}", ErrorMessage = "Некорректно указано тип телефона")]
        [StringLength(20, ErrorMessage = "Длина типа телефона должна быть от 2 до 20 символов", MinimumLength = 2)]
        public string Phone2_Type { get; set; }

        [RegularExpression(@"[a-zA-ZА-Яа-я\s-.\/\d]{2,}", ErrorMessage = "Некорректно указано названия города")]
        [Display(Name = "Город проживания")]
        [StringLength(40, ErrorMessage = "Длина названия города должна быть от 1 до 40 символов", MinimumLength = 1)]
        public string City { get; set; }
         
        [RegularExpression(@"[\d]{6}", ErrorMessage = "Некорректно указан почтовый индекс")]
        [Display(Name = "Почтовый индекс")]
        [StringLength(6, ErrorMessage = "Длина почтового индекса должна быть 6 символов", MinimumLength = 6)]
        public string PostalCode { get; set; }
         
        [StringLength(100, ErrorMessage = "Длина адреса проживания должна быть от 1 до 100 символов", MinimumLength = 1)]
        [Display(Name = "Адрес проживания")]
        [RegularExpression(@"[a-zA-ZА-Яа-я\s-.\/\d]+", ErrorMessage = "Некорректно указан адреса проживания")]
        public string Street1 { get; set; }
         
        [StringLength(100, ErrorMessage = "Длина почтового адреса должна быть от 1 до 100 символов", MinimumLength = 1)]
        [Display(Name = "Почтовый адрес")]
        [RegularExpression(@"[a-zA-ZА-Яа-я\s-.\/\d]+", ErrorMessage = "Некорректно указан почтовый адрес")]
        public string Street2 { get; set; }

        [Display(Name = "Заметки")]
        [RegularExpression(@"[a-zA-ZА-Яа-я\s-.\/\d]*", ErrorMessage = "Некорректно указана заметка")]
        [StringLength(100, ErrorMessage = "Длина заметки должна быть до 100 символов")]
        public string Notes { get; set; }

        [Display(Name = "Роль")]
        [RegularExpression(@"[a-zA-ZА-Яа-я\s-.\/\d]*", ErrorMessage = "Некорректно указана роль")]
        [StringLength(100, ErrorMessage = "Длина заметки должна быть до 100 символов")]
        public string Role { get; set; }
        public string Who { get; set; }
    }

    public class CreateModel
    { 
        [Required]
        [StringLength(50, ErrorMessage = "Длина пароля должна быть от 6 до 50 символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректно указан email")]
        [Required(ErrorMessage = "Email не может быть пустым")]
        [StringLength(50, ErrorMessage = "Длина email должна быть от 6 до 50 символов", MinimumLength = 6)]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [RegularExpression(@"[a-zA-ZА-Яа-я]{2,}", ErrorMessage = "Некорректно указано имя")]
        [Required(ErrorMessage = "Укажите ваше имя")]
        [StringLength(20, ErrorMessage = "Длина имени должна быть от 2 до 20 символов", MinimumLength = 2)]
        public string Name { get; set; }

        [RegularExpression(@"[a-zA-ZА-Яа-я]{2,}", ErrorMessage = "Некорректно указана фамилия")]
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Укажите вашу фамилию")]
        [StringLength(30, ErrorMessage = "Длина фамилии должна быть от 1 до 30 символов", MinimumLength = 1)]
        public string Surname { get; set; }

        [RegularExpression(@"[a-zA-ZА-Яа-я]{2,}", ErrorMessage = "Некорректно указано отчество")]
        [Display(Name = "Отчество")]
        [StringLength(40, ErrorMessage = "Длина отчества должна быть до 40 символов")]
        public string Middlename { get; set; }

        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Некорректно указан номер телефона")]
        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "Укажите номер телефона")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Укажите город проживания")]
        [RegularExpression(@"[a-zA-ZА-Яа-я\s-.\/\d]{2,}", ErrorMessage = "Некорректно указано названия города")]
        [Display(Name = "Город проживания")]
        [StringLength(40, ErrorMessage = "Длина названия города должна быть от 1 до 40 символов", MinimumLength = 1)]
        public string City { get; set; }

        [Required(ErrorMessage = "Укажите почтовый индекс")]
        [RegularExpression(@"[\d]{6}", ErrorMessage = "Некорректно указан почтовый индекс")]
        [Display(Name = "Почтовый индекс")]
        [StringLength(6, ErrorMessage = "Длина почтового индекса должна быть 6 символов", MinimumLength = 6)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Укажите адрес продивания")]
        [StringLength(100, ErrorMessage = "Длина адреса проживания должна быть от 1 до 100 символов", MinimumLength = 1)]
        [Display(Name = "Адрес проживания")]
        [RegularExpression(@"[a-zA-ZА-Яа-я\s-.\/\d]+", ErrorMessage = "Некорректно указан адреса проживания")]
        public string Street1 { get; set; }

        [Required(ErrorMessage = "Укажите почтовый адрес")]
        [StringLength(100, ErrorMessage = "Длина почтового адреса должна быть от 1 до 100 символов", MinimumLength = 1)]
        [Display(Name = "Почтовый адрес")]
        [RegularExpression(@"[a-zA-ZА-Яа-я\s-.\/\d]+", ErrorMessage = "Некорректно указан почтовый адрес")]
        public string Street2 { get; set; }

        [Display(Name = "Заметки")]
        [RegularExpression(@"[a-zA-ZА-Яа-я\s-.\/\d]*", ErrorMessage = "Некорректно указана заметка")]
        [StringLength(100, ErrorMessage = "Длина заметки должна быть до 100 символов")]
        public string Notes { get; set; }

        [Display(Name = "Роль")]
        [RegularExpression(@"[a-zA-ZА-Яа-я\s-.\/\d]*", ErrorMessage = "Некорректно указана роль")]
        [StringLength(100, ErrorMessage = "Длина заметки должна быть до 100 символов")]
        public string Role { get; set; }
    }

    public class LoginModel
    {
        [RegularExpression(@"[a-zA-ZА-Яа-я]{2,}", ErrorMessage = "Некорректно указано имя")]
        [Required(ErrorMessage = "Укажите ваше имя")]
        [StringLength(20, ErrorMessage = "Длина имени должна быть от 2 до 20 символов", MinimumLength = 2)]
        public string Login { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Длина пароля должна быть от 6 до 50 символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}