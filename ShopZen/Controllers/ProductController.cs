using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ShopZen.Models;
using ShopZen.ViewModels;

namespace ShopZen.Controllers
{
    public class ProductController : Controller
    {
        private ShopZenEntities1 db = new ShopZenEntities1();
        //public ActionResult result()
        //{
        //    var product = (from a in db.ProductTables select a).ToList();
        //    var category = (from c in db.CategoryTables select c).ToList();

        //    var model = new MainHomeViewModel
        //    {
        //        productTable = product,
        //        categoriesTable = category
        //    };
        //    return View(model);
        //}

        public ActionResult result(string searchQuery)
        {
            var productsQuery = from a in db.ProductTables select a;
            var categories = db.CategoryTables.ToList();
            var userId = Session["UserId"];
			if (userId != null)
            {
                // Use the userId as needed
                ViewBag.Message = $"Logged in user ID: {userId}";
                ViewData["User"] = userId;
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                productsQuery = from product in db.ProductTables
                                join category in db.CategoryTables
                                on product.CategoryId equals category.CategoryId
                                where product.ProductName.Contains(searchQuery) ||
                                category.CategoryName.Contains(searchQuery)
                                select product;
            }
			var products = productsQuery.ToList();


            var model = new MainHomeViewModel
            {
                productTable = products,
                categoriesTable = categories
            };

            return View(model);
        }
    

		// GET: Product


		//public ActionResult result(string searchQuery)
		//      {
		//          var db= new ShopZenEntities1();
		//          var product = (from a in db.ProductTables select a).ToList();
		//	var category = (from c in db.CategoryTables select c).ToList();

		//	if (!string.IsNullOrEmpty(searchQuery))
		//          {
		//              product = product.Where(x => x.ProductName.Contains(searchQuery)).ToList();
		//          }
		//	var model = new MainHomeViewModel
		//	{
		//		productTable = product,
		//		categoriesTable = category
		//	};
		//          return View(model);
		//      }

		public ActionResult Index()
        {
            return View();
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
