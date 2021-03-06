﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WhoLends.Data;

namespace WhoLends.ViewModels
{
    public class LendViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Von")]
        [Required]
        public DateTime From { get; set; }

        [Display(Name = "Bis")]
        public DateTime? To { get; set; }

        [Display(Name = "Erstellt am")]
        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int LendItemId { get; set; }
        
        public LendItemViewModel SelectedLendItem { get; set; }

        public int UserId { get; set; }

        [Display(Name = "Erstellt von")]
        public User CreatedBy { get; set; }

        [Required]
        public int LenderUserId { get; set; }

        [Display(Name = "Ausleiher")]
        public User SelectedLendUser { get; set; }

        public LendReturnViewModel LendReturn { get; set; }
        
        public IEnumerable<LendItemViewModel> LendItemsList { get; set; }

        public IEnumerable<User> UserList { get; set; }

        [Display(Name = "Ersteller")]
        public string CurrentUserwithID { get; set; }

        public bool ShowLendReturnButton { get; set; }
    }
}