using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAssetsUI.Models
{
    public class Currency
    {
        [Key]
        public int CurrencyID { get; set; }

        [Required]
        [Display(Name ="Currency")]
        [StringLength(50)]
        public string CurrencyName { get; set; }

        [Display(Name ="Description")]
        [StringLength(150)]
        public string CurrencyDescription { get; set; }

        [Display(Name ="Exchange rate")]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString ="{0:F3}")]
        public decimal ExchangeRate { get; set; }

        [Display(Name ="Default")]
        public bool CurrencyDefault { get; set; }

        [Display(Name ="Active")]
        public bool CurrencyActive { get; set; }

        public ICollection<Office> Offices { get; set; }
    }
}
