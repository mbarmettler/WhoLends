//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WhoLends.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class LendReturn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LendReturn()
        {
            this.File = new HashSet<File>();
            this.Lend = new HashSet<Lend>();
        }
    
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public bool SetComplete { get; set; }
        public int LendId { get; set; }
    
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<File> File { get; set; }
        public virtual Lend Lend1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lend> Lend { get; set; }
    }
}
