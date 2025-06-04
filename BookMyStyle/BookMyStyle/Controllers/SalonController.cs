using BookMyStyle.Data;
using BookMyStyle.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyStyle.Controllers
{
    public class SalonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalonController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Administrator, Korisnik, Frizer")]
        // GET: Salon
        public async Task<IActionResult> Index()
        {
            return View(await _context.Salon.ToListAsync());
        }

        [Authorize(Roles = "Administrator, Korisnik, Frizer")]
        // GET: Salon/Details/5
        [Authorize(Roles = "Administrator, Korisnik, Frizer")]
        // GET: Salon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            // Uključujemo i kolekciju Usluga i kolekciju Termin
            var salon = await _context.Salon
                .Include(s => s.Usluga)
                .Include(s => s.Termin)
                .FirstOrDefaultAsync(m => m.salonID == id);

            if (salon == null)
                return NotFound();

            return View(salon);
        }

        [Authorize(Roles = "Administrator, Frizer")]
        // GET: Salon/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Salon/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Frizer")]
        public async Task<IActionResult> Create([Bind("salonID,Naziv,Adresa,RadnoVrijeme")] Salon salon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salon);
        }

        [Authorize(Roles = "Administrator, Frizer")]
        // GET: Salon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salon = await _context.Salon.FindAsync(id);
            if (salon == null)
            {
                return NotFound();
            }
            return View(salon);
        }

        // POST: Salon/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Frizer")]
        public async Task<IActionResult> Edit(int id, [Bind("salonID,Naziv,Adresa,RadnoVrijeme")] Salon salon)
        {
            if (id != salon.salonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalonExists(salon.salonID))
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
            return View(salon);
        }

        [Authorize(Roles = "Administrator, Frizer")]
        // GET: Salon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salon = await _context.Salon
                .FirstOrDefaultAsync(m => m.salonID == id);
            if (salon == null)
            {
                return NotFound();
            }

            return View(salon);
        }

        // POST: Salon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Frizer")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salon = await _context.Salon.FindAsync(id);
            if (salon != null)
            {
                _context.Salon.Remove(salon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalonExists(int id)
        {
            return _context.Salon.Any(e => e.salonID == id);
        }
    }
}
