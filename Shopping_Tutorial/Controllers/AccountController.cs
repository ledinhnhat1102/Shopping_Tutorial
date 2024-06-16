﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shopping_Tutorial.Models;
using Shopping_Tutorial.Models.ViewModels;

namespace Shopping_Tutorial.Controllers
{
	public class AccountController: Controller
	{
		private UserManager<AppUserModel> _userManager;
		private SignInManager<AppUserModel> _signInManager;
		public AccountController(SignInManager<AppUserModel> signInManager, UserManager<AppUserModel> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}	

		public IActionResult Login(string returnUrl)
		{
			return View(new LoginViewModel { ReturnUrl = returnUrl });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel loginVM)
		{
			if (ModelState.IsValid)
			{
				Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, false, false);
				if (result.Succeeded)
				{
					return Redirect(loginVM.ReturnUrl ?? "/");
				}
				ModelState.AddModelError("", "Invalid Username and Password");
			}
			return View(loginVM);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public  async Task<IActionResult> Create(UserModel user)
		{
			if (ModelState.IsValid) 
			{
				AppUserModel newUser = new AppUserModel { UserName = user.UserName, Email = user.Email };
				IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);
				if (result.Succeeded)
				{
					TempData["success"] = "Tạo user thành công";
					return Redirect("/Account/Login");
				}
				foreach(IdentityError error in result.Errors)
				{
					ModelState.AddModelError("",error.Description);
				}
			}
			return View();
		}

		public async Task<IActionResult> Logout(string returnUrl = "/")
		{
			await _signInManager.SignOutAsync();
			return Redirect(returnUrl);
		}
	}
}
