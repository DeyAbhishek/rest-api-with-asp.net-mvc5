using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class WorkOrderShiftDataIMModel : BaseViewModel
    {
        public string RawMatID { get; set; }
        public double QtyUsed { get; set; }
        public string RMUoM { get; set; }

        public string PUoM { get; set; }
        public int GoodCartons { get; set; }
        public int GoodParts { get; set; }
        public double GoodWt { get; set; }

        public double GoodPerc 
        {
            get 
            {
                double goodPerc = 0;
                if (GoodWt != 0) 
                {
                    goodPerc = GoodWt / GoodWt + ScWt2 + ScWt3 + ScWt4;
                }
                return goodPerc;
            }
        }

        public int ScParts2 { get; set; }
        public double ScWt2 { get; set; }

        public double ScPerc2 
        {
            get 
            {
                double scPerc2 = 0;

                if (ScWt2 != 0)
                {
                    scPerc2 = ScWt2 / GoodWt + ScWt2 + ScWt3 + ScWt4;
                }

                return scPerc2;
            } 
        }

        public int ScParts3 { get; set; }
        public double ScWt3 { get; set; }

        public double ScPerc3 
        {
            get 
            {
                double scPerc3 = 0;

                if (ScWt3 != 0)
                {
                    scPerc3 = ScWt3 / GoodWt + ScWt2 + ScWt3 + ScWt4;
                }

                return scPerc3;
            }
        }

        public int ScParts4 { get; set; }
        public double ScWt4 { get; set; }

        public double ScPerc4 
        {
            get 
            {
                double scPerc4 = 0;

                if (ScWt4 != 0)
                {
                    scPerc4 = ScWt4 / GoodWt + ScWt2 + ScWt3 + ScWt4;
                }

                return scPerc4;
            }
        }

        public int TtlDT { get; set; }

        public int ShiftDT { get; set; }
        public int SchedTime { get; set; }

        public double UptimeIM 
        {
            get 
            {
                double uptimeIM = 100.00;

                if (SchedTime != 0 || ShiftDT !=0) 
                {
                    uptimeIM = (SchedTime - ShiftDT) / SchedTime;
                }

                return uptimeIM;
            }
        }

        public double ShiftFPYIM 
        {
            get 
            {
                double shiftFPYIM = 0;

                //insert logic


                return shiftFPYIM;
            }
        }
    }
}