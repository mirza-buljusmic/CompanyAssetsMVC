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
    public class AssetsController : Controller
    {
        private readonly AssetContext _context;

        public AssetsController(AssetContext context)
        {
            _context = context;
        }

        // GET: Assets
        public async Task<IActionResult> Index()
        {
            var assetContext = _context.Assets.Include(a => a.Catalogue).Include(a => a.Office).Include(a => a.Catalogue.Category);
            return View(await assetContext.Where(a => a.AssetActive == true).OrderBy(a=>a.Office.OfficeName).ThenBy(a=>a.Catalogue.Category.CategoryName).ThenBy(a=>a.AssetPurchaseDate).ToListAsync());
        }
        
        // GET: Assets
        public async Task<IActionResult> Deactivated()
        {
            var assetContext = _context.Assets.Include(a => a.Catalogue).Include(a => a.Office).Include(a => a.Catalogue.Category);
            return View(await assetContext.Where(a => a.AssetActive == false).OrderBy(a=>a.Office.OfficeName).ThenBy(a=>a.Catalogue.Category.CategoryName) .ToListAsync());
        }

        // GET: Assets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets
                .Include(a => a.Catalogue)
                .Include(a => a.Office)
                .FirstOrDefaultAsync(m => m.AssetID == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        // GET: Assets/Create
        public IActionResult Create()
        {
            // Select only products which are not Obsolete
            ViewData["CatalogueID"] = new SelectList(_context.Catalogues.Where(a => a.Obsolete == false), "CatalogueID", "ProductName");
            // Select only active offices
            ViewData["OfficeID"] = new SelectList(_context.Offices.Where(a => a.OfficeActive == true), "OfficeID", "OfficeName");
            return View();
        }

        // POST: Assets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssetID,CatalogueID,AssetPurchaseDate,AssetExpirationDate,AssetPrice,AssetValue,AssetActive,OfficeID")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                // Select Catalogue item to populate price and EOL lifespan from Category
                var catalogueItem = await _context.Catalogues.Include(b => b.Category).FirstOrDefaultAsync(a => a.CatalogueID == asset.CatalogueID);
                asset.AssetExpirationDate = asset.AssetPurchaseDate.AddMonths(catalogueItem.Category.CategoryEOLMonths);
                asset.AssetPrice = catalogueItem.CataloguePrice;
                _context.Add(asset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatalogueID"] = new SelectList(_context.Catalogues.Where(a => a.Obsolete == false), "CatalogueID", "ProductName", asset.CatalogueID);
            ViewData["OfficeID"] = new SelectList(_context.Offices.Where(a => a.OfficeActive == true), "OfficeID", "OfficeName", asset.OfficeID);
            return View(asset);
        }

        // GET: Assets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }
            ViewData["CatalogueID"] = new SelectList(_context.Catalogues.Where(a => a.Obsolete == false), "CatalogueID", "ProductName", asset.CatalogueID);
            ViewData["OfficeID"] = new SelectList(_context.Offices.Where(a => a.OfficeActive == true), "OfficeID", "OfficeName", asset.OfficeID);
            return View(asset);
        }

        // POST: Assets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssetID,CatalogueID,AssetPurchaseDate,AssetExpirationDate,AssetPrice,AssetValue,AssetActive,OfficeID")] Asset asset)
        {
            if (id != asset.AssetID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetExists(asset.AssetID))
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
            ViewData["CatalogueID"] = new SelectList(_context.Catalogues.Where(a => a.Obsolete == false), "CatalogueID", "ProductName", asset.CatalogueID);
            ViewData["OfficeID"] = new SelectList(_context.Offices.Where(a => a.OfficeActive == true), "OfficeID", "OfficeName", asset.OfficeID);
            return View(asset);
        }

        // GET: Assets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets
                .Include(a => a.Catalogue)
                .Include(a => a.Office)
                .FirstOrDefaultAsync(m => m.AssetID == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        // Deactivates Asset instead of delete
        // POST: Assets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asset = await _context.Assets.FindAsync(id);
            asset.AssetActive = false;
            _context.Assets.Update(asset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // activate asset
        public async Task<IActionResult> Activate(int id)
        {
            var asset = await _context.Assets.FindAsync(id);
            asset.AssetActive = true;
            _context.Assets.Update(asset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Deactivated));
        }

        private bool AssetExists(int id)
        {
            return _context.Assets.Any(e => e.AssetID == id);
        }
    }
}
