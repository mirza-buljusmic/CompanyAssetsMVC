using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAssetsUI.Models
{
    public class Country
    {
        public int CountryId { get; set; }

        [Required]
        [Display(Name = "Country code")]
        [StringLength(10)]
        public string CountryName { get; set; }

        [Required]
        [Display(Name = "Country name")]
        [StringLength(50)]
        public string CountryDescription { get; set; }

        public bool CountryVisible { get; set; }

        public ICollection<Supplier> Suppliers { get; set; }
        public ICollection<Office> Offices { get; set; }
    }
}
