using Indingo.Models;
using Indingo.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Indingo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM newuser)
        {
            if (!ModelState.IsValid) { return View(); }
            AppUser user = new AppUser()
            {
                Name = newuser.Name,
                Email = newuser.Email,
                UserName = newuser.UserName,
                Surname = newuser.Surname,
            };

            IdentityResult result = await _userManager.CreateAsync(user, newuser.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
                return View();
            }
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home", new { Areas = "" });
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM user)
        {
            if (!ModelState.IsValid) { return View(); }
            AppUser exist = await _userManager.FindByEmailAsync(user.UsernameOrEmail);
            if (exist == null)
            {
                exist = await _userManager.FindByNameAsync(user.UsernameOrEmail);
                if (exist == null)
                {
                    ModelState.AddModelError("", "Username or Email or Password is False");
                    return View();
                }
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(exist, user.Password,user.IsRememberMe,true );
            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Email or Password is False");
                return View();
            }
            return RedirectToAction("Index", "Home", new { Areas = "" });

        }
    }
}

