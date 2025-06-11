using BookMyStyle.Data;
using BookMyStyle.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyStyle.Controllers
{
    public class UslugaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Korisnik> _userManager;

        public UslugaController(ApplicationDbContext context, UserManager<Korisnik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Administrator")]
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


        // GET: Usluga/Create
        [Authorize(Roles = "Administrator, Frizer")]
        public async Task<IActionResult> Create()
        {
            var korisnik = await _userManager.GetUserAsync(User);

            if (User.IsInRole("Administrator"))
            {
                ViewBag.SalonID = new SelectList(_context.Salon, "salonID", "Naziv");
            }
            else if (korisnik?.SalonID != null)
            {
                ViewBag.FrizerovSalonID = korisnik.SalonID;
            }

            return View();
        }




        // POST: Usluga/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Frizer")]
        public async Task<IActionResult> Create([Bind("Cijena,Naziv,Popust,Opis,Trajanje,Tip,salonID")] Usluga usluga)
        {
            var korisnik = await _userManager.GetUserAsync(User);
            var salon = await _context.Salon.FirstOrDefaultAsync(s => s.salonID == korisnik.SalonID);

            // Oslobađamo se validacije za navigacijsku properti
            ModelState.Remove("Salon");

            if (User.IsInRole("Frizer") && korisnik?.SalonID != null)
            {
                usluga.salonID = salon.salonID;
            }

            if (User.IsInRole("Administrator") && usluga.salonID == 0)
            {
                ModelState.AddModelError("salonID", "Salon je obavezan.");
            }

            if (!ModelState.IsValid)
            {
                if (User.IsInRole("Administrator"))
                {
                    ViewBag.SalonID = new SelectList(_context.Salon, "salonID", "Naziv", usluga.salonID);
                }
                else
                {
                    ViewBag.FrizerovSalonID = korisnik?.SalonID;
                }

                return View(usluga);
            }

            _context.Usluga.Add(usluga);
            await _context.SaveChangesAsync();

            TempData["Uspjeh"] = "Usluga uspješno dodana.";
            return RedirectToAction("Details", "Salon", new { id = usluga.salonID });
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
