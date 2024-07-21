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
    public class MainHomeViewModelsController : Controller
    {
        private ShopZenEntities1 db = new ShopZenEntities1();

		// GET: MainHomeViewModels
		public ActionResult Home()
        {
            
            return View();
        }

    }
}
