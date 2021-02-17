using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAssetsUI.Models
{
    public class AssetContext : DbContext
    {
        public AssetContext(DbContextOptions<AssetContext> options) : base(options)
        {

        }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Catalogue> Catalogues { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Replacement> Replacements { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Depreciation> Depreciations { get; set; }

    }
}
