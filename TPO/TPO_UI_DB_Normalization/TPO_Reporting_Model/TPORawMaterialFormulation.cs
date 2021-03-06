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
    
    public partial class TPORawMaterialFormulation
    {
        public int ID { get; set; }
        public int PlantID { get; set; }
        public int FormulationID { get; set; }
        public int RawMaterialID { get; set; }
        public int TPOExtruderID { get; set; }
        public int FeederNumber { get; set; }
        public double PercentComplete { get; set; }
        public Nullable<bool> Delete { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    
        public virtual Plant Plant { get; set; }
        public virtual TPOExtruder TPOExtruder { get; set; }
        public virtual TPOFormulation TPOFormulation { get; set; }
    }
}
