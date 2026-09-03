using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using stranky_skoly.Models;
using System.Diagnostics;

namespace stranky_skoly.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Stránku uvidí POUZE Student i Učitel (nepřihlášeného to vyhodí na Login stránku)
        [Authorize(Roles = "Student,Teacher")]
        public IActionResult Rozvrh()
        {
            return View();
        }

        // Stránku vidí POUZE a JEN Učitel, studentovi to zobrazí odepření přístupu
        [Authorize(Roles = "Teacher")]
        public IActionResult VseCoVidiUcitel()
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
