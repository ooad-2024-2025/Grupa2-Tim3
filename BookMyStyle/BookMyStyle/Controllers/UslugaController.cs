using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookMyStyle.Data;
using BookMyStyle.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookMyStyle.Controllers
{
    public class UslugaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UslugaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Administrator, Korisnik, Frizer")]
        // GET: Usluga
        public async Task<IActionResult> Index()
        {
            // Možete u budućnosti uključiti i .Include(s => s.Salon) ako želite prikazati naziv salona uz uslugu
            var usluge = await _context.Usluga.Include(u => u.Salon).ToListAsync();
            return View(usluge);
        }

        [Authorize(Roles = "Administrator, Korisnik, Frizer")]
        // GET: Usluga/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usluga = await _context.Usluga
                .Include(u => u.Salon) // da vidite i naziv salona u Details viewu
                .FirstOrDefaultAsync(m => m.uslugaID == id);
            if (usluga == null)
            {
                return NotFound();
            }

            return View(usluga);
        }

        [Authorize(Roles = "Administrator, Frizer")]
        // GET: Usluga/Create
        public IActionResult Create(int? salonID)
        {
            if (salonID != null)
            {
                // Ako URL dolazi s ?salonID=3, onda DropDown bude samo taj salon i unaprijed odabran
                ViewBag.SalonID = new SelectList(
                    _context.Salon.Where(s => s.salonID == salonID),
                    "salonID",
                    "Naziv",
                    salonID);
            }
            else
            {
                // Inače drop‐down popunimo svim salonima
                ViewBag.SalonID = new SelectList(_context.Salon, "salonID", "Naziv");
            }
            return View();
        }

        // POST: Usluga/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Frizer")]
        public async Task<IActionResult> Create([Bind("uslugaID,Cijena,Naziv,Popust,Opis,Trajanje,Tip,salonID")] Usluga usluga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usluga);
                await _context.SaveChangesAsync();

                // Opcionalno: nakon kreiranja, preusmjerite natrag na Details salona
                return RedirectToAction("Details", "Salon", new { id = usluga.salonID });
            }

            // Ako validacija ne prođe, ponovno popunimo DropDown listu salona (da oporavak forme ne bi pao)
            ViewBag.SalonID = new SelectList(_context.Salon, "salonID", "Naziv", usluga.salonID);
            return View(usluga);
        }

        [Authorize(Roles = "Administrator, Frizer")]
        // GET: Usluga/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usluga = await _context.Usluga.FindAsync(id);
            if (usluga == null)
            {
                return NotFound();
            }

            // Prilikom Edit prikaza također želimo DropDown svih salona s aktivnim odabirom
            ViewBag.SalonID = new SelectList(_context.Salon, "salonID", "Naziv", usluga.salonID);
            return View(usluga);
        }

        // POST: Usluga/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Frizer")]
        public async Task<IActionResult> Edit(int id, [Bind("uslugaID,Cijena,Naziv,Popust,Opis,Trajanje,Tip,salonID")] Usluga usluga)
        {
            if (id != usluga.uslugaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usluga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UslugaExists(usluga.uslugaID))
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
            // Ako validacija ne prođe, ponovo popunimo DropDown listu
            ViewBag.SalonID = new SelectList(_context.Salon, "salonID", "Naziv", usluga.salonID);
            return View(usluga);
        }

        [Authorize(Roles = "Administrator, Frizer")]
        // GET: Usluga/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usluga = await _context.Usluga
                .Include(u => u.Salon)
                .FirstOrDefaultAsync(m => m.uslugaID == id);
            if (usluga == null)
            {
                return NotFound();
            }

            return View(usluga);
        }

        // POST: Usluga/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Frizer")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usluga = await _context.Usluga.FindAsync(id);
            if (usluga != null)
            {
                _context.Usluga.Remove(usluga);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool UslugaExists(int id)
        {
            return _context.Usluga.Any(e => e.uslugaID == id);
        }
    }
}
