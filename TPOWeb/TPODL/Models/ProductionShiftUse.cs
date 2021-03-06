//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TPO.DL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductionShiftUse
    {
        public int ID { get; set; }
        public int PlantID { get; set; }
        public int LineID { get; set; }
        public int ShiftID { get; set; }
        public bool UseShift { get; set; }
        public int Day1Minutes { get; set; }
        public int Day2Minutes { get; set; }
        public int Day3Minutes { get; set; }
        public int Day4Minutes { get; set; }
        public int Day5Minutes { get; set; }
        public int Day6Minutes { get; set; }
        public int Day7Minutes { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    
        public virtual Plant Plant { get; set; }
        public virtual ProductionShift ProductionShift { get; set; }
    }
}
