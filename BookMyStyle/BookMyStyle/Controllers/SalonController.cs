using BookMyStyle.Data;
using BookMyStyle.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // GET: Salon
        public async Task<IActionResult> Index()
        {
            return View(await _context.Salon.ToListAsync());
        }

        // GET: Salon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue) return NotFound();

            var salon = await _context.Salon
                // ADDED: uključimo navigacijske kolekcije Usluga i Termin
                .Include(s => s.Usluge)
                .Include(s => s.Termin)
                .FirstOrDefaultAsync(m => m.salonID == id.Value);

            if (salon == null) return NotFound();
            return View(salon);
        }

        [Authorize(Roles = "Administrator, Frizer")]
        // GET: Salon/Create
        public IActionResult Create()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Frizer")]
        // POST: Salon/Create
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
            if (!id.HasValue) return NotFound();
            var salon = await _context.Salon.FindAsync(id.Value);
            if (salon == null) return NotFound();
            return View(salon);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Frizer")]
        // POST: Salon/Edit/5
        public async Task<IActionResult> Edit(int id, Salon salon)
        {
            if (id != salon.salonID) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Salon.Any(e => e.salonID == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(salon);
        }

        [Authorize(Roles = "Administrator, Frizer")]
        // GET: Salon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return NotFound();
            var salon = await _context.Salon.FirstOrDefaultAsync(m => m.salonID == id.Value);
            if (salon == null) return NotFound();
            return View(salon);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Frizer")]
        // POST: Salon/Delete/5
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salon = await _context.Salon.FindAsync(id);
            if (salon != null)
            {
                _context.Salon.Remove(salon);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SalonExists(int id)
            => _context.Salon.Any(e => e.salonID == id);

        [Authorize(Roles = "Administrator, Frizer")]
        // ADDED: preusmjerava na Create u UslugaController i prosljeđuje salonID
        // GET: Salon/DodajUslugu/5
        public IActionResult DodajUslugu(int id)
            => RedirectToAction("Create", "Usluga", new { salonID = id });
    }
}
