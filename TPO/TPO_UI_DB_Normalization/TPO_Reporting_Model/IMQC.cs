//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TPO_Reporting_Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class IMQC
    {
        public IMQC()
        {
            this.IMQCDetail = new HashSet<IMQCDetail>();
        }
    
        public int ID { get; set; }
        public int PlantID { get; set; }
        public int LineID { get; set; }
        public int LotID { get; set; }
        public int ProductID { get; set; }
        public Nullable<int> LabTechUserID { get; set; }
        public Nullable<int> OperatorUserID { get; set; }
        public System.DateTime ProductionDate { get; set; }
        public Nullable<double> Color { get; set; }
        public Nullable<bool> Passed { get; set; }
        public string Comment { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual Plant Plant { get; set; }
        public virtual ICollection<IMQCDetail> IMQCDetail { get; set; }
    }
}