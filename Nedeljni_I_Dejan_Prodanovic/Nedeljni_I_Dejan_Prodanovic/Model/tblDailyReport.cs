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
    
    public partial class tblDailyReport
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public Nullable<int> ReportID { get; set; }
    
        public virtual tblReport tblReport { get; set; }
        public virtual tblProject tblProject { get; set; }
    }
}
