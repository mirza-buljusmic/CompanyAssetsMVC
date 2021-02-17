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
    public class ReplacementsController : Controller
    {
        private readonly AssetContext _context;

        public ReplacementsController(AssetContext context)
        {
            _context = context;
        }

        // GET: Replacements
        public async Task<IActionResult> Index()
        {
            var assetContext = _context.Replacements.Include(r => r.Replaced).Include(r => r.Replacing);
            return View(await assetContext.ToListAsync());
        }

        // GET: Replacements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var replacement = await _context.Replacements
                .Include(r => r.Replaced)
                .Include(r => r.Replacing)
                .FirstOrDefaultAsync(m => m.ReplacementID == id);
            if (replacement == null)
            {
                return NotFound();
            }

            return View(replacement);
        }

        // GET: Replacements/Create
        public IActionResult Create()
        {
            // Select ONLY Obsolete products
            ViewData["CatalogueID"] = new SelectList(_context.Catalogues.Where(a => a.Obsolete == true), "CatalogueID", "ProductName");
            // Select only NON obsolete products
            ViewData["ReplacingID"] = new SelectList(_context.Catalogues.Where(a => a.Obsolete == false), "CatalogueID", "ProductName");
            return View();
        }

        // POST: Replacements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReplacementID,CatalogueID,ReplacingID")] Replacement replacement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(replacement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatalogueID"] = new SelectList(_context.Catalogues.Where(a => a.Obsolete == true), "CatalogueID", "ProductName", replacement.CatalogueID);
            ViewData["ReplacingID"] = new SelectList(_context.Catalogues.Where(a => a.Obsolete == false), "CatalogueID", "ProductName", replacement.ReplacingID);
            return View(replacement);
        }

        // GET: Replacements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var replacement = await _context.Replacements.FindAsync(id);
            if (replacement == null)
            {
                return NotFound();
            }
            ViewData["CatalogueID"] = new SelectList(_context.Catalogues.Where(a => a.Obsolete == true), "CatalogueID", "ProductName", replacement.CatalogueID);
            ViewData["ReplacingID"] = new SelectList(_context.Catalogues.Where(a => a.Obsolete == false), "CatalogueID", "ProductName", replacement.ReplacingID);
            return View(replacement);
        }

        // POST: Replacements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReplacementID,CatalogueID,ReplacingID")] Replacement replacement)
        {
            if (id != replacement.ReplacementID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(replacement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReplacementExists(replacement.ReplacementID))
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
            ViewData["CatalogueID"] = new SelectList(_context.Catalogues.Where(a => a.Obsolete == true), "CatalogueID", "ProductName", replacement.CatalogueID);
            ViewData["ReplacingID"] = new SelectList(_context.Catalogues.Where(a => a.Obsolete == false), "CatalogueID", "ProductName", replacement.ReplacingID);
            return View(replacement);
        }

        // GET: Replacements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var replacement = await _context.Replacements
                .Include(r => r.Replaced)
                .Include(r => r.Replacing)
                .FirstOrDefaultAsync(m => m.ReplacementID == id);
            if (replacement == null)
            {
                return NotFound();
            }

            return View(replacement);
        }

        // POST: Replacements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var replacement = await _context.Replacements.FindAsync(id);
            _context.Replacements.Remove(replacement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReplacementExists(int id)
        {
            return _context.Replacements.Any(e => e.ReplacementID == id);
        }
    }
}
