﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Comment> Comment { get; set; }
        public DbSet<DownTime> DownTime { get; set; }
        public DbSet<DownTimeEquipment> DownTimeEquipment { get; set; }
        public DbSet<DownTimeReason> DownTimeReason { get; set; }
        public DbSet<DownTimeType> DownTimeType { get; set; }
        public DbSet<EndOfRun> EndOfRun { get; set; }
        public DbSet<EndOfRunRework> EndOfRunRework { get; set; }
        public DbSet<EndOfRunTPOFeeder> EndOfRunTPOFeeder { get; set; }
        public DbSet<FailProperty> FailProperty { get; set; }
        public DbSet<IMLineScrap> IMLineScrap { get; set; }
        public DbSet<IMLineScrapReason> IMLineScrapReason { get; set; }
        public DbSet<IMLineScrapType> IMLineScrapType { get; set; }
        public DbSet<IMProduction> IMProduction { get; set; }
        public DbSet<IMQC> IMQC { get; set; }
        public DbSet<IMQCDetail> IMQCDetail { get; set; }
        public DbSet<Plant> Plant { get; set; }
        public DbSet<ProductionBudget> ProductionBudget { get; set; }
        public DbSet<ProductionBudgetType> ProductionBudgetType { get; set; }
        public DbSet<ProductionLineSchedule> ProductionLineSchedule { get; set; }
        public DbSet<ProductionShift> ProductionShift { get; set; }
        public DbSet<ProductionShiftType> ProductionShiftType { get; set; }
        public DbSet<ProductionShiftUse> ProductionShiftUse { get; set; }
        public DbSet<QCKickout> QCKickout { get; set; }
        public DbSet<QCPrintSpecification> QCPrintSpecification { get; set; }
        public DbSet<QCPrintSpecificationType> QCPrintSpecificationType { get; set; }
        public DbSet<QCRedHold> QCRedHold { get; set; }
        public DbSet<RawMaterialAction> RawMaterialAction { get; set; }
        public DbSet<RawMaterialActionReason> RawMaterialActionReason { get; set; }
        public DbSet<RawMaterialActionType> RawMaterialActionType { get; set; }
        public DbSet<RawMaterialQC> RawMaterialQC { get; set; }
        public DbSet<RawMaterialReceived> RawMaterialReceived { get; set; }
        public DbSet<RawMaterialReceivedSizeLimit> RawMaterialReceivedSizeLimit { get; set; }
        public DbSet<RawMaterialTest> RawMaterialTest { get; set; }
        public DbSet<ReclaimType> ReclaimType { get; set; }
        public DbSet<ScrimAction> ScrimAction { get; set; }
        public DbSet<ScrimActionReason> ScrimActionReason { get; set; }
        public DbSet<ScrimActionType> ScrimActionType { get; set; }
        public DbSet<ScrimRoll> ScrimRoll { get; set; }
        public DbSet<ScrimType> ScrimType { get; set; }
        public DbSet<SupervisorOnShift> SupervisorOnShift { get; set; }
        public DbSet<TestLimitType> TestLimitType { get; set; }
        public DbSet<TPOBatch> TPOBatch { get; set; }
        public DbSet<TPOCProductRoll> TPOCProductRoll { get; set; }
        public DbSet<TPOExtruder> TPOExtruder { get; set; }
        public DbSet<TPOFormulation> TPOFormulation { get; set; }
        public DbSet<TPOFormulationLineProduct> TPOFormulationLineProduct { get; set; }
        public DbSet<TPOLineScrap> TPOLineScrap { get; set; }
        public DbSet<TPOLineScrapCode> TPOLineScrapCode { get; set; }
        public DbSet<TPOLineScrapType> TPOLineScrapType { get; set; }
        public DbSet<TPOQC> TPOQC { get; set; }
        public DbSet<TPOQC2X2Detail> TPOQC2X2Detail { get; set; }
        public DbSet<TPOQCAshDetail> TPOQCAshDetail { get; set; }
        public DbSet<TPOQCCapThicknessDetail> TPOQCCapThicknessDetail { get; set; }
        public DbSet<TPOQCColorDetail> TPOQCColorDetail { get; set; }
        public DbSet<TPOQCCoreThicknessDetail> TPOQCCoreThicknessDetail { get; set; }
        public DbSet<TPOQCDimensionalStabilityDetail> TPOQCDimensionalStabilityDetail { get; set; }
        public DbSet<TPOQCFlashElongationDetail> TPOQCFlashElongationDetail { get; set; }
        public DbSet<TPOQCFlashTearingDetail> TPOQCFlashTearingDetail { get; set; }
        public DbSet<TPOQCFlashTensileDetail> TPOQCFlashTensileDetail { get; set; }
        public DbSet<TPOQCGrabElongationDetail> TPOQCGrabElongationDetail { get; set; }
        public DbSet<TPOQCGrabTearingDetail> TPOQCGrabTearingDetail { get; set; }
        public DbSet<TPOQCGrabTensileDetail> TPOQCGrabTensileDetail { get; set; }
        public DbSet<TPOQCGumDetail> TPOQCGumDetail { get; set; }
        public DbSet<TPOQCOverallThicknessDetail> TPOQCOverallThicknessDetail { get; set; }
        public DbSet<TPOQCPlyDetail> TPOQCPlyDetail { get; set; }
        public DbSet<TPOQCPrintMeasurementDetail> TPOQCPrintMeasurementDetail { get; set; }
        public DbSet<TPOQCPunctureDetail> TPOQCPunctureDetail { get; set; }
        public DbSet<TPOQCSpecificGravityDetail> TPOQCSpecificGravityDetail { get; set; }
        public DbSet<TPOQCThicknessFromEdgeDetail> TPOQCThicknessFromEdgeDetail { get; set; }
        public DbSet<TPOQCType> TPOQCType { get; set; }
        public DbSet<TPOQCWeldStrengthDetail> TPOQCWeldStrengthDetail { get; set; }
        public DbSet<TPORawMaterialFormulation> TPORawMaterialFormulation { get; set; }
        public DbSet<TPOReclaimAction> TPOReclaimAction { get; set; }
        public DbSet<TPOReclaimActionType> TPOReclaimActionType { get; set; }
        public DbSet<TPOReclaimWIP> TPOReclaimWIP { get; set; }
        public DbSet<TPOReworkAction> TPOReworkAction { get; set; }
        public DbSet<TPOReworkActionType> TPOReworkActionType { get; set; }
        public DbSet<TPOReworkRoll> TPOReworkRoll { get; set; }
        public DbSet<TPOSpecificGravity> TPOSpecificGravity { get; set; }
        public DbSet<TPOSpecificGravityWeightDetail> TPOSpecificGravityWeightDetail { get; set; }
        public DbSet<TPOSpecificGravityWeightDetailType> TPOSpecificGravityWeightDetailType { get; set; }
        public DbSet<UnitOfMeasure> UnitOfMeasure { get; set; }
        public DbSet<UnitOfMeasureConversion> UnitOfMeasureConversion { get; set; }
        public DbSet<UnitOfMeasureDefault> UnitOfMeasureDefault { get; set; }
        public DbSet<UnitOfMeasureType> UnitOfMeasureType { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<WorkOrder> WorkOrder { get; set; }
        public DbSet<Role> RoleSet { get; set; }
        public DbSet<RoleAssignment> RoleAssignmentSet { get; set; }
        public DbSet<SecuredArea> SecuredAreaSet { get; set; }
    }
}
