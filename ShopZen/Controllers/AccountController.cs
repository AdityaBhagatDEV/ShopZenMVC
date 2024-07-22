using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopZen.Models;
using ShopZen.ViewModels;

namespace ShopZen.Controllers
{
    public class AccountController : Controller
    {
        private ShopZenEntities1 db = new ShopZenEntities1();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
				var user = db.UserTables.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
				if (user != null)
				{
					// Set the session value
					Session["UserName"] = user.FirstName; // Assuming UserTable has a UserName property

					// Redirect to a secure page or home page after login
					return RedirectToAction("result", "Product");
				}
				else
				{
					ModelState.AddModelError("", "Invalid login attempt.");
				}
			}
            return View(model);
        }

		public ActionResult Logout()
		{
            Session.Clear();
            Session.Abandon();
			if (Request.Cookies[".ASPXAUTH"] != null)
			{
				var cookie = new HttpCookie(".ASPXAUTH")
				{
					Expires = DateTime.Now.AddDays(-1),
					Value = null
				};
				Response.Cookies.Add(cookie);
			}
			return RedirectToAction("Login", "Account");
		}

		[HttpGet]
        public ActionResult CreateAccount() 
        { 
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccount(UserTable usermodel)
        {
            if (ModelState.IsValid)
            {
                usermodel.CreatedAt = DateTime.Now;
                db.UserTables.Add(usermodel);
                db.SaveChanges();
                return RedirectToAction("Login", "Account");
            }
            return View(usermodel);
        }
    }
}
