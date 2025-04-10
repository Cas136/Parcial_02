using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parcial.Models;

namespace Parcial.Controllers
{
    public class MeserosController : Controller
    {
        private readonly restauranteDbContext _context;

        public MeserosController(restauranteDbContext context)
        {
            _context = context;
        }

        // GET: Meseros
        public async Task<IActionResult> Index()
        {
            return View(await _context.meseros.ToListAsync());
        }

        // GET: Meseros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meseros = await _context.meseros
                .FirstOrDefaultAsync(m => m.id_mesero == id);
            if (meseros == null)
            {
                return NotFound();
            }

            return View(meseros);
        }

        // GET: Meseros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Meseros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_mesero,nombre,numero_pedidos,total_ventas")] meseros meseros)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meseros);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(meseros);
        }

        // GET: Meseros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meseros = await _context.meseros.FindAsync(id);
            if (meseros == null)
            {
                return NotFound();
            }
            return View(meseros);
        }

        // POST: Meseros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_mesero,nombre,numero_pedidos,total_ventas")] meseros meseros)
        {
            if (id != meseros.id_mesero)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meseros);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!meserosExists(meseros.id_mesero))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(meseros);
        }

        // GET: Meseros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meseros = await _context.meseros
                .FirstOrDefaultAsync(m => m.id_mesero == id);
            if (meseros == null)
            {
                return NotFound();
            }

            return View(meseros);
        }

        // POST: Meseros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meseros = await _context.meseros.FindAsync(id);
            if (meseros != null)
            {
                _context.meseros.Remove(meseros);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool meserosExists(int id)
        {
            return _context.meseros.Any(e => e.id_mesero == id);
        }
    }
}
