using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shopping_Tutorial.Repository;

namespace Shopping_Tutorial.Controllers
{
	public class ProductController : Controller
	{
		private readonly DataContext _dataContext;
		public ProductController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}
		public async Task<IActionResult> Index()
		{

			ViewData["searchQuery"] = "searchQuery";
			return View();
		}

		public IActionResult Create()
		{
			ViewBag.searchQuery = "searchQuery";
			ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name");
			ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name");
			return View();
		}
		
		
		public async Task<IActionResult> Details(int Id)
		{
			if (Id == null) return RedirectToAction("Index");
			var productsById = _dataContext.Products.Where(c => c.Id == Id).FirstOrDefault();
			return View(productsById);

		}

		public async Task<IActionResult> Search(string searchQuery)
		{
			var products = from p in _dataContext.Products.Include(p => p.Category).Include(p => p.Brand)
						   select p;

			if (!string.IsNullOrEmpty(searchQuery))
			{
				searchQuery = searchQuery.ToLower();
				products = products.Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{searchQuery}%"));
				ViewData["searchQuery"] = searchQuery;
			}
			else
			{
                ViewData["searchQuery"] = string.Empty;
            }

			return View(await products.OrderByDescending(p => p.Id).ToListAsync());
		}
	}
}
