using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyAssetsUI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFCAssets.Controllers
{
    public class ReportsController : Controller
    {
        private readonly AssetContext _context;

        public ReportsController(AssetContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PurchaseList()
        {
            // How many of each expired asset per office
            var expiredAssets = _context.Assets.Include(b=>b.Office).Include(c=>c.Catalogue).Where(a => a.AssetExpirationDate < DateTime.Today).OrderBy(a=>a.Office.OfficeName).ThenBy(a=>a.Catalogue.ProductName).ToList();

            var groupedOffices = from p in expiredAssets 
                                 group p by (p.Office.OfficeName, p.Catalogue.ProductName)
                                 into g select new VM_PurchaseList { OfficeName = g.Key.OfficeName, AssetName = g.Key.ProductName, Count = g.Count() };

            // convert from IEnumerable to Ilist to be able to update
            var purchase = groupedOffices.ToList();
            // replacement-check
            foreach(var item in purchase)
            {
                // check if replacement exists
                var replacement = _context.Replacements.Include(b => b.Replaced).FirstOrDefault(a => a.Replaced.ProductName == item.AssetName);
                if(replacement != null)
                {
                    // get replacement
                    var replacingAsset = _context.Catalogues.FirstOrDefault(m=>m.CatalogueID == replacement.ReplacingID);
                    item.ReplacedBy = replacingAsset.ProductName;
                    
                }

            }

            return View(purchase);
        }

        public IActionResult DashboardValue()
        {
            var officeGroup = _context.Assets.GroupBy(a => a.Office.OfficeName)
                .Select(x => new VM_OfcLIst
                {
                    OfficeName = x.Key,
                    AssetSum = x.Sum(y => y.AssetPrice)
                }).ToList();

            ViewBag.ofcList = officeGroup;

            return View(officeGroup);

        }   
        public IActionResult DashboardDeprValue()
        {
            var officeGroup = _context.Assets.GroupBy(a => a.Office.OfficeName)
                .Select(x => new VM_OfcLIst
                {
                    OfficeName = x.Key,
                    AssetSum = x.Sum(y => y.AssetValue)
                }).ToList();

            ViewBag.ofcList = officeGroup;

            return View(officeGroup);
        }
        
        public IActionResult DashboardCategory()
        {
            var categoryGroup = _context.Assets.Include(b=>b.Catalogue).GroupBy(b => b.Catalogue.Category.CategoryName)
                .Select(x => new VM_CategorySummary
                {
                    CategoryName = x.Key,
                    CategorySum = x.Sum(y => y.AssetPrice)
                }).ToList();
            //ViewBag.ofcList = officeGroup;

            return View(categoryGroup);
        }
        
        public IActionResult DashboardDeprCategory()
        {
            var categoryGroup = _context.Assets.Include(b=>b.Catalogue).GroupBy(a => a.Catalogue.Category.CategoryName)
                .Select(x => new VM_CategorySummary
                {
                    CategoryName = x.Key,
                    CategorySum = x.Sum(y => y.AssetValue)
                }).ToList();
            //ViewBag.ofcList = officeGroup;

            return View(categoryGroup);
        }

        /*
        public async Task<IActionResult> DeactivatedCurrencies()
        {
            return View(await _context.Currencies.Where(a => a.CurrencyActive == false).ToListAsync());
        }

        public IActionResult ActivateCurrency(int id)
        {
            var selectedCurrency = _context.Currencies.Where(a => a.Id == id).FirstOrDefault();
            selectedCurrency.CurrencyActive = true;
            _context.Update(selectedCurrency);
            _context.SaveChanges();
            return RedirectToAction(nameof(DeactivatedCurrencies));
        }

        public async Task<IActionResult> DeactivatedCategories()
        {
            return View(await _context.Categories.Where(a => a.CategoryActive == false).ToListAsync());
        }

        public IActionResult ActivateCategory(int id)
        {
            var selectedCategory = _context.Categories.Where(a => a.CategoryId == id).FirstOrDefault();
            selectedCategory.CategoryActive = true;
            _context.Update(selectedCategory);
            _context.SaveChanges();
            return RedirectToAction(nameof(DeactivatedCategories));
        }

        public async Task<IActionResult> DeactivatedOffices()
        {
            return View(await _context.Offices.Where(a => a.OfficeActive == false).Include(b => b.Country).ToListAsync());
        }

        public IActionResult ActivateOffice(int id)
        {
            var selectedOffice = _context.Offices.Where(a => a.OfficeId == id).FirstOrDefault();
            selectedOffice.OfficeActive = true;
            _context.Update(selectedOffice);
            _context.SaveChanges();
            return RedirectToAction(nameof(DeactivatedOffices));
        }

        public async Task<IActionResult> DeactivatedSuppliers()
        {
            return View(await _context.Suppliers.Where(a => a.SupplierActive == false).ToListAsync());
        }

        public IActionResult ActivateSupplier(int id)
        {
            var selectedSupplier = _context.Suppliers.Where(a => a.SupplierId == id).FirstOrDefault();
            selectedSupplier.SupplierActive = true;
            _context.Update(selectedSupplier);
            _context.SaveChanges();
            return RedirectToAction(nameof(DeactivatedSuppliers));
        }

        */

        public IActionResult ChooseOfficeForActiveAssets()
        {
            // Preprare dropdown list content
            ViewData["OfficeId"] = new SelectList(_context.Offices.Where(a => a.OfficeActive == true), nameof(Office.OfficeID), nameof(Office.OfficeName));
            return View();
        }

        public async Task<IActionResult> ExpiredAssets()
        {
            var assetContext = await _context.Assets.Include(a => a.Catalogue).ThenInclude(d=>d.Category).Include(b => b.Office).ThenInclude(c => c.Currency).Where(a => a.AssetActive == true).ToListAsync();
            var filteredResult = assetContext.Where(x => x.AssetExpirationDate <= DateTime.Today && x.AssetActive == true);
            var offices = _context.Offices.Include(a => a.Currency).ToList();

            ViewData["sumPrice"] = filteredResult.Select(c => c.AssetPrice).Sum();

            return View(filteredResult);
        }


        //public async Task<IActionResult> OfcActiveLocalCurrency(int id)
        //{
        //    var assetContext = await _context.Assets
        //        .Include(a => a.Category)
        //        .Include(b => b.Office)
        //            .ThenInclude(c => c.Currency)
        //        .Where(a => a.AssetActive == true && a.OfficeId == id)
        //        .ToListAsync();

        //    var filteredResult = assetContext
        //        .Where(x => x.OfficeId == id && x.AssetActive == true)
        //        .OrderBy(x=>x.Category.CategoryName);

        //    ViewData["office"] = _context.Offices.FirstOrDefault(x => x.OfficeId == id).OfficeName;
        //    ViewData["currency"] = _context.Offices
        //        .Include(a => a.Currency)
        //        .FirstOrDefault(x => x.OfficeId == id)
        //        .Currency.CurrencyName;

        //    return View(filteredResult);
        //}
    }
}
