﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HospitalSystem_Corona.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PersonalInformationEntities : DbContext
    {
        public PersonalInformationEntities()
            : base("name=PersonalInformationEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<Sickness> Sickness { get; set; }
        public virtual DbSet<Vaccination> Vaccination { get; set; }
    }
}