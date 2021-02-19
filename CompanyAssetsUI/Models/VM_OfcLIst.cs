using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAssetsUI.Models
{
    public class VM_OfcLIst
    {
        [Display(Name ="Office")]
        public string OfficeName { get; set; }
        [Display(Name ="Value")]
        public decimal AssetSum { get; set; }

    }
}
