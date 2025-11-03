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

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM registerVM)
		{

			await ChangeRole();

			if (!ModelState.IsValid)
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

			var userCount = _userManager.Users.Count();

			if (userCount == 1)
			{
				await _userManager.AddToRoleAsync(appUser, "SuperAdmin");
			}
			else if (userCount == 2)
			{
				await _userManager.AddToRoleAsync(appUser, "Admin");
			}
			else
			{
				await _userManager.AddToRoleAsync(appUser, "Member");
			}

			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginVM loginVM)
		{
			if (!ModelState.IsValid)
				return View(loginVM);

			var user = await _userManager.FindByEmailAsync(loginVM.Email);

			if (user == null)
			{
				ModelState.AddModelError("", "Not User");
				return View(loginVM);
			}

			var result = await _signInManager.PasswordSignInAsync(
				user.UserName,
				loginVM.Password,
				true,
				true
				);

			if (result.Succeeded)
			{
				return RedirectToAction("Index", "Home");
			}

			ModelState.AddModelError("", "Wrong email or password");
			return View(loginVM);
		}
		public async Task ChangeRole()
		{
			if (!await _roleManager.RoleExistsAsync(Helpers.Helper.CreateRole.SuperAdmin.ToString()))
			{
				await _roleManager.CreateAsync(new IdentityRole(Helpers.Helper.CreateRole.SuperAdmin.ToString()));
			}
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
