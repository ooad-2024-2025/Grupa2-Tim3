﻿using System;
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
    public class ObavijestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ObavijestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Administrator, Korisnik, Frizer")]
        // GET: Obavijest
        public async Task<IActionResult> Index()
        {
            return View(await _context.Obavijest.ToListAsync());
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
