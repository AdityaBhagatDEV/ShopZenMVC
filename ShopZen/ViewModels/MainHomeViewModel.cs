using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopZen.Models;

namespace ShopZen.ViewModels
{
	public class MainHomeViewModel
	{
		public List<ProductTable> productTable {  get; set; }
		public List<CategoryTable> categoriesTable { get; set; }

	}
}
