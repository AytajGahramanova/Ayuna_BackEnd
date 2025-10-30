using ayuna_main.Models;
using ayuna_main.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

//UserManager
//SignInManager
//RoleManager

namespace ayuna_main.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly SignInManager<AppUser> _signInManager;
		public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;
		}
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM registerVM)
		{
			if(!ModelState.IsValid)
			{
				return View(registerVM);
			}

			AppUser appUser = new AppUser
			{
				Name = registerVM.Name,
				Surname = registerVM.Surname,
				UserName = registerVM.Name,
				Email = registerVM.Email,
			};

			IdentityResult identityResult = await _userManager.CreateAsync(appUser, registerVM.Password);
			if (!identityResult.Succeeded)
			{
				foreach (var error in identityResult.Errors)
				{
					ModelState.AddModelError("", error.Description);
					return View(registerVM);
				}
			}

			await _signInManager.SignInAsync(appUser, true);

			return RedirectToAction("Index", "Home");
		}

		public async Task ChangeRole()
		{
			if (!await _roleManager.RoleExistsAsync(Helpers.Helper.CreateRole.Admin.ToString()))
			{
				await _roleManager.CreateAsync(new IdentityRole(Helpers.Helper.CreateRole.Admin.ToString()));
			}
			if (!await _roleManager.RoleExistsAsync(Helpers.Helper.CreateRole.Member.ToString()))
			{
				await _roleManager.CreateAsync(new IdentityRole(Helpers.Helper.CreateRole.Member.ToString()));
			}
		}

		public async Task<IActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
