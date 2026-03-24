using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PFE.Models;

namespace PFE.Controllers
{
    public class PeripheriquesController : Controller
    {
        private readonly PostgresContext _context;

        public PeripheriquesController(PostgresContext context)
        {
            _context = context;
        }

        // GET: Peripheriques
        public async Task<IActionResult> Index()
        {
            return View(await _context.Peripheriques.ToListAsync());
        }

        // GET: Peripheriques/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peripherique = await _context.Peripheriques
                .FirstOrDefaultAsync(m => m.Idperi == id);
            if (peripherique == null)
            {
                return NotFound();
            }

            return View(peripherique);
        }

        // GET: Peripheriques/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Peripheriques/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idperi,Nomperi,Adresseip")] Peripherique peripherique)
        {
            if (ModelState.IsValid)
            {
                _context.Add(peripherique);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(peripherique);
        }

        // GET: Peripheriques/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peripherique = await _context.Peripheriques.FindAsync(id);
            if (peripherique == null)
            {
                return NotFound();
            }
            return View(peripherique);
        }

        // POST: Peripheriques/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idperi,Nomperi,Adresseip")] Peripherique peripherique)
        {
            if (id != peripherique.Idperi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(peripherique);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeripheriqueExists(peripherique.Idperi))
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
            return View(peripherique);
        }

        // GET: Peripheriques/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peripherique = await _context.Peripheriques
                .FirstOrDefaultAsync(m => m.Idperi == id);
            if (peripherique == null)
            {
                return NotFound();
            }

            return View(peripherique);
        }

        // POST: Peripheriques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var peripherique = await _context.Peripheriques.FindAsync(id);
            if (peripherique != null)
            {
                _context.Peripheriques.Remove(peripherique);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeripheriqueExists(int id)
        {
            return _context.Peripheriques.Any(e => e.Idperi == id);
        }
    }
}
