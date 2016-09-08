using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace WhoLends.Models
{
    public class Lend
    {
        [Key]
        public int ID { get; set; }
        public int LendItemId { get; set; }
        //public LendItem LendObject { get; set; }
        
        [DisplayName("Ausgeleiht am")]
        public DateTime From { get; set; }

        [DisplayName("Ausgeleiht bis")]
        public DateTime To { get; set; }

        [DisplayName("Ausleiher / Mitarbeiter")]
        public string Employee { get; set; }

        [DisplayName("Abschluss Kommentar")]
        public string ReviewDescriotionLendItem { get; set; }

        //[DisplayName("Erfasst durch")]
        //public ApplicationUser Creator { get; set; }
    }

    public class LendDBContext : DbContext
    {
        public DbSet<Lend> LendsList { get; set; }

        public LendDBContext()
        {
            Database.SetInitializer<LendDBContext>(null);
        }
    }
}