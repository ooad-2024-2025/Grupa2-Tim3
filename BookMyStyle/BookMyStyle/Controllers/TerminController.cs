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
    public class TerminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Korisnik> _userManager;

        public TerminController(ApplicationDbContext context,UserManager<Korisnik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Termin
        public async Task<IActionResult> Index()
        {
            ViewBag.Usluge = await _context.Usluga.ToListAsync();

            return View(await _context.Termin
                .Where(t => t.KorisnikID == null).ToListAsync());
        }

        // GET: Termin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termin = await _context.Termin
                .FirstOrDefaultAsync(m => m.terminID == id);
            if (termin == null)
            {
                return NotFound();
            }

            return View(termin);
        }

        // GET: Termin/Create
        [Authorize(Roles = "Administrator, Frizer")]
        public async Task<IActionResult> Create()
        {
            var frizerId = _userManager.GetUserId(User);
            var frizer = await _userManager.FindByIdAsync(frizerId);

            if (frizer?.SalonID == null)
            {
                TempData["Error"] = "Frizer nije povezan s nijednim salonom.";
                return RedirectToAction("Index");
            }

            var usluge = await _context.Usluga
                .Where(u => u.salonID == frizer.SalonID)
                .Select(u => new SelectListItem
                {
                    Value = u.uslugaID.ToString(),
                    Text = $"{u.Naziv} – {u.Cijena} KM – {u.Trajanje} min"
                })
                .ToListAsync();

            ViewBag.Usluge = usluge;
            return View();
        }

        // POST: Termin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Frizer")]
        public async Task<IActionResult> Create([Bind("DatumIVrijeme,uslugaID")] Termin termin)
        {
            var frizerId = _userManager.GetUserId(User);
            var frizer = await _userManager.FindByIdAsync(frizerId);

            if (frizer?.SalonID == null)
            {
                TempData["Error"] = "Frizer nije povezan s nijednim salonom.";
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                termin.FrizerID = frizerId;

                var salon = await _context.Salon.FirstOrDefaultAsync(s => s.salonID == frizer.SalonID);
                if (salon != null)
                {
                    termin.salonID = salon.salonID;
                    termin.NazivSalona = salon.Naziv;
                    termin.AdresaSalona = salon.Adresa;
                }

                termin.NazivFrizera = $"{frizer.Ime} {frizer.Prezime}";

                _context.Add(termin);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Termin uspješno kreiran!";
                return RedirectToAction(nameof(Index));
            }

            // Ako validacija ne prođe, ponovo popuni listu usluga
            var usluge = await _context.Usluga
                .Where(u => u.salonID == frizer.SalonID)
                .Select(u => new SelectListItem
                {
                    Value = u.uslugaID.ToString(),
                    Text = $"{u.Naziv} – {u.Cijena} KM – {u.Trajanje} min"
                })
                .ToListAsync();

            ViewBag.Usluge = usluge;
            return View(termin);
        }







        // GET: Termin/Edit/5
        [Authorize(Roles = "Administrator, Frizer")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termin = await _context.Termin.FindAsync(id);
            if (termin == null)
            {
                return NotFound();
            }

            var frizerId = _userManager.GetUserId(User);
            if (User.IsInRole("Frizer") && termin.FrizerID != frizerId)
                return Forbid();

            return View(termin);
        }

        // POST: Termin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Termin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Frizer")]
        public async Task<IActionResult> Edit(int id, Termin izForme)
        {
            if (id != izForme.terminID)
                return NotFound();

            var frizerId = _userManager.GetUserId(User);

            // Dohvati originalni termin iz baze
            var termin = await _context.Termin.FirstOrDefaultAsync(t => t.terminID == id);
            if (termin == null)
                return NotFound();

            // Ako frizer pokušava uređivati tuđi termin
            if (User.IsInRole("Frizer") && termin.FrizerID != frizerId)
                return Forbid();

            // Samo ažuriraj dozvoljena polja
            if (ModelState.IsValid)
            {
                termin.NazivSalona = izForme.NazivSalona;
                termin.AdresaSalona = izForme.AdresaSalona;
                termin.NazivFrizera = izForme.NazivFrizera;
                termin.DatumIVrijeme = izForme.DatumIVrijeme;

                try
                {
                    _context.Update(termin);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Termin uspješno izmijenjen.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TerminExists(termin.terminID))
                        return NotFound();
                    else
                        throw;
                }
            }

            return View(izForme);
        }



        // GET: Termin/Delete/5
        [Authorize(Roles = "Administrator, Frizer")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var termin = await _context.Termin.FirstOrDefaultAsync(m => m.terminID == id);
            if (termin == null)
                return NotFound();

            var frizerId = _userManager.GetUserId(User);
            if (User.IsInRole("Frizer") && termin.FrizerID != frizerId)
                return Forbid(); // Ili RedirectToAction("Index")

            return View(termin);
        }


        // POST: Termin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Frizer")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var termin = await _context.Termin.FindAsync(id);
            var frizerId = _userManager.GetUserId(User);

            if (termin == null)
                return NotFound();

            if (User.IsInRole("Frizer") && termin.FrizerID != frizerId)
                return Forbid(); // Ili RedirectToAction("Index")

            _context.Termin.Remove(termin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Zakazivanje termina
        [HttpPost]
        [Authorize(Roles = "Administrator, Korisnik")]
        public async Task<IActionResult> Zakazi(int id)
        {
            var korisnikId = _userManager.GetUserId(User);

            var termin = await _context.Termin.FirstOrDefaultAsync(t => t.terminID == id && t.KorisnikID == null);
            if (termin == null)
            {
                return NotFound();
            }

            // ➕ Popuni korisnika koji zakazuje
            termin.KorisnikID = korisnikId;

            // ➕ Kreiraj obavijest povezanu sa tim terminom
            var obavijest = new Obavijest
            {
                Tekst = $"Zakazali ste termin kod frizera {termin.NazivFrizera} u salonu {termin.NazivSalona}, " +
                        $"na adresi {termin.AdresaSalona}, dana {termin.DatumIVrijeme:dd.MM.yyyy. u HH:mm}.",
                DatumIVrijeme = termin.DatumIVrijeme,
                terminID = termin.terminID
            };

            _context.Update(termin);
            _context.Obavijest.Add(obavijest);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Termin je uspješno zakazan i obavijest je kreirana.";
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Korisnik")]
        public async Task<IActionResult> Otkazi(int id)
        {
            var korisnikId = _userManager.GetUserId(User);

            var termin = await _context.Termin
                .FirstOrDefaultAsync(t => t.terminID == id && t.KorisnikID == korisnikId);

            if (termin == null)
                return NotFound();

            return View(termin); // Ovo prikazuje Otkazi.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Korisnik")]
        public async Task<IActionResult> OtkaziPotvrda(int terminID)
        {
            var korisnikId = _userManager.GetUserId(User);

            var termin = await _context.Termin
                .FirstOrDefaultAsync(t => t.terminID == terminID && t.KorisnikID == korisnikId);

            if (termin == null)
                return NotFound();

            termin.KorisnikID = null;

            var obavijest = await _context.Obavijest.FirstOrDefaultAsync(o => o.terminID == terminID);
            if (obavijest != null)
                _context.Obavijest.Remove(obavijest);

            _context.Update(termin);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Termin je uspješno otkazan.";
            return RedirectToAction("Index", "Obavijest");
        }




        private bool TerminExists(int id)
        {
            return _context.Termin.Any(e => e.terminID == id);
        }
    }
}
