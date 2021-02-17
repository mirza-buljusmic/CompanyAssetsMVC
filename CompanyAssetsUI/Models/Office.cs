using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAssetsUI.Models
{
    public class Office
    {
        [Key]
        public int OfficeID { get; set; }

        [Required]
        [Display(Name ="Office Name")]
        public string OfficeName { get; set; }

        [Required]
        [Display(Name ="Description")]
        [StringLength(250)]
        public string OfficeDescription { get; set; }

        public int CountryID { get; set; }
        public int CurrencyID { get; set; }

        [Display(Name ="Active")]
        public bool OfficeActive { get; set; }

        public Currency Currency { get; set; }
        public Country Country { get; set; }

        public ICollection<Asset> Assets { get; set; }
    }
}
