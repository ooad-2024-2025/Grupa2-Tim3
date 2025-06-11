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
           
            return View();
        }

        // POST: Usluga/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Frizer")]
        public async Task<IActionResult> Create(Usluga usluga)
        {
            if (!ModelState.IsValid)
            {
                TempData["Greska"] = "Neispravni podaci. Molimo provjerite formu.";
                return View(usluga);
            }

            var frizerId = _userManager.GetUserId(User);
            var frizer = await _userManager.FindByIdAsync(frizerId);

            if (frizer?.SalonID == null)
            {
                TempData["Greska"] = "Vaš korisnički račun nije povezan s nijednim salonom.";
                return View(usluga);
            }

            usluga.salonID = frizer.SalonID.Value;

            _context.Add(usluga);
            await _context.SaveChangesAsync();

            TempData["Uspjeh"] = "Usluga uspješno dodana.";

            // vraćanje na prethodnu stranicu ako postoji Referer
            var referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
                return Redirect(referer);

            return RedirectToAction("Index", "Usluga");
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
