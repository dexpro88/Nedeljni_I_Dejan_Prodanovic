﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nedeljni_I_Dejan_Prodanovic.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CompanyDataEntities : DbContext
    {
        public CompanyDataEntities()
            : base("name=CompanyDataEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblAdmin> tblAdmins { get; set; }
        public virtual DbSet<tblDailyReport> tblDailyReports { get; set; }
        public virtual DbSet<tblEmployee> tblEmployees { get; set; }
        public virtual DbSet<tblManager> tblManagers { get; set; }
        public virtual DbSet<tblPosition> tblPositions { get; set; }
        public virtual DbSet<tblProject> tblProjects { get; set; }
        public virtual DbSet<tblReport> tblReports { get; set; }
        public virtual DbSet<tblRequestForChange> tblRequestForChanges { get; set; }
        public virtual DbSet<tblSector> tblSectors { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
    }
}
