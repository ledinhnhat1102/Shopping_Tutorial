﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping_Tutorial.Models;
using Shopping_Tutorial.Repository;

namespace Shopping_Tutorial.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize]
	public class BrandController : Controller
    {

        private readonly DataContext _dataContext;
        public BrandController(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Brands.OrderByDescending(p => p.Id).ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandModel brand)
        {
            if (ModelState.IsValid)
            {
                //them
                brand.Slug = brand.Name.Replace(" ", "-");
                var slug = await _dataContext.Brands.FirstOrDefaultAsync(p => p.Slug == brand.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Thương hiệu đã có trong database");
                    return View(brand);
                }
                _dataContext.Add(brand);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Thêm thương hiệu thành công ";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Thêm thương hiệu thất bại";
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }

            return View(brand);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            BrandModel brand = await _dataContext.Brands.FindAsync(Id);
            _dataContext.Brands.Remove(brand);
            await _dataContext.SaveChangesAsync();
            TempData["success"] = "Xóa thương hiệu thành công ";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int Id)
        {
            BrandModel brand = await _dataContext.Brands.FindAsync(Id);
            return View(brand);
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(BrandModel brand)
		{
			if (ModelState.IsValid)
			{
				// Lấy thông tin thương hiệu hiện tại từ database
				var existingBrand = await _dataContext.Brands.AsNoTracking().FirstOrDefaultAsync(p => p.Id == brand.Id);

				if (existingBrand == null)
				{
					TempData["error"] = "Thương hiệu không tồn tại";
					return RedirectToAction("Index");
				}

				// Kiểm tra nếu slug mới không trùng với slug hiện tại và slug mới đã tồn tại trong database
				if (existingBrand.Slug != brand.Slug)
				{
					brand.Slug = brand.Name.Replace(" ", "-");
					var slug = await _dataContext.Brands.FirstOrDefaultAsync(p => p.Slug == brand.Slug);
					if (slug != null)
					{
						ModelState.AddModelError("", "Thương hiệu đã có trong database");
						return View(brand);
					}
				}
				else
				{
					// Giữ nguyên slug nếu không thay đổi
					brand.Slug = existingBrand.Slug;
				}

				// Cập nhật thương hiệu
				_dataContext.Update(brand);
				await _dataContext.SaveChangesAsync();
				TempData["success"] = "Cập nhật thương hiệu thành công";
				return RedirectToAction("Index");
			}
			else
			{
				TempData["error"] = "Cập nhật thương hiệu thất bại";
				List<string> errors = new List<string>();
				foreach (var value in ModelState.Values)
				{
					foreach (var error in value.Errors)
					{
						errors.Add(error.ErrorMessage);
					}
				}
				string errorMessage = string.Join("\n", errors);
				return BadRequest(errorMessage);
			}

			return View(brand);
		}



	}
}
