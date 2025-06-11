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
    public class SalonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Korisnik> _userManager;

        public SalonController(ApplicationDbContext context, UserManager<Korisnik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Salon
        public async Task<IActionResult> Index()
        {

            if (User.IsInRole("Frizer"))
            {
                var currentUser = await _userManager.GetUserAsync(User);
                ViewBag.MojSalonID = currentUser?.SalonID;
            }
            else
            {
                ViewBag.MojSalonID = null;
            }

            return View(await _context.Salon.ToListAsync());
        }


        // GET: Salon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var salon = await _context.Salon.FirstOrDefaultAsync(m => m.salonID == id);
            if (salon == null)
                return NotFound();

            ViewBag.Usluge = await _context.Usluga.Where(u => u.salonID == id).ToListAsync();
            ViewBag.Termini = await _context.Termin.Where(t => t.salonID == id && t.KorisnikID != null).ToListAsync();

            // Dodajemo informaciju o vlasništvu
            ViewBag.JeVlasnik = false;

            if (User.IsInRole("Frizer"))
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser?.SalonID == salon.salonID)
                    ViewBag.JeVlasnik = true;
            }
            else if (User.IsInRole("Administrator"))
            {
                ViewBag.JeVlasnik = true;
            }

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

            if(User.IsInRole("Frizer")){
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser?.SalonID != salon.salonID)
                    return Forbid();
            }

            return View(salon);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Frizer")]
        public async Task<IActionResult> Edit(int id, [Bind("salonID,Naziv,Adresa,RadnoVrijeme")] Salon salon)
        {
            if (id != salon.salonID)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (User.IsInRole("Frizer") && currentUser?.SalonID != id)
                return Forbid();


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

            var currentUser = await _userManager.GetUserAsync(User);
            if (User.IsInRole("Frizer") && currentUser?.SalonID != salon.salonID)
                return Forbid();


            return View(salon);
        }

        // POST: Salon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Frizer")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salon = await _context.Salon.FindAsync(id);
            if (salon == null)
                return NotFound();

            var currentUser = await _userManager.GetUserAsync(User);
            if (User.IsInRole("Frizer") && currentUser?.SalonID != salon.salonID)
                return Forbid();

            _context.Salon.Remove(salon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool SalonExists(int id)
        {
            return _context.Salon.Any(e => e.salonID == id);
        }
    }
}
