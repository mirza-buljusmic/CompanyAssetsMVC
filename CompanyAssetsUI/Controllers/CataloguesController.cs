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
    public class CataloguesController : Controller
    {
        private readonly AssetContext _context;

        public CataloguesController(AssetContext context)
        {
            _context = context;
        }

        // GET: Active Catalogues
        public async Task<IActionResult> Index()
        {
            var assetContext = _context.Catalogues.Include(c => c.Category).Include(c => c.Supplier);
            return View(await assetContext.Where(a=>a.CatalogueItemActive == true).OrderBy(a=>a.Obsolete).ThenBy(b=>b.Category.CategoryName).ToListAsync());
        } 
        
        // GET: Deactivated Catalogues
        public async Task<IActionResult> Deactivated()
        {
            var assetContext = _context.Catalogues.Include(c => c.Category).Include(c => c.Supplier);
            return View(await assetContext.Where(a=>a.CatalogueItemActive == false).OrderBy(a=>a.Obsolete).ThenBy(b=>b.Category.CategoryName).ToListAsync());
        }

        // GET: Catalogues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogue = await _context.Catalogues
                .Include(c => c.Category)
                .Include(c => c.Supplier)
                .FirstOrDefaultAsync(m => m.CatalogueID == id);
            if (catalogue == null)
            {
                return NotFound();
            }

            return View(catalogue);
        }

        // GET: Catalogues/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierName");
            return View();
        }

        // POST: Catalogues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatalogueID,ProductName,ProductDescription,CategoryID,CataloguePrice,SupplierProductID,SupplierID,Obsolete,CatalogueItemActive")] Catalogue catalogue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catalogue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", catalogue.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierName", catalogue.SupplierID);
            return View(catalogue);
        }

        // GET: Catalogues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogue = await _context.Catalogues.FindAsync(id);
            if (catalogue == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", catalogue.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierName", catalogue.SupplierID);
            return View(catalogue);
        }

        // POST: Catalogues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CatalogueID,ProductName,ProductDescription,CategoryID,CataloguePrice,SupplierProductID,SupplierID,Obsolete,CatalogueItemActive")] Catalogue catalogue)
        {
            if (id != catalogue.CatalogueID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalogue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogueExists(catalogue.CatalogueID))
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
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", catalogue.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierName", catalogue.SupplierID);
            return View(catalogue);
        }

        // GET: Catalogues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogue = await _context.Catalogues
                .Include(c => c.Category)
                .Include(c => c.Supplier)
                .FirstOrDefaultAsync(m => m.CatalogueID == id);
            if (catalogue == null)
            {
                return NotFound();
            }

            return View(catalogue);
        }

        // Deactivate product instead of delelete
        // POST: Catalogues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catalogue = await _context.Catalogues.FindAsync(id);
            catalogue.CatalogueItemActive = false;
            _context.Catalogues.Update(catalogue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Activate deactivated product
        public async Task<IActionResult> Activate(int id)
        {
            var catalogue = await _context.Catalogues.FindAsync(id);
            catalogue.CatalogueItemActive = true;
            _context.Catalogues.Update(catalogue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Deactivated));
        }

        private bool CatalogueExists(int id)
        {
            return _context.Catalogues.Any(e => e.CatalogueID == id);
        }
    }
}
