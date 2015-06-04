using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class TPOMonthlyProductionBudgetModel : BaseViewModel
    {
        public int ID { get; set; }
        public int TypeID { get; set; }
        public int PlantID { get; set; }
        public int Month { get; set; }
       
        public string MonthName
        {
            get
            {
                if (Month == 1)
                {
                   return "Jan";
                }
                else if (Month == 2)
                {
                    return "Feb";
                }
                else if (Month == 3)
                {
                    return "Mar";
                }
                else if (Month == 4)
                {
                    return "Apr";
                }
                else if (Month == 5)
                {
                    return "May";
                }
                else if (Month == 6)
                {
                    return "Jun";
                }
                else if (Month == 7)
                {
                    return "Jul";
                }
                else if (Month == 8)
                {
                    return "Aug";
                }
                else if (Month == 9)
                {
                    return "Sep";
                }
                else if (Month == 10)
                {
                    return "Oct";
                }
                else if (Month == 11)
                {
                    return "Nov";
                }
                else if (Month == 12)
                {
                    return "Dec";
                }
                else
                {
                    return "Error";
                }
            }
        }
        public int Year { get; set; }
        public double Budget { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    }
}