using System;
using System.ComponentModel.DataAnnotations;
using WhoLends.Data;

namespace WhoLends.ViewModels
{
    //[MetadataType(typeof(LendReturnViewModel))]
    //public partial class LendReturn { }
    public class LendReturnViewModel
    {
        public int Id { get; set; }
        public int LendId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public int? LRId { get; set; }
        public bool SetComplete { get; set; }

        public User CreatedBy { get; set; }

        [Display(Name = "Ersteller")]
        public string CurrentUserwithID { get; set; }
    }
}