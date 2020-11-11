using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebEng.ReplacementParts.Data;
using WebEng.ReplacementParts.Models;

namespace WebEng.ReplacementParts.Controllers
{
    public class ReplacementPartsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReplacementPartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReplacementParts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReplacementPart.Include(r => r.Manufacturer).Include(r => r.OEM);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ReplacementParts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var replacementPart = await _context.ReplacementPart
                .Include(r => r.Manufacturer)
                .Include(r => r.OEM)
                .FirstOrDefaultAsync(m => m.Key == id);
            if (replacementPart == null)
            {
                return NotFound();
            }

            return View(replacementPart);
        }

        // GET: ReplacementParts/Create
        public IActionResult Create()
        {
            ViewData["ManufacturerKey"] = new SelectList(_context.Manufacturer, "Name", "Name");
            ViewData["OEMKey"] = new SelectList(_context.OEM, "OEMNumber", "OEMNumber");
            return View();
        }

        // POST: ReplacementParts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Key,OEMKey,ManufacturerKey,Name,Description,Price,Weight,Available")] ReplacementPart replacementPart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(replacementPart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerKey"] = new SelectList(_context.Manufacturer, "Name", "Name", replacementPart.ManufacturerKey);
            ViewData["OEMKey"] = new SelectList(_context.OEM, "OEMNumber", "OEMNumber", replacementPart.OEMKey);
            return View(replacementPart);
        }

        // GET: ReplacementParts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var replacementPart = await _context.ReplacementPart.FindAsync(id);
            if (replacementPart == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerKey"] = new SelectList(_context.Manufacturer, "Name", "Name", replacementPart.ManufacturerKey);
            ViewData["OEMKey"] = new SelectList(_context.OEM, "OEMNumber", "OEMNumber", replacementPart.OEMKey);
            return View(replacementPart);
        }

        // POST: ReplacementParts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Key,OEMKey,ManufacturerKey,Name,Description,Price,Weight,Available")] ReplacementPart replacementPart)
        {
            if (id != replacementPart.Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(replacementPart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReplacementPartExists(replacementPart.Key))
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
            ViewData["ManufacturerKey"] = new SelectList(_context.Manufacturer, "Name", "Name", replacementPart.ManufacturerKey);
            ViewData["OEMKey"] = new SelectList(_context.OEM, "OEMNumber", "OEMNumber", replacementPart.OEMKey);
            return View(replacementPart);
        }

        // GET: ReplacementParts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var replacementPart = await _context.ReplacementPart
                .Include(r => r.Manufacturer)
                .Include(r => r.OEM)
                .FirstOrDefaultAsync(m => m.Key == id);
            if (replacementPart == null)
            {
                return NotFound();
            }

            return View(replacementPart);
        }

        // POST: ReplacementParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var replacementPart = await _context.ReplacementPart.FindAsync(id);
            _context.ReplacementPart.Remove(replacementPart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReplacementPartExists(long id)
        {
            return _context.ReplacementPart.Any(e => e.Key == id);
        }
    }
}
