using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CompanyAssetsUI.Models;

namespace CompanyAssetsUI.Controllers
{
    public class DepreciationsController : Controller
    {
        private readonly AssetContext _context;

        public DepreciationsController(AssetContext context)
        {
            _context = context;
        }

        // GET: Active Depreciations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Depreciations.Where(a => a.DepreciationActive == true).ToListAsync());
        }
        
        // GET: Deactivated Depreciations
        public async Task<IActionResult> Deactivated()
        {
            return View(await _context.Depreciations.Where(a => a.DepreciationActive == false).ToListAsync());
        }

        // GET: Depreciations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depreciation = await _context.Depreciations
                .FirstOrDefaultAsync(m => m.DepreciationID == id);
            if (depreciation == null)
            {
                return NotFound();
            }

            return View(depreciation);
        }

        // GET: Depreciations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Depreciations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepreciationID,DepreciationName,DepreciationDescription")] Depreciation depreciation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(depreciation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(depreciation);
        }

        // GET: Depreciations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depreciation = await _context.Depreciations.FindAsync(id);
            if (depreciation == null)
            {
                return NotFound();
            }
            return View(depreciation);
        }

        // POST: Depreciations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepreciationID,DepreciationName,DepreciationDescription")] Depreciation depreciation)
        {
            if (id != depreciation.DepreciationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(depreciation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepreciationExists(depreciation.DepreciationID))
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
            return View(depreciation);
        }

        // GET: Depreciations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depreciation = await _context.Depreciations
                .FirstOrDefaultAsync(m => m.DepreciationID == id);
            if (depreciation == null)
            {
                return NotFound();
            }

            return View(depreciation);
        }

        // Deactivate instead of delete
        // POST: Depreciations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var depreciation = await _context.Depreciations.FindAsync(id);
            depreciation.DepreciationActive = false;
            _context.Depreciations.Update(depreciation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Activate deactivated Depreciation
        public async Task<IActionResult> Activate(int id)
        {
            var depreciation = await _context.Depreciations.FindAsync(id);
            depreciation.DepreciationActive = true;
            _context.Depreciations.Update(depreciation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Deactivated));
        }

        private bool DepreciationExists(int id)
        {
            return _context.Depreciations.Any(e => e.DepreciationID == id);
        }
    }
}
