using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAssetsUI.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Required]
        [Display(Name = "EOL Months")]
        public int CategoryEOLMonths { get; set; }

        [Display(Name = "Active")]
        public bool CategoryActive { get; set; }

        [StringLength(150)]
        [Display(Name = "Comment")]
        public string CategoryComment { get; set; }

        [Required]
        [Display(Name ="Depreciation")]
        public int DepreciationID { get; set; }

        public Depreciation Depreciation { get; set; }
        public ICollection<Catalogue> Catalogues { get; set; }
    }
}
