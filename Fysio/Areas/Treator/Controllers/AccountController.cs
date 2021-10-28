using Fysio.Areas.Treator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fysio.Areas.Treator.Controllers
{
    [Area("Treator")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private static HttpClient client = new HttpClient();

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


                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://fysxapi.azurewebsites.net/api/authorization");
                    var json = JsonConvert.SerializeObject(loginModel);
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.SendAsync(request).Result;

                    TokenModel tokenModel = JsonConvert.DeserializeObject<TokenModel>(response.Content.ReadAsStringAsync().Result);

                    CookieOptions option = new CookieOptions();

                    option.Expires = DateTime.Parse(tokenModel.expiration);
                    option.Secure = true;

                    Response.Cookies.Append("apiToken", tokenModel.token, option);
                }

                if (result)
                {
                    return isTreator ? RedirectToAction("Index", "Home") : RedirectToAction("Index", "Home", new { area = "Patient" });
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
            return RedirectToAction("Index", "Account");
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
                await userManager.AddClaimAsync(user, new Claim("Claim.Patient", "Patient"));
                if (result.Succeeded)
                {
                    return await LoginAsync(new LoginModel(registerModel.Email, registerModel.Password));
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { area = "Patient" });
                }
            } else
            {
                return View();
            }
            
        }

        [Authorize]
        public async Task<IActionResult> LogOutAsync()
        {
            Response.Cookies.Delete("apiToken");
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Account", "Treator");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}