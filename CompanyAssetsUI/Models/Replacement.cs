using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAssetsUI.Models
{
    public class Replacement
    {
        [Key]
        public int ReplacementID { get; set; }

        [Display(Name ="Replaced Product")]
        public int CatalogueID { get; set; }

        [Display(Name ="Replacing Product")]
        public int ReplacingID { get; set; }

        public Catalogue Replaced { get; set; }
        public Catalogue Replacing { get; set; }
    }

}
