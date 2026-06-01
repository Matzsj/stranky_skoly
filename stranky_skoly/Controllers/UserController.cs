using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using stranky_skoly.DbContext;
using stranky_skoly.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using System.Linq;



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
        public async Task<IActionResult> Přihlášení(string email, string heslo)
        {
            var user = _context.Users.FirstOrDefault(u => u.Name == email);

            if (user != null)
            {
                var hasher = new PasswordHasher<User>();
                var result = hasher.VerifyHashedPassword(user, user.Password, heslo);

                if (result == PasswordVerificationResult.Success)
                {
                    // Detekce role učitele (pokud jméno neobsahuje číslo)
                    bool isTeacher = !user.Name.Any(char.IsDigit);
                    string userRole = isTeacher ? "Teacher" : "Student";

                    // Vytvoření Identity ("občanky" přihlášeného)
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Role, userRole)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                    // Přihlášení do cookie
                    await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity));

                    // Přesměrování na základě role
                    if (isTeacher)
                    {
                        return RedirectToAction("Učitelé", "User");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            
            // Zde můžete nastavit chybovou hlášku pro špatné jméno nebo heslo
            return View(); 
        
        }



        [Authorize(Roles = "Teacher")]
        public IActionResult Učitelé()
        {
            return View();
        }

        [Authorize(Roles = "Student,Teacher")]
        public IActionResult Rozvrh()
        {
            return View();
        }

        public async Task<IActionResult> Odhlášení()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Registrace()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrace(string email, string heslo, string heslo2)
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
                Name = email
                
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
