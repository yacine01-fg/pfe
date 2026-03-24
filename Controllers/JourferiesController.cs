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
    public class JourferiesController : Controller
    {
        private readonly PostgresContext _context;

        public JourferiesController(PostgresContext context)
        {
            _context = context;
        }

        // GET: Jourferies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Jourferies.ToListAsync());
        }

        // GET: Jourferies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jourferie = await _context.Jourferies
                .FirstOrDefaultAsync(m => m.Idjf == id);
            if (jourferie == null)
            {
                return NotFound();
            }

            return View(jourferie);
        }

        // GET: Jourferies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jourferies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idjf,Nomjf,Datejf")] Jourferie jourferie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jourferie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jourferie);
        }

        // GET: Jourferies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jourferie = await _context.Jourferies.FindAsync(id);
            if (jourferie == null)
            {
                return NotFound();
            }
            return View(jourferie);
        }

        // POST: Jourferies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idjf,Nomjf,Datejf")] Jourferie jourferie)
        {
            if (id != jourferie.Idjf)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jourferie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JourferieExists(jourferie.Idjf))
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
            return View(jourferie);
        }

        // GET: Jourferies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jourferie = await _context.Jourferies
                .FirstOrDefaultAsync(m => m.Idjf == id);
            if (jourferie == null)
            {
                return NotFound();
            }

            return View(jourferie);
        }

        // POST: Jourferies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jourferie = await _context.Jourferies.FindAsync(id);
            if (jourferie != null)
            {
                _context.Jourferies.Remove(jourferie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JourferieExists(int id)
        {
            return _context.Jourferies.Any(e => e.Idjf == id);
        }
    }
}
