using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial.Models;
using System.Threading.Tasks;

namespace Parcial.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly restauranteDbContext _context;

        public UsuariosController(restauranteDbContext context)
        {
            _context = context;
        }

        // GET: Vista de Login
        public IActionResult Index()
        {
            // Establecemos un ViewData para ocultar el navbar
            ViewData["HideNavbar"] = true;
            return View();
        }

        // POST: Procesar Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string gmail, string contrasena)
        {
            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(u => u.gmail == gmail && u.contrasena == contrasena);

            if (usuario != null)
            {
                // Redirigir al home después de login exitoso
                return RedirectToAction("Index", "Home");
            }

            // Mantener el navbar oculto en caso de error
            ViewData["HideNavbar"] = true;
            ViewBag.ErrorMessage = "Credenciales incorrectas. Por favor intente nuevamente.";
            return View("Index");
        }
    }
}