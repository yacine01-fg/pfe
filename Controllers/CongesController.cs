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
    public class CongesController : Controller
    {
        private readonly PostgresContext _context;

        public CongesController(PostgresContext context)
        {
            _context = context;
        }

        // GET: Conges
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.Conges.Include(c => c.IdemployeNavigation);
            return View(await postgresContext.ToListAsync());
        }

        // GET: Conges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conge = await _context.Conges
                .Include(c => c.IdemployeNavigation)
                .FirstOrDefaultAsync(m => m.Idconge == id);
            if (conge == null)
            {
                return NotFound();
            }

            return View(conge);
        }

        // GET: Conges/Create
        public IActionResult Create()
        {
            ViewData["Idemploye"] = new SelectList(_context.Employes, "Idemp", "Idemp");
            return View();
        }

        // POST: Conges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idconge,Idemploye,Datedebut,Datefin,Typeconge")] Conge conge)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idemploye"] = new SelectList(_context.Employes, "Idemp", "Idemp", conge.Idemploye);
            return View(conge);
        }

        // GET: Conges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conge = await _context.Conges.FindAsync(id);
            if (conge == null)
            {
                return NotFound();
            }
            ViewData["Idemploye"] = new SelectList(_context.Employes, "Idemp", "Idemp", conge.Idemploye);
            return View(conge);
        }

        // POST: Conges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idconge,Idemploye,Datedebut,Datefin,Typeconge")] Conge conge)
        {
            if (id != conge.Idconge)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CongeExists(conge.Idconge))
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
            ViewData["Idemploye"] = new SelectList(_context.Employes, "Idemp", "Idemp", conge.Idemploye);
            return View(conge);
        }

        // GET: Conges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conge = await _context.Conges
                .Include(c => c.IdemployeNavigation)
                .FirstOrDefaultAsync(m => m.Idconge == id);
            if (conge == null)
            {
                return NotFound();
            }

            return View(conge);
        }

        // POST: Conges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conge = await _context.Conges.FindAsync(id);
            if (conge != null)
            {
                _context.Conges.Remove(conge);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CongeExists(int id)
        {
            return _context.Conges.Any(e => e.Idconge == id);
        }
    }
}
