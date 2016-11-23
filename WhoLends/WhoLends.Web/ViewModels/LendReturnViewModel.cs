﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WhoLends.Data;

namespace WhoLends.ViewModels
{
    public class LendReturnViewModel
    {
        public int Id { get; set; }
        public int LendId { get; set; }
        [Required(ErrorMessage = "Bitte kurz eine Beschreibung der Ware bei Rückgabe geben (z.B. Verschleiss)")]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        [Display(Name="Set komplett?")]
        public bool? SetComplete { get; set; }
        public int UserId { get; set; }
        public User CreatedBy { get; set; }
        [Display(Name = "Ersteller")]
        public string CurrentUserwithID { get; set; }
        public IEnumerable<FileViewModel> ReturnImageViewModels { get; set; }
        public int FileId { get; set; }
    }
}