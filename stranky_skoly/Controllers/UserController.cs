using Microsoft.AspNetCore.Mvc;
using stranky_skoly.Models;
using System.Diagnostics;

namespace stranky_skoly.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Přihlášení()
        {
            return View();
        }

        public IActionResult Registrace()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
