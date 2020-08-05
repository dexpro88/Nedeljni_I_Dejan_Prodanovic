//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class tblEmployee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblEmployee()
        {
            this.tblReports = new HashSet<tblReport>();
            this.tblRequestForChanges = new HashSet<tblRequestForChange>();
            this.tblRequestForChanges1 = new HashSet<tblRequestForChange>();
        }
    
        public int EmployeeID { get; set; }
        public string YearsOfService { get; set; }
        public string JMBG { get; set; }
        public string OrderStatus { get; set; }
        public Nullable<decimal> Salary { get; set; }
        public string ProfessionalQualifications { get; set; }
        public Nullable<int> SectorID { get; set; }
        public Nullable<int> PositionID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> ManagerID { get; set; }
    
        public virtual tblManager tblManager { get; set; }
        public virtual tblPosition tblPosition { get; set; }
        public virtual tblSector tblSector { get; set; }
        public virtual tblUser tblUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblReport> tblReports { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblRequestForChange> tblRequestForChanges { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblRequestForChange> tblRequestForChanges1 { get; set; }
    }
}
