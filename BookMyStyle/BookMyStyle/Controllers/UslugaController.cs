using BookMyStyle.Data;
using BookMyStyle.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        [Authorize(Roles = "Administrator, Frizer")]
        // GET: Usluga?salonID=5
        public async Task<IActionResult> Index(int? salonID)   // ADDED: filtriranje po salonu
        {
            var query = _context.Usluga
                                .Include(u => u.Salon)
                                .AsQueryable();

            if (salonID.HasValue)
            {
                query = query.Where(u => u.salonID == salonID.Value);
                ViewBag.SalonID = salonID.Value;              // šaljemo ID u view ako treba
            }

            var usluge = await query.ToListAsync();
            return View(usluge);
        }

        [Authorize(Roles = "Administrator, Frizer")]
        // GET: Usluga/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue) return NotFound();

            var usluga = await _context.Usluga
                .Include(u => u.Salon)
                .FirstOrDefaultAsync(m => m.uslugaID == id.Value);

            if (usluga == null) return NotFound();
            return View(usluga);
        }

        [Authorize(Roles = "Administrator, Frizer")]
        // GET: Usluga/Create?salonID=5
        public async Task<IActionResult> Create(int? salonID)
        {
            var model = new Usluga();

            if (User.IsInRole("Frizer"))
            {
                // ADDED: frizer može dodavati usluge samo u svoj salon
                var frizer = await _userManager.GetUserAsync(User);
                if (frizer?.SalonID == null)
                    return Forbid();
                model.salonID = frizer.SalonID.Value;
            }
            else if (User.IsInRole("Administrator"))
            {
                // ADDED: admin može birati salon iz padajuće liste
                ViewBag.Saloni = new SelectList(_context.Salon, "salonID", "Naziv");
            }

            // ADDED: ako smo dohvatili salonID kroz ruta, prepiši ga
            if (salonID.HasValue)
                model.salonID = salonID.Value;

            return View(model);
        }

        // POST: Usluga/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Frizer")]
        public async Task<IActionResult> Create(Usluga usluga)
        {
            // ADDED: uvijek zaštitimo salonID iz trenutnog usera (frizer ne može lažirati)
            if (User.IsInRole("Frizer"))
            {
                var frizer = await _userManager.GetUserAsync(User);
                usluga.salonID = frizer.SalonID.Value;
            }

            if (!ModelState.IsValid)
            {
                // ako je admin, ponovo napuni listu salona
                if (User.IsInRole("Administrator"))
                    ViewBag.Saloni = new SelectList(_context.Salon, "salonID", "Naziv", usluga.salonID);
                return View(usluga);
            }

            _context.Add(usluga);
            await _context.SaveChangesAsync();

            // nakon uspjeha, vraćamo se na detalje tog salona
            return RedirectToAction("Details", "Salon", new { id = usluga.salonID });
        }

        [Authorize(Roles = "Administrator, Frizer")]
        // GET: Usluga/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue) return NotFound();
            var usluga = await _context.Usluga.FindAsync(id.Value);
            if (usluga == null) return NotFound();

            if (User.IsInRole("Frizer"))
            {
                var frizer = await _userManager.GetUserAsync(User);
                if (usluga.salonID != frizer.SalonID)
                    return Forbid();
            }
            else if (User.IsInRole("Administrator"))
            {
                ViewBag.Saloni = new SelectList(_context.Salon, "salonID", "Naziv", usluga.salonID);
            }

            return View(usluga);
        }

        // POST: Usluga/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Frizer")]
        public async Task<IActionResult> Edit(int id, Usluga usluga)
        {
            if (id != usluga.uslugaID) return NotFound();

            if (User.IsInRole("Frizer"))
            {
                var frizer = await _userManager.GetUserAsync(User);
                usluga.salonID = frizer.SalonID.Value;
            }

            if (!ModelState.IsValid)
            {
                if (User.IsInRole("Administrator"))
                    ViewBag.Saloni = new SelectList(_context.Salon, "salonID", "Naziv", usluga.salonID);
                return View(usluga);
            }

            _context.Update(usluga);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Salon", new { id = usluga.salonID });
        }

        [Authorize(Roles = "Administrator, Frizer")]
        // GET: Usluga/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return NotFound();
            var usluga = await _context.Usluga
                .Include(u => u.Salon)
                .FirstOrDefaultAsync(m => m.uslugaID == id.Value);
            if (usluga == null) return NotFound();

            if (User.IsInRole("Frizer"))
            {
                var frizer = await _userManager.GetUserAsync(User);
                if (usluga.salonID != frizer.SalonID)
                    return Forbid();
            }

            return View(usluga);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Frizer")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usluga = await _context.Usluga.FindAsync(id);
            _context.Usluga.Remove(usluga);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Salon", new { id = usluga.salonID });
        }

        private bool UslugaExists(int id)
            => _context.Usluga.Any(e => e.uslugaID == id);
    }
}
