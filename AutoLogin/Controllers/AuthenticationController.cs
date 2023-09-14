using AutoLogin.Data;
using AutoLogin.Models;
using AutoLogin.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AutoLogin.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthenticationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [RequireHttps]
        public IActionResult Registration()
        {
            return View("Registration");
        }
        [HttpPost]
        [RequireHttps]
        public async Task<IActionResult> Registration(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { FirstName = model.FirstName, LastName = model.LastName, PhoneNumber = model.PhoneNumber, Email = model.Email, UserName = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Login", "Authentication");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        [RequireHttps]
        public IActionResult Login()
        {
            var sessiondetail = HttpContext.Session.GetString("Login");
            if (sessiondetail != null)
            {
                return RedirectToAction("HomePage", "Authentication");
            }
            return View();
        }
        
        [HttpPost]
        [RequireHttps]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        HttpContext.Session.SetString("Login", model.Email);
                        return RedirectToAction("HomePage", "Authentication");
                    }
                }
                ModelState.AddModelError("Password", "Invalid email or password");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult HomePage()
        {
            var sessiondetail = HttpContext.Session.GetString("Login");
            if (sessiondetail == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            return View("HomePage");
        }
    }
}
