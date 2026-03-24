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
    public class SortieanticipeesController : Controller
    {
        private readonly PostgresContext _context;

        public SortieanticipeesController(PostgresContext context)
        {
            _context = context;
        }

        // GET: Sortieanticipees
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.Sortieanticipees.Include(s => s.IddetailNavigation);
            return View(await postgresContext.ToListAsync());
        }

        // GET: Sortieanticipees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sortieanticipee = await _context.Sortieanticipees
                .Include(s => s.IddetailNavigation)
                .FirstOrDefaultAsync(m => m.Idsortie == id);
            if (sortieanticipee == null)
            {
                return NotFound();
            }

            return View(sortieanticipee);
        }

        // GET: Sortieanticipees/Create
        public IActionResult Create()
        {
            ViewData["Iddetail"] = new SelectList(_context.Detailrapports, "Iddetail", "Iddetail");
            return View();
        }

        // POST: Sortieanticipees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idsortie,Iddetail,Durees")] Sortieanticipee sortieanticipee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sortieanticipee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Iddetail"] = new SelectList(_context.Detailrapports, "Iddetail", "Iddetail", sortieanticipee.Iddetail);
            return View(sortieanticipee);
        }

        // GET: Sortieanticipees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sortieanticipee = await _context.Sortieanticipees.FindAsync(id);
            if (sortieanticipee == null)
            {
                return NotFound();
            }
            ViewData["Iddetail"] = new SelectList(_context.Detailrapports, "Iddetail", "Iddetail", sortieanticipee.Iddetail);
            return View(sortieanticipee);
        }

        // POST: Sortieanticipees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idsortie,Iddetail,Durees")] Sortieanticipee sortieanticipee)
        {
            if (id != sortieanticipee.Idsortie)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sortieanticipee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SortieanticipeeExists(sortieanticipee.Idsortie))
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
            ViewData["Iddetail"] = new SelectList(_context.Detailrapports, "Iddetail", "Iddetail", sortieanticipee.Iddetail);
            return View(sortieanticipee);
        }

        // GET: Sortieanticipees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sortieanticipee = await _context.Sortieanticipees
                .Include(s => s.IddetailNavigation)
                .FirstOrDefaultAsync(m => m.Idsortie == id);
            if (sortieanticipee == null)
            {
                return NotFound();
            }

            return View(sortieanticipee);
        }

        // POST: Sortieanticipees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sortieanticipee = await _context.Sortieanticipees.FindAsync(id);
            if (sortieanticipee != null)
            {
                _context.Sortieanticipees.Remove(sortieanticipee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SortieanticipeeExists(int id)
        {
            return _context.Sortieanticipees.Any(e => e.Idsortie == id);
        }
    }
}
