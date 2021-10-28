using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fysio.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Login", "Account", new { area = "Treator" });
        }

        public IActionResult AccesDenied()
        {
            return RedirectToAction("Login", "Account", new { area = "Treator" });
        }
    }
}
