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
    
    public partial class LendItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CustomerId { get; set; }
        public short Quantity { get; set; }
        public short Avialable { get; set; }
        public Condition Condition { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public Nullable<int> FileId { get; set; }
        public Nullable<int> DependcyItemId { get; set; }
    
        public virtual User User { get; set; }
        public virtual File File { get; set; }
        public virtual DependcyItems DependcyItems { get; set; }
    }
}
