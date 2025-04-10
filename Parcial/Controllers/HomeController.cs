using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Parcial.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Parcial.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly restauranteDbContext _context;

        public HomeController(ILogger<HomeController> logger, restauranteDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Obtener datos principales
            var mesas = await _context.mesas.ToListAsync();
            var pedidos = await _context.pedidos_proceso.ToListAsync();

            // Obtener meseros top (5 con más ventas)
            var meseros = await _context.meseros
                .OrderByDescending(m => m.total_ventas)
                .Take(5)
                .ToListAsync();

            // Obtener platos más vendidos (top 5)
            var platos = await _context.platos
                .OrderByDescending(p => p.cantidad_vendida)
                .Take(5)
                .ToListAsync();

            // Obtener cocineros más productivos (top 5 por platos preparados)
            var cocineros = await _context.cocineros
                .OrderByDescending(c => c.platos_preparados)
                .Take(5)
                .ToListAsync();

            // Calcular estadísticas generales
            ViewBag.Pedidos = pedidos;
            ViewBag.Meseros = meseros;
            ViewBag.Platos = platos;
            ViewBag.Cocineros = cocineros;

            ViewBag.TotalMeseros = await _context.meseros.CountAsync();
            ViewBag.TotalCocineros = await _context.cocineros.CountAsync();
            ViewBag.TotalVentas = await _context.meseros.SumAsync(m => m.total_ventas ?? 0);
            ViewBag.TotalPlatos = await _context.platos.CountAsync();
            ViewBag.TotalPlatosPreparados = await _context.cocineros.SumAsync(c => c.platos_preparados);
            ViewBag.TotalMesas = await _context.mesas.CountAsync();

            return View(mesas);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Método opcional para obtener estadísticas via AJAX
        public async Task<IActionResult> GetDashboardStats()
        {
            var stats = new
            {
                totalMeseros = await _context.meseros.CountAsync(),
                totalCocineros = await _context.cocineros.CountAsync(),
                totalVentas = await _context.meseros.SumAsync(m => m.total_ventas ?? 0),
                totalPlatos = await _context.platos.CountAsync(),
                totalPlatosPreparados = await _context.cocineros.SumAsync(c => c.platos_preparados),
                totalMesas = await _context.mesas.CountAsync(),
                topMeseros = await _context.meseros
                    .OrderByDescending(m => m.total_ventas)
                    .Take(5)
                    .Select(m => new { m.nombre, m.total_ventas, m.numero_pedidos })
                    .ToListAsync(),
                topPlatos = await _context.platos
                    .OrderByDescending(p => p.cantidad_vendida)
                    .Take(5)
                    .Select(p => new { p.nombre, p.cantidad_vendida, p.ingresos_generados, p.imagen })
                    .ToListAsync(),
                topCocineros = await _context.cocineros
                    .OrderByDescending(c => c.platos_preparados)
                    .Take(5)
                    .Select(c => new { c.nombre, c.platos_preparados, c.comentario })
                    .ToListAsync(),
                mesasStatus = new
                {
                    libres = await _context.mesas.CountAsync(m => m.estado.ToLower() == "libre" || m.estado.ToLower() == "disponible"),
                    ocupadas = await _context.mesas.CountAsync(m => m.estado.ToLower() == "ocupado" || m.estado.ToLower() == "ocupada"),
                    reservadas = await _context.mesas.CountAsync(m => m.estado.ToLower() == "reservado" || m.estado.ToLower() == "reservada")
                }
            };

            return Json(stats);
        }
    }
}