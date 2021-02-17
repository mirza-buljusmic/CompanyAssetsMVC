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
    public class CurrenciesController : Controller
    {
        private readonly AssetContext _context;

        public CurrenciesController(AssetContext context)
        {
            _context = context;
        }

        
        // GET: Active Currencies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Currencies.Where(a=>a.CurrencyActive == true).ToListAsync());
        } 
        
        // GET: Deactivated Currencies
        public async Task<IActionResult> Deactivated()
        {
            return View(await _context.Currencies.Where(a=>a.CurrencyActive == false).ToListAsync());
        }

        // GET: Currencies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currencies
                .FirstOrDefaultAsync(m => m.CurrencyID == id);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // GET: Currencies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Currencies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CurrencyID,CurrencyName,CurrencyDescription,ExchangeRate,CurrencyDefault,CurrencyActive")] Currency currency)
        {
            if (ModelState.IsValid)
            {
                _context.Add(currency);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(currency);
        }

        // GET: Currencies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currencies.FindAsync(id);
            if (currency == null)
            {
                return NotFound();
            }
            return View(currency);
        }

        // POST: Currencies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CurrencyID,CurrencyName,CurrencyDescription,ExchangeRate,CurrencyDefault,CurrencyActive")] Currency currency)
        {
            if (id != currency.CurrencyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(currency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurrencyExists(currency.CurrencyID))
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
            return View(currency);
        }

        // GET: Currencies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currencies
                .FirstOrDefaultAsync(m => m.CurrencyID == id);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // POST: Currencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currency = await _context.Currencies.FindAsync(id);
            currency.CurrencyActive = false;
            _context.Currencies.Update(currency);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Activate deactivated currency
        public async Task<IActionResult> Activate(int id)
        {
            var currency = await _context.Currencies.FindAsync(id);
            currency.CurrencyActive = true;
            _context.Currencies.Update(currency);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Deactivated));
        }
        private bool CurrencyExists(int id)
        {
            return _context.Currencies.Any(e => e.CurrencyID == id);
        }
    }
}
