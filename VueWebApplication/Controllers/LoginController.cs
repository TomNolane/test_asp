using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VueWebApplication.Models;

namespace VueWebApplication.Controllers
{
    public class LoginController : Controller
    {
        MyDbContext db = new MyDbContext(); 

        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Title = "Логин";
            return View();
        }
    }
}