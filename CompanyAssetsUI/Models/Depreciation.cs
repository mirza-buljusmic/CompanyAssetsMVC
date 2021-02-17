using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAssetsUI.Models
{
    public class Depreciation
    {
        [Key]
        public int DepreciationID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Depreciation")]
        public string DepreciationName { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name ="Description")]
        public string DepreciationDescription { get; set; }

        [Required]
        [Display(Name ="Active")]
        public bool DepreciationActive { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
