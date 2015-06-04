using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models 
{
    public class ProductionLineScheduleModel : BaseViewModel
    {
        public int LineId { get; set; }
        public int ShiftId { get; set; }
        public DateTime ProductionDate { get; set; }
        public int MinutesScheduled { get; set; }
    }

    public class ProductionLineScheduleWeekly
    {
        public DateTime FirstDayOfWeek { get; set; }
        public int ShiftId { get; set; }
        public string ShiftCode { get; set; }

        public int Day1 { get; set; }
        public int Day2 { get; set; }
        public int Day3 { get; set; }
        public int Day4 { get; set; }
        public int Day5 { get; set; }
        public int Day6 { get; set; }
        public int Day7 { get; set; }

        public int Day1Id { get; set; }
        public int Day2Id { get; set; }
        public int Day3Id { get; set; }
        public int Day4Id { get; set; }
        public int Day5Id { get; set; }
        public int Day6Id { get; set; }
        public int Day7Id { get; set; }
    }

}