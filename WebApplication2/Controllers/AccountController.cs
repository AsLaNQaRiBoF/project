using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;
using WebApplication2.ViewModel;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View();
            ApplicationUser newUser = new ApplicationUser()
            {
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                Email = registerVM.Email,
                UserName = registerVM.UserName,
            };
            IdentityResult result = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            return View();
        }
            public IActionResult Login()
        {
            return View();
        }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Login(LoginVM loginVM)
            {
                if (!ModelState.IsValid) return View();

                if (loginVM.UserNameOrEmail.Contains("@"))
                {
                    ApplicationUser user = await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);
                    if (user != null)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, true);
                        if (result.IsLockedOut)
                        {
                            ModelState.AddModelError("", "Gozde");
                            return View();
                        }
                        if (!user.EmailConfirmed)
                        {
                            ModelState.AddModelError("", "Emaili Yaz Qaqa");
                            return View();
                        }
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", "Qaqa Duz Yazmirsan");
                            return View();
                        }

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ApplicationUser user = await _userManager.FindByNameAsync(loginVM.UserNameOrEmail);
                    if (user != null)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, true);
                        if (result.IsLockedOut)
                        {
                            ModelState.AddModelError("", "Gozde ");
                            return View();
                        }
                        if (!user.EmailConfirmed)
                        {
                            ModelState.AddModelError("", "Emaili Yaz Qaqa");
                            return View();
                        }
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", "Qaqa Duz Yazmirsan");
                            return View();
                        }

                        return RedirectToAction("Index", "Home");
                    }
                }
                return View();
            }

            public async Task<IActionResult> LogOut()
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
        }

    }

    


