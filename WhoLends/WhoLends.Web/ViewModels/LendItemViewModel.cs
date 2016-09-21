using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WhoLends.Data;

namespace WhoLends.Models
{
    [MetadataType(typeof(LendItemHelper))]
    public partial class LendItem { }
    public partial class LendItemHelper
    {
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Beschreibung")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Kunden Geräte-ID")]
        [Required]
        public string CustomerId { get; set; }

        [Display(Name = "Menge")]
        [Required]
        public short Quantity { get; set; }

        [Display(Name = "Zustand")]
        [Required]
        public Condition Condition { get; set; }
    }
}