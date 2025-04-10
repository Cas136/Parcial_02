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
    public class CocinerosController : Controller
    {
        private readonly restauranteDbContext _context;

        public CocinerosController(restauranteDbContext context)
        {
            _context = context;
        }

        // GET: Cocineros
        public async Task<IActionResult> Index()
        {
            return View(await _context.cocineros.ToListAsync());
        }

        // GET: Cocineros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocineros = await _context.cocineros
                .FirstOrDefaultAsync(m => m.id_cocinero == id);
            if (cocineros == null)
            {
                return NotFound();
            }

            return View(cocineros);
        }

        // GET: Cocineros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cocineros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_cocinero,nombre,platos_preparados,comentario")] cocineros cocineros)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cocineros);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cocineros);
        }

        // GET: Cocineros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocineros = await _context.cocineros.FindAsync(id);
            if (cocineros == null)
            {
                return NotFound();
            }
            return View(cocineros);
        }

        // POST: Cocineros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_cocinero,nombre,platos_preparados,comentario")] cocineros cocineros)
        {
            if (id != cocineros.id_cocinero)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cocineros);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!cocinerosExists(cocineros.id_cocinero))
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
            return View(cocineros);
        }

        // GET: Cocineros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocineros = await _context.cocineros
                .FirstOrDefaultAsync(m => m.id_cocinero == id);
            if (cocineros == null)
            {
                return NotFound();
            }

            return View(cocineros);
        }

        // POST: Cocineros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cocineros = await _context.cocineros.FindAsync(id);
            if (cocineros != null)
            {
                _context.cocineros.Remove(cocineros);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool cocinerosExists(int id)
        {
            return _context.cocineros.Any(e => e.id_cocinero == id);
        }
    }
}
