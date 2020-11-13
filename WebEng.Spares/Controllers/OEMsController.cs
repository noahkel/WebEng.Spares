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
    public class OEMsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OEMsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OEMs
        public async Task<IActionResult> Index()
        {
            return View(await _context.OEM.ToListAsync());
        }

        // GET: OEMs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oEM = await _context.OEM
                .FirstOrDefaultAsync(m => m.OEMNumber == id);
            if (oEM == null)
            {
                return NotFound();
            }

            return View(oEM);
        }

        // GET: OEMs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OEMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OEM oEM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oEM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(oEM);
        }

        // GET: OEMs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oEM = await _context.OEM.FindAsync(id);
            if (oEM == null)
            {
                return NotFound();
            }
            return View(oEM);
        }

        // POST: OEMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, OEM oEM)
        {
            if (id != oEM.OEMNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oEM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OEMExists(oEM.OEMNumber))
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
            return View(oEM);
        }

        // GET: OEMs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oEM = await _context.OEM
                .FirstOrDefaultAsync(m => m.OEMNumber == id);
            if (oEM == null)
            {
                return NotFound();
            }

            return View(oEM);
        }

        // POST: OEMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var oEM = await _context.OEM.FindAsync(id);
            _context.OEM.Remove(oEM);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OEMExists(string id)
        {
            return _context.OEM.Any(e => e.OEMNumber == id);
        }
    }
}
