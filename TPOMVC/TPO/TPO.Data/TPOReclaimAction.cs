//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TPO.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class TPOReclaimAction
    {
        public int ID { get; set; }
        public int PlantID { get; set; }
        public System.DateTime ProductionDate { get; set; }
        public int ProductionShiftId { get; set; }
        public int ProdLineId { get; set; }
        public int ReclaimActionTypeId { get; set; }
        public double ActionAmount { get; set; }
        public double CompAmount { get; set; }
        public string AssocAction { get; set; }
        public string ReclaimType { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    
        public virtual Plant Plant { get; set; }
        public virtual ProdLine ProdLine { get; set; }
        public virtual ProductionShift ProductionShift { get; set; }
        public virtual TPOReclaimActionType TPOReclaimActionType { get; set; }
    }
}
