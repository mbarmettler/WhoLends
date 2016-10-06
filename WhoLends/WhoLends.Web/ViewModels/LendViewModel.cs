using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WhoLends.Data;

namespace WhoLends.ViewModels
{
    [MetadataType(typeof(LendViewModel))]
    public partial class Lend { }
    public partial class LendViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Von")]
        [Required]
        public System.DateTime From { get; set; }
        [Display(Name = "Bis")]
        public Nullable<System.DateTime> To { get; set; }

        [Required]
        public System.DateTime CreatedAt { get; set; }
        [Required]
        public int LendItemId { get; set; }

        public LendItemViewModel SelectedLendItem { get; set; }

        public int UserId { get; set; }
        [Required]
        public int LenderUserId { get; set; }

        public User SelectedLendUser { get; set; }

        public IEnumerable<Data.LendItem> LendItemsList { get; set; }
        public IEnumerable<Data.User> UserList { get; set; }   

    }
}