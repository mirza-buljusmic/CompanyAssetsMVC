using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAssetsUI.Models
{
    public class VM_CategorySummary
    {
        [Display(Name ="Category")]
        public string CategoryName { get; set; }
        [Display(Name ="Value")]
        public decimal CategorySum { get; set; }

    }
}
