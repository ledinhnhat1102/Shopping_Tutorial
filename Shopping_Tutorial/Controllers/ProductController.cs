using Microsoft.AspNetCore.Mvc;
using Shopping_Tutorial.Repository;

namespace Shopping_Tutorial.Controllers
{
	public class ProductController: Controller
	{
		private readonly DataContext _dataContext;
		public ProductController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}
		public ActionResult Index(string searchText)
		{
			ViewData["CurrentFilter"] = searchText;
			var product = from m in _dataContext.Products
						  select m;
			if (!String.IsNullOrEmpty(searchText))
			{
				product = product.Where(m => m.Name.Contains(searchText));
			}
			return View(product);
		}
		public async Task<IActionResult> Details(int Id )
		{
			if (Id == null) return RedirectToAction("Index");
			var productsById = _dataContext.Products.Where(c => c.Id == Id).FirstOrDefault();
			return View(productsById);
		}
	}
}
