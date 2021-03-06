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
    
    public partial class ProductionShift
    {
        public ProductionShift()
        {
            this.Comment = new HashSet<Comment>();
            this.DownTime = new HashSet<DownTime>();
            this.EndOfRun = new HashSet<EndOfRun>();
            this.EndOfRunRework = new HashSet<EndOfRunRework>();
            this.EndOfRunTPOFeeder = new HashSet<EndOfRunTPOFeeder>();
            this.IMLineScrap = new HashSet<IMLineScrap>();
            this.IMProduction = new HashSet<IMProduction>();
            this.ProductionLineSchedule = new HashSet<ProductionLineSchedule>();
            this.ProductionShiftUse = new HashSet<ProductionShiftUse>();
            this.QCKickout = new HashSet<QCKickout>();
            this.RawMaterialAction = new HashSet<RawMaterialAction>();
            this.ScrimAction = new HashSet<ScrimAction>();
            this.SupervisorOnShift = new HashSet<SupervisorOnShift>();
            this.TPOCProductRoll = new HashSet<TPOCProductRoll>();
            this.TPOLineScrap = new HashSet<TPOLineScrap>();
            this.TPOQC = new HashSet<TPOQC>();
            this.TPOReclaimAction = new HashSet<TPOReclaimAction>();
            this.TPOReworkAction = new HashSet<TPOReworkAction>();
            this.TPOReworkRoll = new HashSet<TPOReworkRoll>();
        }
    
        public int ID { get; set; }
        public int PlantID { get; set; }
        public int TypeID { get; set; }
        public string Code { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<DownTime> DownTime { get; set; }
        public virtual ICollection<EndOfRun> EndOfRun { get; set; }
        public virtual ICollection<EndOfRunRework> EndOfRunRework { get; set; }
        public virtual ICollection<EndOfRunTPOFeeder> EndOfRunTPOFeeder { get; set; }
        public virtual ICollection<IMLineScrap> IMLineScrap { get; set; }
        public virtual ICollection<IMProduction> IMProduction { get; set; }
        public virtual Plant Plant { get; set; }
        public virtual ICollection<ProductionLineSchedule> ProductionLineSchedule { get; set; }
        public virtual ProductionShiftType ProductionShiftType { get; set; }
        public virtual ICollection<ProductionShiftUse> ProductionShiftUse { get; set; }
        public virtual ICollection<QCKickout> QCKickout { get; set; }
        public virtual ICollection<RawMaterialAction> RawMaterialAction { get; set; }
        public virtual ICollection<ScrimAction> ScrimAction { get; set; }
        public virtual ICollection<SupervisorOnShift> SupervisorOnShift { get; set; }
        public virtual ICollection<TPOCProductRoll> TPOCProductRoll { get; set; }
        public virtual ICollection<TPOLineScrap> TPOLineScrap { get; set; }
        public virtual ICollection<TPOQC> TPOQC { get; set; }
        public virtual ICollection<TPOReclaimAction> TPOReclaimAction { get; set; }
        public virtual ICollection<TPOReworkAction> TPOReworkAction { get; set; }
        public virtual ICollection<TPOReworkRoll> TPOReworkRoll { get; set; }
    }
}
