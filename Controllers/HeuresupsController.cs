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
    public class HeuresupsController : Controller
    {
        private readonly PostgresContext _context;

        public HeuresupsController(PostgresContext context)
        {
            _context = context;
        }

        // GET: Heuresups
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.Heuresups.Include(h => h.IddetailNavigation);
            return View(await postgresContext.ToListAsync());
        }

        // GET: Heuresups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heuresup = await _context.Heuresups
                .Include(h => h.IddetailNavigation)
                .FirstOrDefaultAsync(m => m.Idhs == id);
            if (heuresup == null)
            {
                return NotFound();
            }

            return View(heuresup);
        }

        // GET: Heuresups/Create
        public IActionResult Create()
        {
            ViewData["Iddetail"] = new SelectList(_context.Detailrapports, "Iddetail", "Iddetail");
            return View();
        }

        // POST: Heuresups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idhs,Iddetail,Dureehs")] Heuresup heuresup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(heuresup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Iddetail"] = new SelectList(_context.Detailrapports, "Iddetail", "Iddetail", heuresup.Iddetail);
            return View(heuresup);
        }

        // GET: Heuresups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heuresup = await _context.Heuresups.FindAsync(id);
            if (heuresup == null)
            {
                return NotFound();
            }
            ViewData["Iddetail"] = new SelectList(_context.Detailrapports, "Iddetail", "Iddetail", heuresup.Iddetail);
            return View(heuresup);
        }

        // POST: Heuresups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idhs,Iddetail,Dureehs")] Heuresup heuresup)
        {
            if (id != heuresup.Idhs)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(heuresup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeuresupExists(heuresup.Idhs))
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
            ViewData["Iddetail"] = new SelectList(_context.Detailrapports, "Iddetail", "Iddetail", heuresup.Iddetail);
            return View(heuresup);
        }

        // GET: Heuresups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heuresup = await _context.Heuresups
                .Include(h => h.IddetailNavigation)
                .FirstOrDefaultAsync(m => m.Idhs == id);
            if (heuresup == null)
            {
                return NotFound();
            }

            return View(heuresup);
        }

        // POST: Heuresups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var heuresup = await _context.Heuresups.FindAsync(id);
            if (heuresup != null)
            {
                _context.Heuresups.Remove(heuresup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeuresupExists(int id)
        {
            return _context.Heuresups.Any(e => e.Idhs == id);
        }
    }
}
