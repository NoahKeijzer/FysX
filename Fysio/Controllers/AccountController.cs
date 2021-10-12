using Fysio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fysio.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByEmailAsync(loginModel.Email);
                bool result = false;
                bool isTreator = false;
                if (user != null)
                {
                    var temp = await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
                    result = temp.Succeeded;

                    var claims = await userManager.GetClaimsAsync(user);
                    foreach (Claim claim in claims)
                    {
                        if (claim.Value.Equals("Treator")) isTreator = true;
                    }
                }

                if (result)
                {
                    return isTreator ? RedirectToAction("Index", "Home") : View("Register");
                }
                else
                {
                    return View("Index");
                }
            } else
            {
                return View("Index");
            }
            
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GoToRegister()
        {
            return Register();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser() { UserName = registerModel.Email.Split("@")[0], Email = registerModel.Email };
                var result = await userManager.CreateAsync(user, registerModel.Password);
                await userManager.AddClaimAsync(user, new Claim("Claim.Treator", "Treator"));
                await userManager.AddClaimAsync(user, new Claim("Claim.Fysio", "Fysio"));
                if (result.Succeeded)
                {
                    return await LoginAsync(new LoginModel(registerModel.Email, registerModel.Password));
                }
                else
                {
                    return View();
                }
            } else
            {
                return View();
            }
            
        }

        [Authorize]
        public async Task<IActionResult> LogOutAsync()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}