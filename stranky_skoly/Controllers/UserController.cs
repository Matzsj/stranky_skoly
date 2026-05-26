using Microsoft.AspNetCore.Mvc;
using stranky_skoly.DbContext;
using stranky_skoly.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;



namespace stranky_skoly.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Přihlášení()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Přihlášení(string jmeno, string heslo)
        {
            var user = _context.Users.FirstOrDefault(u => u.Name == jmeno);

            if (user != null)
            {
                var hasher = new PasswordHasher<User>();

                var result = hasher.VerifyHashedPassword(user, user.Password, heslo);

                if (result == PasswordVerificationResult.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Špatné jméno nebo heslo.");
            return View(); // 👈 TO TI CHYBÍ
        
        }

        public IActionResult Registrace()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrace(string jmeno, string heslo, string heslo2)
        {
            var hasher = new PasswordHasher<User>();
            // 1. Kontrola, zda se hesla shodují
            if (heslo != heslo2)
            {
                ModelState.AddModelError(string.Empty, "Zadaná hesla se neshodují.");
                return View();
            }

            // 2. Vytvoření nového uživatele (objektu User)
            var novyUzivatel = new User
            {
                Name = jmeno,
                
            };
            novyUzivatel.Password = hasher.HashPassword(novyUzivatel, heslo);
            // 3. Přidání uživatele do databázového kontextu
            _context.Users.Add(novyUzivatel);

            // 4. Uložení změn do databáze
            _context.SaveChanges();

            // 5. Po úspěšné registraci přesměrovat na přihlášení
            return RedirectToAction("Přihlášení");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
