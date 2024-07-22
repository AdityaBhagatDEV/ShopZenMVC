using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;
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

        public ActionResult Buy(int productId, int quantity)
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddToCart(int productId, int quantity)
        {

            if (Session["UserName"] != null)
            {
                string userName = Session["UserName"].ToString();
                var user = db.UserTables.FirstOrDefault(u => u.FirstName.Equals(userName));

                if (user != null)
                {
                    int userId = user.UserId;
                    var product = db.ProductTables.FirstOrDefault(p => p.ProductId == productId);

                    if (product != null)
                    {
                        var price = product.Price;
                        var total = price * quantity;

                        OrderTable orderTable = new OrderTable()
                        {
                            OrderId = userId,
                            UserId = userId,
                            OrderDate = DateTime.Now,
                            TotalAmount = total,
                            Status = "Add to Cart"
                        };
                        db.OrderTables.Add(orderTable);
                        db.SaveChanges();
                        return RedirectToAction("result", "Product");
                    }
                }
            }
			return RedirectToAction("Login", "Account");
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
