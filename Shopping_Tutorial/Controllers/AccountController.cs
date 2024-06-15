using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shopping_Tutorial.Models;

namespace Shopping_Tutorial.Controllers
{
	public class AccountController : Controller
	{
		private UserManager<AppUserModel> _userManage;
		private SignInManager<AppUserModel> _signInManager;
		public AccountController(SignInManager<AppUserModel> signInManager, UserManager<AppUserModel> userManager)
		{
			_signInManager = signInManager;
			_userManage = userManager;
		}
		public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> Login()
		{
			return View();
		}
	}
}
