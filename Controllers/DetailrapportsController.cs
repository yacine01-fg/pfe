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
    public class DetailrapportsController : Controller
    {
        private readonly PostgresContext _context;

        public DetailrapportsController(PostgresContext context)
        {
            _context = context;
        }

        // GET: Detailrapports
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.Detailrapports.Include(d => d.IdemployeNavigation);
            return View(await postgresContext.ToListAsync());
        }

        // GET: Detailrapports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detailrapport = await _context.Detailrapports
                .Include(d => d.IdemployeNavigation)
                .FirstOrDefaultAsync(m => m.Iddetail == id);
            if (detailrapport == null)
            {
                return NotFound();
            }

            return View(detailrapport);
        }

        // GET: Detailrapports/Create
        public IActionResult Create()
        {
            ViewData["Idemploye"] = new SelectList(_context.Employes, "Idemp", "Idemp");
            return View();
        }

        // POST: Detailrapports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Iddetail,Idemploye,Datedetail,Typedetail")] Detailrapport detailrapport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detailrapport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idemploye"] = new SelectList(_context.Employes, "Idemp", "Idemp", detailrapport.Idemploye);
            return View(detailrapport);
        }

        // GET: Detailrapports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detailrapport = await _context.Detailrapports.FindAsync(id);
            if (detailrapport == null)
            {
                return NotFound();
            }
            ViewData["Idemploye"] = new SelectList(_context.Employes, "Idemp", "Idemp", detailrapport.Idemploye);
            return View(detailrapport);
        }

        // POST: Detailrapports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Iddetail,Idemploye,Datedetail,Typedetail")] Detailrapport detailrapport)
        {
            if (id != detailrapport.Iddetail)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detailrapport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetailrapportExists(detailrapport.Iddetail))
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
            ViewData["Idemploye"] = new SelectList(_context.Employes, "Idemp", "Idemp", detailrapport.Idemploye);
            return View(detailrapport);
        }

        // GET: Detailrapports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detailrapport = await _context.Detailrapports
                .Include(d => d.IdemployeNavigation)
                .FirstOrDefaultAsync(m => m.Iddetail == id);
            if (detailrapport == null)
            {
                return NotFound();
            }

            return View(detailrapport);
        }

        // POST: Detailrapports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detailrapport = await _context.Detailrapports.FindAsync(id);
            if (detailrapport != null)
            {
                _context.Detailrapports.Remove(detailrapport);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetailrapportExists(int id)
        {
            return _context.Detailrapports.Any(e => e.Iddetail == id);
        }
    }
}
