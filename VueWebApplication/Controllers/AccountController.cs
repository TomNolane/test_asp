using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results; 
using System.Web.Script.Serialization;
using System.Web.Security;
using VueWebApplication.Infrastructure;
using VueWebApplication.Models;

namespace VueWebApplication.Controllers
{
    public class AccountController : ApiController
    {
        private MyDbContext db = new MyDbContext(); 

        private List<AppUser> GetAllAppUser()
        {
            var g = db.Users.AsNoTracking().ToList();
            return g;
        }

        private AppUser FindAppUser(string login)
        {
            var g = db.Users.Where(x => x.UserName == login).FirstOrDefault();
            return g;
        }

        private AppUser FindAppUserById(string id)
        {
            var g = db.Users.Where(x => x.Id == id).FirstOrDefault();
            return g;
        }

        private List<UserModelView> GetAllUserModelView()
        {
            var g = db.Users.ToList();
            List<UserModelView> umv = new List<UserModelView>();
            foreach (var temp in g)
            {
                umv.Add(new UserModelView(temp));
            }
            return umv;
        }

        private UserModelView GetuserByUserName(string username)
        {
            var g = db.Users.Where(x => x.UserName == username).FirstOrDefault();
            UserModelView umv = new UserModelView(g);
            return umv;
        }

        [Route("Accounts")]
        [System.Web.Http.Authorize(Roles = "Admin")]
        public IHttpActionResult Get()
        {
            var _getUsers = GetAllUserModelView();

            if (_getUsers != null)
                return Json(_getUsers);
            else
                return Json("пусто"); 
        }

        [System.Web.Http.Authorize]
        [Route("Accounts/{username:alpha}")]
        public IHttpActionResult Get(string username)
        { 

            return Json(FindAppUser(username));
        }
 
        [HttpPost]
        [System.Web.Http.Authorize]
        [Route("LoginIn")]
        public IHttpActionResult Post([FromBody]LoginModel loginModel)
        {  
            AppUserManager userMgr = new AppUserManager(new UserStore<AppUser>(db));
            var model = FindAppUser(loginModel.Login);

            if ( userMgr.CheckPassword(model, loginModel.Password) )
            { 
                return Json(new UserModelView(model));
            }
            else
            {
                var message =  "Неверный логин или пароль";
                HttpError err = new HttpError(message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Unauthorized,err));
            } 
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Registration")]
        public async System.Threading.Tasks.Task<IHttpActionResult> PostRegistrationAsync([FromBody]CreateModel createModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AppUser app = new AppUser();
            
            IdentityResult result = await app.CreateAsync(createModel);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok("OK");
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }
            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
                if (ModelState.IsValid)
                { 
                    return BadRequest();
                }
                return BadRequest(ModelState);
            }
            return null;
        }

        [System.Web.Http.Authorize]
        [HttpPost]
        [Route("EditPost")]
        public async System.Threading.Tasks.Task<IHttpActionResult> PostEditAsync([FromBody]EditUserModel value)
        { 
                AppUserManager userMgr = new AppUserManager(new UserStore<AppUser>(db));
                var model = FindAppUserById(value.Id);

                if (model != null)
                { 
                    model.Middlename = value.Middlename;
                    model.Surname = value.Surname; 
                    model.Name = value.Name;
                    model.Changes.Add(new ChangesProfile { Date = DateTime.Now.ToString("Дата: dd.MM.yyyy время: hh:mm:ss Кто менял: ") + value.Who });

                    if (!string.IsNullOrWhiteSpace(value.Notes))
                    {
                        model.Notes = value.Notes;
                    }

                    if (!string.IsNullOrWhiteSpace(value.Email1))
                    {
                        model.Emails[0].Value = value.Email1;
                        model.Emails[0].Type = value.Email1_Type;
                    }
                        
                    if (!string.IsNullOrWhiteSpace(value.Email2))
                    {
                        model.Emails[1].Value = value.Email2;
                        model.Emails[1].Type = value.Email2_Type;
                    }

                    if (!string.IsNullOrWhiteSpace(value.Phone1))
                    {
                        model.Phones[0].Value = value.Phone1;
                        model.Phones[0].Type = value.Phone1_Type;
                    }

                    if (!string.IsNullOrWhiteSpace(value.Phone2))
                    {
                        model.Phones[1].Value = value.Phone2;
                        model.Phones[1].Type = value.Phone2_Type;
                    } 

                    var result = await userMgr.UpdateAsync(model);
                    if (!result.Succeeded)
                    {
                        return GetErrorResult(result);
                    }

                    return Json(new UserModelView(model)); 
                }

            return Ok("Error"); 
        }

        [System.Web.Http.Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("EditAdmin")]
        public async System.Threading.Tasks.Task<IHttpActionResult> PostEditAdminAsync([FromBody]EditUserModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            AppUserManager userMgr = new AppUserManager(new UserStore<AppUser>(db));

            var model = FindAppUserById(value.Id);

            if (model != null)
            {
                model.Middlename = value.Middlename;
                model.Surname = value.Surname;
                model.Name = value.Name; 

                model.Changes.Add(new ChangesProfile { Date = DateTime.Now.ToString("Дата: dd.MM.yyyy время: hh:mm:ss Кто менял: " ) + value.Who });

                if (!string.IsNullOrWhiteSpace(value.Notes))
                {
                    model.Notes = value.Notes;
                }

                if (!string.IsNullOrWhiteSpace(value.Email1))
                {
                    model.Emails[0].Value = value.Email1;
                    model.Emails[0].Type = value.Email1_Type;
                }

                if (!string.IsNullOrWhiteSpace(value.Email2))
                {
                    if(model.Emails.Count > 1)
                    {
                        model.Emails[1].Value = value.Email2;
                        model.Emails[1].Type = value.Email2_Type;
                    }
                    else
                    {
                        model.Emails.Add(
                            new Emails { Value = value.Email2, Type = value.Email2_Type }
                        );
                    }
                }

                if (!string.IsNullOrWhiteSpace(value.Phone1))
                {
                    model.Phones[0].Value = value.Phone1;
                    model.Phones[0].Type = value.Phone1_Type;
                }

                if (!string.IsNullOrWhiteSpace(value.Phone2))
                {
                    if (model.Phones.Count > 1)
                    {
                        model.Phones[1].Value = value.Phone2;
                        model.Phones[1].Type = value.Phone2_Type;
                    }
                    else
                    {
                        model.Phones.Add(
                            new Phones { Type = value.Phone2_Type, Value = value.Phone2 }
                        );
                    } 
                }

                var result = await userMgr.UpdateAsync(model);
                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                return Json("OK");
            }

            return Ok("Error");
        }

        [System.Web.Http.Authorize(Roles = "Admin")]
        [HttpPost] 
        [Route("DeleteUser/{username:alpha}")]
        public async System.Threading.Tasks.Task<IHttpActionResult> Delete(string name)
        {
            AppUserManager userMgr = new AppUserManager(new UserStore<AppUser>(db));
            AppUser user = await userMgr.FindByNameAsync(name);
            if (user != null)
            {
                IdentityResult result = await userMgr.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
                return Ok("OK");
            }

            return BadRequest(ModelState);
        }

        public IEnumerable<AppUser> GetData(string selectedRole, List<AppUser> UserData)
        {
            IEnumerable<AppUser> users = UserData;
            if (selectedRole != "All")
            {
                AppRole selected = (AppRole)Enum.Parse(typeof(AppRole), selectedRole);
                users = UserData.Where(p => p.Roles == selected);
            }
            return users;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    } 
}