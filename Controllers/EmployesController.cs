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
    public class EmployesController : Controller
    {
        private readonly PostgresContext _context;

        public EmployesController(PostgresContext context)
        {
            _context = context;
        }

        // GET: Employes
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.Employes.Include(e => e.IddeptNavigation).Include(e => e.IdequipeNavigation).Include(e => e.IdgroupeNavigation);
            return View(await postgresContext.ToListAsync());
        }

        // GET: Employes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employe = await _context.Employes
                .Include(e => e.IddeptNavigation)
                .Include(e => e.IdequipeNavigation)
                .Include(e => e.IdgroupeNavigation)
                .FirstOrDefaultAsync(m => m.Idemp == id);
            if (employe == null)
            {
                return NotFound();
            }

            return View(employe);
        }

        // GET: Employes/Create
        public IActionResult Create()
        {
            ViewData["Iddept"] = new SelectList(_context.Departements, "Iddept", "Iddept");
            ViewData["Idequipe"] = new SelectList(_context.Equipes, "Idequipe", "Idequipe");
            ViewData["Idgroupe"] = new SelectList(_context.Strategiedegroupes, "Idgroupe", "Idgroupe");
            return View();
        }

        // POST: Employes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idemp,Nomemp,Prenomemp,Iddept,Idequipe,Idgroupe")] Employe employe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Iddept"] = new SelectList(_context.Departements, "Iddept", "Iddept", employe.Iddept);
            ViewData["Idequipe"] = new SelectList(_context.Equipes, "Idequipe", "Idequipe", employe.Idequipe);
            ViewData["Idgroupe"] = new SelectList(_context.Strategiedegroupes, "Idgroupe", "Idgroupe", employe.Idgroupe);
            return View(employe);
        }

        // GET: Employes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employe = await _context.Employes.FindAsync(id);
            if (employe == null)
            {
                return NotFound();
            }
            ViewData["Iddept"] = new SelectList(_context.Departements, "Iddept", "Iddept", employe.Iddept);
            ViewData["Idequipe"] = new SelectList(_context.Equipes, "Idequipe", "Idequipe", employe.Idequipe);
            ViewData["Idgroupe"] = new SelectList(_context.Strategiedegroupes, "Idgroupe", "Idgroupe", employe.Idgroupe);
            return View(employe);
        }

        // POST: Employes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idemp,Nomemp,Prenomemp,Iddept,Idequipe,Idgroupe")] Employe employe)
        {
            if (id != employe.Idemp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeExists(employe.Idemp))
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
            ViewData["Iddept"] = new SelectList(_context.Departements, "Iddept", "Iddept", employe.Iddept);
            ViewData["Idequipe"] = new SelectList(_context.Equipes, "Idequipe", "Idequipe", employe.Idequipe);
            ViewData["Idgroupe"] = new SelectList(_context.Strategiedegroupes, "Idgroupe", "Idgroupe", employe.Idgroupe);
            return View(employe);
        }

        // GET: Employes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employe = await _context.Employes
                .Include(e => e.IddeptNavigation)
                .Include(e => e.IdequipeNavigation)
                .Include(e => e.IdgroupeNavigation)
                .FirstOrDefaultAsync(m => m.Idemp == id);
            if (employe == null)
            {
                return NotFound();
            }

            return View(employe);
        }

        // POST: Employes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employe = await _context.Employes.FindAsync(id);
            if (employe != null)
            {
                _context.Employes.Remove(employe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeExists(int id)
        {
            return _context.Employes.Any(e => e.Idemp == id);
        }
    }
}
