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
    public class OEMCarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OEMCarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OEMCars
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OEMCar.Include(o => o.Car).ThenInclude(o => o.Brand).Include(o => o.OEM);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OEMCars/Create
        public IActionResult Create()
        {
            ViewData["CarFK"] = new SelectList(_context.Car, "Key", "Name");
            ViewData["OEMFK"] = new SelectList(_context.OEM, "OEMNumber", "Name");
            return View();
        }

        // POST: OEMCars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Key,CarFK,OEMFK")] OEMCar oEMCar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oEMCar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarFK"] = new SelectList(_context.Car, "Key", "Key", oEMCar.CarFK);
            ViewData["OEMFK"] = new SelectList(_context.OEM, "OEMNumber", "OEMNumber", oEMCar.OEMFK);
            return View(oEMCar);
        }

        // GET: OEMCars/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oEMCar = await _context.OEMCar
                .Include(o => o.Car).ThenInclude(e => e.Brand)
                .Include(o => o.OEM)
                .FirstOrDefaultAsync(m => m.Key == id);
            if (oEMCar == null)
            {
                return NotFound();
            }

            return View(oEMCar);
        }

        // POST: OEMCars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var oEMCar = await _context.OEMCar.FindAsync(id);
            _context.OEMCar.Remove(oEMCar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OEMCarExists(long id)
        {
            return _context.OEMCar.Any(e => e.Key == id);
        }
    }
}
