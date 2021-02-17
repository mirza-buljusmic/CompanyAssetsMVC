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
    public class CategoriesController : Controller
    {
        private readonly AssetContext _context;

        public CategoriesController(AssetContext context)
        {
            _context = context;
        }

        // GET: Active Categories
        public async Task<IActionResult> Index()
        {
            var assetContext = _context.Categories.Include(c => c.Depreciation);
            return View(await assetContext.Where(a=>a.CategoryActive == true).ToListAsync());
        }
        
        // GET: Deactivated Categories
        public async Task<IActionResult> Deactivated()
        {
            var assetContext = _context.Categories.Include(c => c.Depreciation);
            return View(await assetContext.Where(a=>a.CategoryActive == false).ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.Depreciation)
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            ViewData["DepreciationID"] = new SelectList(_context.Depreciations, "DepreciationID", "DepreciationDescription");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryEOLMonths,CategoryActive,CategoryComment,DepreciationID")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepreciationID"] = new SelectList(_context.Depreciations, "DepreciationID", "DepreciationDescription", category.DepreciationID);
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["DepreciationID"] = new SelectList(_context.Depreciations, "DepreciationID", "DepreciationDescription", category.DepreciationID);
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,CategoryEOLMonths,CategoryActive,CategoryComment,DepreciationID")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
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
            ViewData["DepreciationID"] = new SelectList(_context.Depreciations, "DepreciationID", "DepreciationDescription", category.DepreciationID);
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.Depreciation)
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // Deactivate instead of delete
        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            category.CategoryActive = false;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Activate deactivated category
        public async Task<IActionResult> Activate(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            category.CategoryActive = true;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Deactivated));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
