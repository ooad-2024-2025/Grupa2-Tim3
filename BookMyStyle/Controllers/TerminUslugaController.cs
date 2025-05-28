using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookMyStyle.Data;
using BookMyStyle.Models;

namespace BookMyStyle.Controllers
{
    public class TerminUslugaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TerminUslugaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TerminUsluga
        public async Task<IActionResult> Index()
        {
            return View(await _context.TerminUsluga.ToListAsync());
        }

        // GET: TerminUsluga/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terminUsluga = await _context.TerminUsluga
                .FirstOrDefaultAsync(m => m.terminuslugaId == id);
            if (terminUsluga == null)
            {
                return NotFound();
            }

            return View(terminUsluga);
        }

        // GET: TerminUsluga/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TerminUsluga/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("terminuslugaId,terminID,uslugaID")] TerminUsluga terminUsluga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(terminUsluga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(terminUsluga);
        }

        // GET: TerminUsluga/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terminUsluga = await _context.TerminUsluga.FindAsync(id);
            if (terminUsluga == null)
            {
                return NotFound();
            }
            return View(terminUsluga);
        }

        // POST: TerminUsluga/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("terminuslugaId,terminID,uslugaID")] TerminUsluga terminUsluga)
        {
            if (id != terminUsluga.terminuslugaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(terminUsluga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TerminUslugaExists(terminUsluga.terminuslugaId))
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
            return View(terminUsluga);
        }

        // GET: TerminUsluga/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terminUsluga = await _context.TerminUsluga
                .FirstOrDefaultAsync(m => m.terminuslugaId == id);
            if (terminUsluga == null)
            {
                return NotFound();
            }

            return View(terminUsluga);
        }

        // POST: TerminUsluga/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var terminUsluga = await _context.TerminUsluga.FindAsync(id);
            if (terminUsluga != null)
            {
                _context.TerminUsluga.Remove(terminUsluga);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TerminUslugaExists(int id)
        {
            return _context.TerminUsluga.Any(e => e.terminuslugaId == id);
        }
    }
}
