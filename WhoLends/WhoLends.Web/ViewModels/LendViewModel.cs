using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WhoLends.Models
{
    [MetadataType(typeof(LendHelpter))]
    public partial class Lend { }
    public partial class LendHelpter
    {
        public int Id { get; set; }

        [Display(Name = "Von")]
        public System.DateTime From { get; set; }
        [Display(Name = "Bis")]
        public Nullable<System.DateTime> To { get; set; }
    }
}