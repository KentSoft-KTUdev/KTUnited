//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI.DataModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class Visit
    {
        public int ID { get; set; }
        public System.DateTime VisitRegDateTime { get; set; }
        public bool IsOver { get; set; }
        public System.DateTime VisitEndDateTime { get; set; }
        public long ResidentPersonalCode { get; set; }
        public long GuardPersonalCode { get; set; }
        public int DormitoryID { get; set; }
    
        public virtual Resident Resident { get; set; }
        public virtual Guard Guard { get; set; }
        public virtual Dormitory Dormitory { get; set; }
        public virtual Guest Guest { get; set; }
    }
}
