using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebEng.ReplacementParts.Data;
using WebEng.ReplacementParts.Models;

namespace WebEng.Spares.Controllers
{
    public class SparePartsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SparePartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SpareParts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReplacementPart.Include(s => s.Manufacturer).Include(s => s.OEM);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SpareParts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sparePart = await _context.ReplacementPart
                .Include(s => s.Manufacturer)
                .Include(s => s.OEM)
                .FirstOrDefaultAsync(m => m.Key == id);
            if (sparePart == null)
            {
                return NotFound();
            }

            return View(sparePart);
        }

        // GET: SpareParts/Create
        public IActionResult Create()
        {
            ViewData["ManufacturerKey"] = new SelectList(_context.Manufacturer, "Key", "Name");
            ViewData["OEMKey"] = new SelectList(_context.OEM, "OEMNumber", "Name");
            return View();
        }

        // POST: SpareParts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Key,OEMKey,ManufacturerKey,Name,Description,Price,Weight,Available,PictureUrl")] SparePart sparePart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sparePart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerKey"] = new SelectList(_context.Manufacturer, "Key", "Key", sparePart.ManufacturerKey);
            ViewData["OEMKey"] = new SelectList(_context.OEM, "OEMNumber", "OEMNumber", sparePart.OEMKey);
            return View(sparePart);
        }

        // GET: SpareParts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sparePart = await _context.ReplacementPart.FindAsync(id);
            if (sparePart == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerKey"] = new SelectList(_context.Manufacturer, "Key", "Key", sparePart.ManufacturerKey);
            ViewData["OEMKey"] = new SelectList(_context.OEM, "OEMNumber", "OEMNumber", sparePart.OEMKey);
            return View(sparePart);
        }

        // POST: SpareParts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Key,OEMKey,ManufacturerKey,Name,Description,Price,Weight,Available,PictureUrl")] SparePart sparePart)
        {
            if (id != sparePart.Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sparePart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SparePartExists(sparePart.Key))
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
            ViewData["ManufacturerKey"] = new SelectList(_context.Manufacturer, "Key", "Key", sparePart.ManufacturerKey);
            ViewData["OEMKey"] = new SelectList(_context.OEM, "OEMNumber", "OEMNumber", sparePart.OEMKey);
            return View(sparePart);
        }

        // GET: SpareParts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sparePart = await _context.ReplacementPart
                .Include(s => s.Manufacturer)
                .Include(s => s.OEM)
                .FirstOrDefaultAsync(m => m.Key == id);
            if (sparePart == null)
            {
                return NotFound();
            }

            return View(sparePart);
        }

        // POST: SpareParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var sparePart = await _context.ReplacementPart.FindAsync(id);
            _context.ReplacementPart.Remove(sparePart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SparePartExists(long id)
        {
            return _context.ReplacementPart.Any(e => e.Key == id);
        }
    }
}
