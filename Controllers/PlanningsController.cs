using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFE.Models;

namespace PFE.Controllers
{
    public class PlanningsController : Controller
    {
        private readonly PostgresContext _context;

        public PlanningsController(PostgresContext context)
        {
            _context = context;
        }

        // GET: Plannings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Plannings.AsNoTracking().ToListAsync());
        }

        // GET: Plannings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var planning = await _context.Plannings
                .FirstOrDefaultAsync(m => m.Idplanning == id);

            if (planning == null) return NotFound();

            return View(planning);
        }

        // GET: Plannings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Plannings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nomplanning,Horairedebut,Horairefin")] Planning planning)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Ma dirlhomch conversion l' DateTime UTC psq rahoum TimeOnly
                    _context.Add(planning);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                // Hna rayah y-banlek l'mouchkil ida bqa m3a la base
                var inner = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Content("Srat erreur f'l'ajout: " + inner);
            }
            return View(planning);
        }

        // GET: Plannings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var planning = await _context.Plannings.FindAsync(id);
            if (planning == null) return NotFound();

            return View(planning);
        }

        // POST: Plannings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idplanning,Nomplanning,Horairedebut,Horairefin")] Planning planning)
        {
            if (id != planning.Idplanning) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planning);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanningExists(planning.Idplanning)) return NotFound();
                    else throw;
                }
                catch (Exception ex)
                {
                    var inner = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    return Content("Srat erreur f'l'update: " + inner);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(planning);
        }

        // GET: Plannings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var planning = await _context.Plannings
                .FirstOrDefaultAsync(m => m.Idplanning == id);

            if (planning == null) return NotFound();

            return View(planning);
        }

        // POST: Plannings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planning = await _context.Plannings.FindAsync(id);
            if (planning != null)
            {
                _context.Plannings.Remove(planning);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanningExists(int id)
        {
            return _context.Plannings.Any(e => e.Idplanning == id);
        }
    }
}