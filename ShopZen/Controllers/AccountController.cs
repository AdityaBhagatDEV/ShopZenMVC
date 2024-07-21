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
                var user = db.UserTables.FirstOrDefault(x => x.Email == model.Email);
                if (user != null && user.Password == model.Password)
                {
					var user1 = db.UserTables.FirstOrDefault(x => x.Email == model.Email);
					var userId = user?.UserId; 
					Session["UserId"] = user.UserId;
					return RedirectToAction("result", "Product");
                }
                else { ModelState.AddModelError("", "Invalid login attempt."); }
            }
            return View(model);
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
