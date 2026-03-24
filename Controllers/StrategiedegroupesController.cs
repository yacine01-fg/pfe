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
    public class StrategiedegroupesController : Controller
    {
        private readonly PostgresContext _context;

        public StrategiedegroupesController(PostgresContext context)
        {
            _context = context;
        }

        // GET: Strategiedegroupes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Strategiedegroupes.ToListAsync());
        }

        // GET: Strategiedegroupes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var strategiedegroupe = await _context.Strategiedegroupes
                .FirstOrDefaultAsync(m => m.Idgroupe == id);
            if (strategiedegroupe == null)
            {
                return NotFound();
            }

            return View(strategiedegroupe);
        }

        // GET: Strategiedegroupes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Strategiedegroupes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idgroupe,Nomgroupe,Seuilentree,Seuilsortie")] Strategiedegroupe strategiedegroupe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(strategiedegroupe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(strategiedegroupe);
        }

        // GET: Strategiedegroupes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var strategiedegroupe = await _context.Strategiedegroupes.FindAsync(id);
            if (strategiedegroupe == null)
            {
                return NotFound();
            }
            return View(strategiedegroupe);
        }

        // POST: Strategiedegroupes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idgroupe,Nomgroupe,Seuilentree,Seuilsortie")] Strategiedegroupe strategiedegroupe)
        {
            if (id != strategiedegroupe.Idgroupe)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(strategiedegroupe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StrategiedegroupeExists(strategiedegroupe.Idgroupe))
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
            return View(strategiedegroupe);
        }

        // GET: Strategiedegroupes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var strategiedegroupe = await _context.Strategiedegroupes
                .FirstOrDefaultAsync(m => m.Idgroupe == id);
            if (strategiedegroupe == null)
            {
                return NotFound();
            }

            return View(strategiedegroupe);
        }

        // POST: Strategiedegroupes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var strategiedegroupe = await _context.Strategiedegroupes.FindAsync(id);
            if (strategiedegroupe != null)
            {
                _context.Strategiedegroupes.Remove(strategiedegroupe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StrategiedegroupeExists(int id)
        {
            return _context.Strategiedegroupes.Any(e => e.Idgroupe == id);
        }
    }
}
