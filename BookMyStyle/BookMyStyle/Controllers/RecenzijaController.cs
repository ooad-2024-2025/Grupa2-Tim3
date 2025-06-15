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
    public class RecenzijaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Korisnik> _userManager;

        public RecenzijaController(ApplicationDbContext context, UserManager<Korisnik> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        [Authorize(Roles = "Administrator, Korisnik, Frizer")]
        // GET: Recenzija
        public async Task<IActionResult> Index()
        {
            ViewBag.Saloni = await _context.Salon.ToListAsync();
            ViewBag.Korisnici = await _context.Users.ToListAsync();

            var recenzije = await _context.Recenzija
                .Include(r => r.Salon)
                .Include(r => r.Korisnik)
                 .OrderByDescending(r => r.Ocjena)
                .ToListAsync();

            return View(recenzije);
        }

        [Authorize(Roles = "Administrator, Korisnik, Frizer")]
        // GET: Recenzija/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recenzija = await _context.Recenzija
                .FirstOrDefaultAsync(m => m.recenzijaID == id);
            if (recenzija == null)
            {
                return NotFound();
            }

            return View(recenzija);
        }

        [Authorize(Roles = "Administrator, Korisnik")]
        // GET: Recenzija/Create
        public async Task<IActionResult> Create()
        {
            //dropdown lista
            var saloni = await _context.Salon
                .Select(s => new SelectListItem
                {
                    Value = s.salonID.ToString(),
                    Text = s.Naziv
                }).ToListAsync();

            ViewBag.Saloni = saloni;

            return View();
        }

        // POST: Recenzija/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Korisnik")]
        public async Task<IActionResult> Create([Bind("Ocjena,Komentar,SalonID")] Recenzija recenzija)
        {
            var korisnikID = _userManager.GetUserId(User);
            recenzija.KorisnikID = korisnikID;
            recenzija.DatumObjave = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(recenzija);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Recenzija uspješno dodana.";
                return RedirectToAction(nameof(Index));
            }

            // Ako model nije validan, ponovo popuni ViewBag.Saloni
            ViewBag.Saloni = await _context.Salon
                .Select(s => new SelectListItem
                {
                    Value = s.salonID.ToString(),
                    Text = s.Naziv
                }).ToListAsync();

            return View(recenzija); ;
        }



        [Authorize(Roles = "Administrator, Korisnik")]
        // GET: Recenzija/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var recenzija = await _context.Recenzija.FindAsync(id);
            if (recenzija == null)
            {
                return NotFound();
            }

            // Dozvoli samo vlasniku recenzije ili administratoru
            var korisnikId = _userManager.GetUserId(User);
            if (User.IsInRole("Korisnik") && recenzija.KorisnikID != korisnikId)
            {
                return Forbid(); // 403 - korisnik pokušava uređivati tuđu recenziju
            }

            return View(recenzija);

        }

        // POST: Recenzija/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Korisnik")]
        public async Task<IActionResult> Edit(int id, [Bind("recenzijaID,Ocjena,Komentar,SalonID")] Recenzija recenzija)
        {
            if (id != recenzija.recenzijaID)
            {
                return NotFound();
            }

            // Dohvati originalnu recenziju iz baze
            var original = await _context.Recenzija.AsNoTracking().FirstOrDefaultAsync(r => r.recenzijaID == id);
            if (original == null)
            {
                return NotFound();
            }
            // Dozvoli samo vlasniku recenzije ili administratoru
            var korisnikId = _userManager.GetUserId(User);
            if (User.IsInRole("Korisnik") && original.KorisnikID != korisnikId)
            {
                return Forbid(); // 403 - korisnik pokušava urediti tuđu recenziju
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Zadrži korisnikID i DatumObjave iz originala (ne dolaze iz forme!)
                    recenzija.KorisnikID = original.KorisnikID;
                    recenzija.DatumObjave = original.DatumObjave;

                    _context.Update(recenzija);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Recenzija je uspješno ažurirana.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecenzijaExists(recenzija.recenzijaID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            // Ako validacija ne prođe, ponovo popuni salone
            ViewBag.Saloni = await _context.Salon
                .Select(s => new SelectListItem
                {
                    Value = s.salonID.ToString(),
                    Text = s.Naziv
                }).ToListAsync();

            return View(recenzija);
        }

        // GET: Recenzija/Delete/5
        [Authorize(Roles = "Administrator, Korisnik")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var recenzija = await _context.Recenzija
    .Include(r => r.Korisnik)
    .Include(r => r.Salon)
    .FirstOrDefaultAsync(m => m.recenzijaID == id);
            if (recenzija == null)
            {
                return NotFound();
            }

            //  Dodaj provjeru: samo vlasnik ili admin može pristupiti
            var korisnikId = _userManager.GetUserId(User);
            if (User.IsInRole("Korisnik") && recenzija.KorisnikID != korisnikId)
            {
                return Forbid(); // 403 - pokušaj brisanja tuđe recenzije
            }

            return View(recenzija);
        }

        // POST: Recenzija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Korisnik")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recenzija = await _context.Recenzija.FindAsync(id);
            if (recenzija == null)
            {
                return NotFound();
            }

            var korisnikId = _userManager.GetUserId(User);

            //  Dozvoli samo ako je admin ili vlasnik recenzije
            if (User.IsInRole("Korisnik") && recenzija.KorisnikID != korisnikId)
            {
                return Forbid(); // 403 - korisnik pokušava obrisati tuđu recenziju
            }

            _context.Recenzija.Remove(recenzija);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Recenzija je uspješno obrisana.";
            return RedirectToAction(nameof(Index));
        }

        private bool RecenzijaExists(int id)
        {
            return _context.Recenzija.Any(e => e.recenzijaID == id);
        }
    }
}
