//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wastearn.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Registration
    {
        public long Id { get; set; }
        public Nullable<decimal> ContactNumber { get; set; }
        public string UserName { get; set; }
        public Nullable<decimal> MPIN { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<long> ResidenceId { get; set; }
        public Nullable<bool> isVisible { get; set; }
    
        public virtual Residence Residence { get; set; }
    }
}