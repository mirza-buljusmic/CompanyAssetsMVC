using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAssetsUI.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Name")]
        public string SupplierName { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name ="Description")]
        public string SupplierDescription { get; set; }

        [Required]
        [StringLength(150)]
        [EmailAddress]
        [Display(Name ="Email")]
        public string SupplierEmail { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Contact First Name")]
        public string SupplierContactFirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Contact Last Name")]
        public string SupplierContactLastName { get; set; }

        [Required]
        [StringLength(150)]
        [EmailAddress]
        [Display(Name ="Contact Email")]
        public string SupplierContactEmail { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name ="City")]
        public string SupplierCity { get; set; }

        [Required]
        [Display(Name ="Country")]
        public int CountryID { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name ="Address")]
        public string SupplierAddress { get; set; }

        [Display(Name ="Active")]
        public bool SupplierActive { get; set; }

        public Country Country { get; set; }
        public ICollection<Catalogue> Catalogues { get; set; }
    }
}
