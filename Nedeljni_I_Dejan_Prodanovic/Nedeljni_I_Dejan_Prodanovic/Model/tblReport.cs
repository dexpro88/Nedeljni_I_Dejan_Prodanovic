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
    
    public partial class tblReport
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblReport()
        {
            this.tblDailyReports = new HashSet<tblDailyReport>();
        }
    
        public int ReportID { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public Nullable<int> EmployeeID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblDailyReport> tblDailyReports { get; set; }
        public virtual tblEmployee tblEmployee { get; set; }
    }
}
