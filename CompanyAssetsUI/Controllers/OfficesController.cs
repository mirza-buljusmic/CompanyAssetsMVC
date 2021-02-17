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
    public class OfficesController : Controller
    {
        private readonly AssetContext _context;

        public OfficesController(AssetContext context)
        {
            _context = context;
        }

        // GET: Offices
        public async Task<IActionResult> Index()
        {
            var assetContext = _context.Offices.Include(o => o.Country).Include(o => o.Currency);
            return View(await assetContext.Where(a => a.OfficeActive == true).ToListAsync());
        }

        // GET: Offices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var office = await _context.Offices
                .Include(o => o.Country)
                .Include(o => o.Currency)
                .FirstOrDefaultAsync(m => m.OfficeID == id);
            if (office == null)
            {
                return NotFound();
            }

            return View(office);
        }

        // GET: Offices
        public async Task<IActionResult> Deactivated()
        {
            var assetContext = _context.Offices.Include(o => o.Country).Include(o => o.Currency);
            return View(await assetContext.Where(a => a.OfficeActive == false).ToListAsync());
        }

        // GET: Offices/Create
        public IActionResult Create()
        {
            ViewData["CountryID"] = new SelectList(_context.Countries, "CountryId", "CountryDescription");
            ViewData["CurrencyID"] = new SelectList(_context.Currencies, "CurrencyID", "CurrencyName");
            return View();
        }

        // POST: Offices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfficeID,OfficeName,OfficeDescription,CountryID,CurrencyID,OfficeActive")] Office office)
        {
            if (ModelState.IsValid)
            {
                _context.Add(office);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryID"] = new SelectList(_context.Countries, "CountryId", "CountryDescription", office.CountryID);
            ViewData["CurrencyID"] = new SelectList(_context.Currencies, "CurrencyID", "CurrencyName", office.CurrencyID);
            return View(office);
        }

        // GET: Offices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var office = await _context.Offices.FindAsync(id);
            if (office == null)
            {
                return NotFound();
            }
            ViewData["CountryID"] = new SelectList(_context.Countries, "CountryId", "CountryDescription", office.CountryID);
            ViewData["CurrencyID"] = new SelectList(_context.Currencies, "CurrencyID", "CurrencyName", office.CurrencyID);
            return View(office);
        }

        // POST: Offices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OfficeID,OfficeName,OfficeDescription,CountryID,CurrencyID,OfficeActive")] Office office)
        {
            if (id != office.OfficeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(office);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfficeExists(office.OfficeID))
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
            ViewData["CountryID"] = new SelectList(_context.Countries, "CountryId", "CountryDescription", office.CountryID);
            ViewData["CurrencyID"] = new SelectList(_context.Currencies, "CurrencyID", "CurrencyName", office.CurrencyID);
            return View(office);
        }

        // GET: Offices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var office = await _context.Offices
                .Include(o => o.Country)
                .Include(o => o.Currency)
                .FirstOrDefaultAsync(m => m.OfficeID == id);
            if (office == null)
            {
                return NotFound();
            }

            return View(office);
        }

        // Sets Deactivated instead of delete
        // POST: Offices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var office = await _context.Offices.FindAsync(id);
            office.OfficeActive = false;
            _context.Offices.Update(office);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        // Activates the deactivated Office
        // POST: Offices/Activate/5
        //[HttpPost, ActionName("Activate")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivateOffice(int id)
        {
            var office = await _context.Offices.FindAsync(id);
            office.OfficeActive = true;
            _context.Offices.Update(office);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Deactivated));
        }

        private bool OfficeExists(int id)
        {
            return _context.Offices.Any(e => e.OfficeID == id);
        }
    }
}
