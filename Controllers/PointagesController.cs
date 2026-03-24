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
    public class PointagesController : Controller
    {
        private readonly PostgresContext _context;

        public PointagesController(PostgresContext context)
        {
            _context = context;
        }

        // GET: Pointages
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.Pointages.Include(p => p.IdemployeNavigation).Include(p => p.IdperiNavigation);
            return View(await postgresContext.ToListAsync());
        }

        // GET: Pointages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointage = await _context.Pointages
                .Include(p => p.IdemployeNavigation)
                .Include(p => p.IdperiNavigation)
                .FirstOrDefaultAsync(m => m.Idp == id);
            if (pointage == null)
            {
                return NotFound();
            }

            return View(pointage);
        }

        // GET: Pointages/Create
        public IActionResult Create()
        {
            ViewData["Idemploye"] = new SelectList(_context.Employes, "Idemp", "Idemp");
            ViewData["Idperi"] = new SelectList(_context.Peripheriques, "Idperi", "Idperi");
            return View();
        }

        // POST: Pointages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idp,Idemploye,Idperi,Datep,Type")] Pointage pointage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pointage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idemploye"] = new SelectList(_context.Employes, "Idemp", "Idemp", pointage.Idemploye);
            ViewData["Idperi"] = new SelectList(_context.Peripheriques, "Idperi", "Idperi", pointage.Idperi);
            return View(pointage);
        }

        // GET: Pointages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointage = await _context.Pointages.FindAsync(id);
            if (pointage == null)
            {
                return NotFound();
            }
            ViewData["Idemploye"] = new SelectList(_context.Employes, "Idemp", "Idemp", pointage.Idemploye);
            ViewData["Idperi"] = new SelectList(_context.Peripheriques, "Idperi", "Idperi", pointage.Idperi);
            return View(pointage);
        }

        // POST: Pointages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idp,Idemploye,Idperi,Datep,Type")] Pointage pointage)
        {
            if (id != pointage.Idp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pointage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PointageExists(pointage.Idp))
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
            ViewData["Idemploye"] = new SelectList(_context.Employes, "Idemp", "Idemp", pointage.Idemploye);
            ViewData["Idperi"] = new SelectList(_context.Peripheriques, "Idperi", "Idperi", pointage.Idperi);
            return View(pointage);
        }

        // GET: Pointages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointage = await _context.Pointages
                .Include(p => p.IdemployeNavigation)
                .Include(p => p.IdperiNavigation)
                .FirstOrDefaultAsync(m => m.Idp == id);
            if (pointage == null)
            {
                return NotFound();
            }

            return View(pointage);
        }

        // POST: Pointages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pointage = await _context.Pointages.FindAsync(id);
            if (pointage != null)
            {
                _context.Pointages.Remove(pointage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PointageExists(int id)
        {
            return _context.Pointages.Any(e => e.Idp == id);
        }
    }
}
