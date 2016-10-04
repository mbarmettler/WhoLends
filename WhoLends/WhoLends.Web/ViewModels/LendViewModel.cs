using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WhoLends.Data;

namespace WhoLends.ViewModels
{
    [MetadataType(typeof(LendHelpter))]
    public partial class Lend { }
    public partial class LendHelpter
    {
        public int Id { get; set; }

        [Display(Name = "Von")]
        [Required]
        public System.DateTime From { get; set; }
        [Display(Name = "Bis")]
        public Nullable<System.DateTime> To { get; set; }
        public IEnumerable<LendItem> LendItemsList { get; set; }
        public IEnumerable<User> UserList { get; set; }   

    }
}