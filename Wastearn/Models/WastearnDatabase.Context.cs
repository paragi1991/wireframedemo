﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WastearnEntities : DbContext
    {
        public WastearnEntities()
            : base("name=WastearnEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Residence> Residences { get; set; }
        public virtual DbSet<Society> Societies { get; set; }
    }
}
