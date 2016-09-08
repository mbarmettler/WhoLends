using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WhoLends.Models
{
    public class LendItem
    {
        [Required]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public int Quanitty { get; set; }
        public ItemCondition Condition { get; set; }
    }
    public enum ItemCondition { _new = 1, used = 2, broken = 3 };

    public class LendItemDBContext : DbContext
    {
        public DbSet<LendItem> LendItemsList { get; set; }
    }
}