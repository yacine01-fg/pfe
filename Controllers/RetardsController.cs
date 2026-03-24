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
    public class RetardsController : Controller
    {
        private readonly PostgresContext _context;

        public RetardsController(PostgresContext context)
        {
            _context = context;
        }

        // GET: Retards
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.Retards.Include(r => r.IddetailNavigation);
            return View(await postgresContext.ToListAsync());
        }

        // GET: Retards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var retard = await _context.Retards
                .Include(r => r.IddetailNavigation)
                .FirstOrDefaultAsync(m => m.Idretard == id);
            if (retard == null)
            {
                return NotFound();
            }

            return View(retard);
        }

        // GET: Retards/Create
        public IActionResult Create()
        {
            ViewData["Iddetail"] = new SelectList(_context.Detailrapports, "Iddetail", "Iddetail");
            return View();
        }

        // POST: Retards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idretard,Iddetail,Dureer")] Retard retard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(retard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Iddetail"] = new SelectList(_context.Detailrapports, "Iddetail", "Iddetail", retard.Iddetail);
            return View(retard);
        }

        // GET: Retards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var retard = await _context.Retards.FindAsync(id);
            if (retard == null)
            {
                return NotFound();
            }
            ViewData["Iddetail"] = new SelectList(_context.Detailrapports, "Iddetail", "Iddetail", retard.Iddetail);
            return View(retard);
        }

        // POST: Retards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idretard,Iddetail,Dureer")] Retard retard)
        {
            if (id != retard.Idretard)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(retard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RetardExists(retard.Idretard))
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
            ViewData["Iddetail"] = new SelectList(_context.Detailrapports, "Iddetail", "Iddetail", retard.Iddetail);
            return View(retard);
        }

        // GET: Retards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var retard = await _context.Retards
                .Include(r => r.IddetailNavigation)
                .FirstOrDefaultAsync(m => m.Idretard == id);
            if (retard == null)
            {
                return NotFound();
            }

            return View(retard);
        }

        // POST: Retards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var retard = await _context.Retards.FindAsync(id);
            if (retard != null)
            {
                _context.Retards.Remove(retard);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RetardExists(int id)
        {
            return _context.Retards.Any(e => e.Idretard == id);
        }
    }
}
