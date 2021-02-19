using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAssetsUI.Models
{
    public class VM_PurchaseList
    {
        [Display(Name = "Office")]
        public string OfficeName { get; set; }
        [Display(Name = "Asset")]
        public string AssetName { get; set; }
        public string ReplacedBy { get; set; }
        public int Count { get; set; }
    }
}
