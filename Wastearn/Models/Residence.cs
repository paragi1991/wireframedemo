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
    
    public partial class Residence
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Residence()
        {
            this.Registrations = new HashSet<Registration>();
        }
    
        public long Id { get; set; }
        public Nullable<long> SocietyId { get; set; }
        public string ResidenceNumber { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Registration> Registrations { get; set; }
        public virtual Society Society { get; set; }
    }
}
