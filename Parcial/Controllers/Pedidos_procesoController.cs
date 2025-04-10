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
    public class Pedidos_procesoController : Controller
    {
        private readonly restauranteDbContext _context;

        public Pedidos_procesoController(restauranteDbContext context)
        {
            _context = context;
        }

        // GET: Pedidos_proceso
        public async Task<IActionResult> Index()
        {
            return View(await _context.pedidos_proceso.ToListAsync());
        }

        // GET: Pedidos_proceso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidos_proceso = await _context.pedidos_proceso
                .FirstOrDefaultAsync(m => m.id_pedido == id);
            if (pedidos_proceso == null)
            {
                return NotFound();
            }

            return View(pedidos_proceso);
        }

        // GET: Pedidos_proceso/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pedidos_proceso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_pedido,cliente,mesa_numero,mesero,estado,fecha")] pedidos_proceso pedidos_proceso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedidos_proceso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pedidos_proceso);
        }

        // GET: Pedidos_proceso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidos_proceso = await _context.pedidos_proceso.FindAsync(id);
            if (pedidos_proceso == null)
            {
                return NotFound();
            }
            return View(pedidos_proceso);
        }

        // POST: Pedidos_proceso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_pedido,cliente,mesa_numero,mesero,estado,fecha")] pedidos_proceso pedidos_proceso)
        {
            if (id != pedidos_proceso.id_pedido)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedidos_proceso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!pedidos_procesoExists(pedidos_proceso.id_pedido))
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
            return View(pedidos_proceso);
        }

        // GET: Pedidos_proceso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidos_proceso = await _context.pedidos_proceso
                .FirstOrDefaultAsync(m => m.id_pedido == id);
            if (pedidos_proceso == null)
            {
                return NotFound();
            }

            return View(pedidos_proceso);
        }

        // POST: Pedidos_proceso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedidos_proceso = await _context.pedidos_proceso.FindAsync(id);
            if (pedidos_proceso != null)
            {
                _context.pedidos_proceso.Remove(pedidos_proceso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool pedidos_procesoExists(int id)
        {
            return _context.pedidos_proceso.Any(e => e.id_pedido == id);
        }
    }
}
