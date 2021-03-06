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
    
    public partial class RawMaterialAction
    {
        public int ID { get; set; }
        public int PlantID { get; set; }
        public int TypeID { get; set; }
        public Nullable<int> ReasonID { get; set; }
        public Nullable<int> LineID { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
        public Nullable<int> ShiftID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> LotNumber { get; set; }
        public Nullable<System.DateTime> ProductionDate { get; set; }
        public double Quantity { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
        public int RawMaterialID { get; set; }
        public string ActionReasonText { get; set; }
    
        public virtual Plant Plant { get; set; }
        public virtual ProdLine ProdLine { get; set; }
        public virtual ProductionShift ProductionShift { get; set; }
        public virtual RawMaterialActionReason RawMaterialActionReason { get; set; }
        public virtual RawMaterialActionType RawMaterialActionType { get; set; }
        public virtual RawMaterial RawMaterial { get; set; }
        public virtual User User { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }
    }
}
