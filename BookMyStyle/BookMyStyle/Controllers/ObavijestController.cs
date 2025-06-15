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
    public class ObavijestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Korisnik> _userManager;

        public ObavijestController(ApplicationDbContext context, UserManager<Korisnik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Administrator, Korisnik, Frizer")]
        // GET: Obavijest
        public async Task<IActionResult> Index()
        {

            var korisnikId = _userManager.GetUserId(User);

            if (User.IsInRole("Administrator"))
            {
                // Admin vidi sve obavijesti
                var sveObavijesti = await _context.Obavijest
                    .OrderByDescending(o => o.DatumIVrijeme)
                    .ToListAsync();
                return View(sveObavijesti);
            }

            var relevantniTermini = await _context.Termin
                .Where(t => t.KorisnikID == korisnikId || t.FrizerID == korisnikId)
                .Select(t => t.terminID)
                .ToListAsync();

            var obavijesti = await _context.Obavijest
                .Where(o => relevantniTermini.Contains(o.terminID))
                .ToListAsync();

            return View(obavijesti);
        }

        [Authorize(Roles = "Administrator, Korisnik, Frizer")]
        // GET: Obavijest/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obavijest = await _context.Obavijest
                .FirstOrDefaultAsync(m => m.obavijestID == id);
            if (obavijest == null)
            {
                return NotFound();
            }

            return View(obavijest);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Obavijest/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Obavijest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("obavijestID,Tekst,DatumIVrijeme,terminID")] Obavijest obavijest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(obavijest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(obavijest);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Obavijest/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obavijest = await _context.Obavijest.FindAsync(id);
            if (obavijest == null)
            {
                return NotFound();
            }
            return View(obavijest);
        }

        // POST: Obavijest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("obavijestID,Tekst,DatumIVrijeme,terminID")] Obavijest obavijest)
        {
            if (id != obavijest.obavijestID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(obavijest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObavijestExists(obavijest.obavijestID))
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
            return View(obavijest);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Obavijest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obavijest = await _context.Obavijest
                .FirstOrDefaultAsync(m => m.obavijestID == id);
            if (obavijest == null)
            {
                return NotFound();
            }

            return View(obavijest);
        }

        // POST: Obavijest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var obavijest = await _context.Obavijest.FindAsync(id);
            if (obavijest != null)
            {
                _context.Obavijest.Remove(obavijest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ObavijestExists(int id)
        {
            return _context.Obavijest.Any(e => e.obavijestID == id);
        }
    }
}
