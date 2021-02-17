using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAssetsUI.Models
{
    public class Asset
    {
        [Key]
        public int AssetID { get; set; }

        [Required]
        public int CatalogueID { get; set; }

        [Required]
        [Display(Name ="Purchase Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime AssetPurchaseDate { get; set; }

        [Required]
        [Display(Name ="Expiration Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime AssetExpirationDate { get; set; }

        [Required]
        [Display(Name ="Price")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F}")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AssetPrice { get; set; }

        [Display(Name ="Value")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F}")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AssetValue { get; set; }

        [Display(Name ="Active")]
        public bool AssetActive { get; set; }

        public int OfficeID { get; set; }

        public Office Office { get; set; }
        public Catalogue Catalogue { get; set; }
    }
}
