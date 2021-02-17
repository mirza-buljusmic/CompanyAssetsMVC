using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAssetsUI.Models
{
    public class Catalogue
    {
        [Key]
        public int CatalogueID { get; set; }
        
        [Required]
        [StringLength(50)]
        [Display(Name ="Product")]
        public string ProductName { get; set; }
        
        [Required]
        [StringLength(250)]
        [Display(Name ="Product Description")]
        public string ProductDescription { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Required]
        [Display(Name ="Price")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F}")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal CataloguePrice { get; set; }

        [Required]
        [Display(Name ="Supplier product ID")]
        public int SupplierProductID { get; set; }
        
        [Required]
        [Display(Name ="Supplier")]
        public int SupplierID { get; set; }

        [Display(Name ="Obsolete")]
        public bool Obsolete { get; set; }

        [Display(Name ="Active")]
        public bool CatalogueItemActive { get; set; }

        public Supplier Supplier { get; set; }
        public Category Category { get; set; }
        public ICollection<Asset> Assets { get; set; }
    }
}
