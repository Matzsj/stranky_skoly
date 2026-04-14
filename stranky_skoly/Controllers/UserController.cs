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

        [HttpPost]
        public IActionResult Přihlášení(string jmeno, string heslo)
        {
            // Zde by normálně probíhalo ověření přes databázi.
            // Pro teď uděláme ukázku: Můžete se přihlásit jen jako "admin" s heslem "1234".
            if (jmeno == "admin" && heslo == "1234")
            {
                // Úspěšné přihlášení -> Přesměrujeme uživatele, například na úvodní stránku (Home/Index)
                return RedirectToAction("Index", "Home");
            }

            // Pokud je heslo nebo jméno špatné, přidáme chybu do ModelState z vašeho kódu
            ModelState.AddModelError(string.Empty, "Špatné jméno nebo heslo.");

            // Vrátíme uživatele zpět na přihlašovací formulář, kde se mu následně zobrazí chyba
            return View();
        }

        public IActionResult Registrace()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrace(string jmeno, string heslo, string heslo2)
        {
            // Kontrola, zda se hesla shodují
            if (heslo != heslo2)
            {
                ModelState.AddModelError(string.Empty, "Zadaná hesla se neshodují.");
                return View();
            }

            // Zde by normálně proběhlo uložení uživatele do databáze

            // Po úspěšné registraci přesměrovat třeba na přihlášení
            return RedirectToAction("Přihlášení");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
