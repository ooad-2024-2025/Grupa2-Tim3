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
    public class KorisnikSalonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KorisnikSalonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KorisnikSalon
        public async Task<IActionResult> Index()
        {
            return View(await _context.KorisnikSalon.ToListAsync());
        }

        // GET: KorisnikSalon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnikSalon = await _context.KorisnikSalon
                .FirstOrDefaultAsync(m => m.korisniksalonId == id);
            if (korisnikSalon == null)
            {
                return NotFound();
            }

            return View(korisnikSalon);
        }

        // GET: KorisnikSalon/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KorisnikSalon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("korisniksalonId,salonID,korisnikID")] KorisnikSalon korisnikSalon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(korisnikSalon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(korisnikSalon);
        }

        // GET: KorisnikSalon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnikSalon = await _context.KorisnikSalon.FindAsync(id);
            if (korisnikSalon == null)
            {
                return NotFound();
            }
            return View(korisnikSalon);
        }

        // POST: KorisnikSalon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("korisniksalonId,salonID,korisnikID")] KorisnikSalon korisnikSalon)
        {
            if (id != korisnikSalon.korisniksalonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(korisnikSalon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KorisnikSalonExists(korisnikSalon.korisniksalonId))
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
            return View(korisnikSalon);
        }

        // GET: KorisnikSalon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnikSalon = await _context.KorisnikSalon
                .FirstOrDefaultAsync(m => m.korisniksalonId == id);
            if (korisnikSalon == null)
            {
                return NotFound();
            }

            return View(korisnikSalon);
        }

        // POST: KorisnikSalon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var korisnikSalon = await _context.KorisnikSalon.FindAsync(id);
            if (korisnikSalon != null)
            {
                _context.KorisnikSalon.Remove(korisnikSalon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KorisnikSalonExists(int id)
        {
            return _context.KorisnikSalon.Any(e => e.korisniksalonId == id);
        }
    }
}
