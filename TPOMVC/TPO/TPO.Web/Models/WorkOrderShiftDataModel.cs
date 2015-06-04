using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class WorkOrderShiftDataModel : BaseViewModel
    {
        public int FormulationId { get; set; }
        public int ExtruderId { get; set; }
        
        public string LengthUnitOfMeasure { get; set; }
        public string WeightUnitOfMeasure { get; set; }
        public int RollCount1 { get; set; }

        public double Length1 { get; set; }
        public double Length2 { get; set; }
        public double Length3 { get; set; }
        public double Length4 { get; set; }

        public double ShiftRollWeight { get; set; }
        public double ShiftScrapWeight { get; set; }

        public double Weight1 { get; set; }
        public double Weight2 { get; set; }
        public double Weight3 { get; set; }
        public double Weight4 { get; set; }
        public double WeightTotal 
        {
            get { return Weight1 + Weight2 + Weight3 + Weight4; }
        }

        public double TotalPercent1 
        {
            get 
            {
                double perc = 0;
                if (WeightTotal != 0) 
                {
                    perc = Weight1 / WeightTotal;
                }
                return perc;
            }
        }
        public double TotalPercent2
        {
            get
            {
                double perc = 0;
                if (WeightTotal != 0) 
                {
                    perc = Weight2 / WeightTotal;
                }
                return perc;
            }
        }
        public double TotalPercent3
        {
            get
            {
                double perc = 0;
                if (WeightTotal != 0) 
                {
                    perc = Weight3 / WeightTotal;
                }
                return perc;
            }
        }
        public double TotalPercent4
        {
            get
            {
                double perc = 0;
                if (WeightTotal != 0) 
                {
                    perc = Weight4 / WeightTotal;
                }
                return perc;
            }
        }

        public double ShiftFPY 
        {
            get 
            {
                double fpy = 0;
                if (ShiftRollWeight != 0) 
                {
                    fpy = ShiftRollWeight / (ShiftRollWeight + ShiftScrapWeight);
                }
                return fpy;
            }
        }

        public int DownTimeMinutes { get; set; }
        public int ShiftDownTimeMinutes { get; set; }
        public int ScheduledRunTime { get; set; }
        public double Uptime 
        {
            get 
            {
                double uptime = 100.00;
                if (ScheduledRunTime != 0) 
                {
                    uptime = (ScheduledRunTime - ShiftDownTimeMinutes) / ScheduledRunTime;
                }
                return uptime;
            }
        }

        //scrim/edge trim
        public double ScrimA { get; set; }
        public double ScrimW { get; set; }
        public double Resin { get; set; }



        public bool RMUse { get; set; }
        public int Form { get; set; }
        public int Extruders { get; set; }
    }
}