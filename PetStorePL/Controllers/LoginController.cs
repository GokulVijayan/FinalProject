using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetStorePL.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string username,string password)
        {
            if(username=="Admin"&&password=="Admin")
            {
                return RedirectToAction("Create", "PetDetails");
            }
            else
            return View();
        }
    }
}