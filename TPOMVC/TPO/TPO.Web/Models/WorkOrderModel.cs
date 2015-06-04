using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class WorkOrderModel : BaseViewModel
    {
        public int LineID { get; set; }
        public int? TPOProductID { get; set; }
        public int? IMProductID { get; set; }
        public string Code { get; set; }
        public int RunOrder { get; set; }
        public double RunArea { get; set; }
        public bool Pal { get; set; }
        public bool Complete { get; set; }
        public string Comment { get; set; }
        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }

        public string ProductName
        {
            get
            {
                string result = string.Empty;

                if ( TPOProductID.HasValue )
                {
                    result = new TPO.Services.Products.TPOProductService().Get(TPOProductID.Value).ProductCode;
                }
                else if ( IMProductID.HasValue )
                {
                    result = new TPO.Services.Products.IMProductService().Get(IMProductID.Value).Code;
                }

                return result;
            }
        }
        public string ProductDescription
        {
            get
            {
                string result = string.Empty;

                if (TPOProductID.HasValue)
                {
                    result = new TPO.Services.Products.TPOProductService().Get(TPOProductID.Value).ProductDesc;
                }
                else if (IMProductID.HasValue)
                {
                    result = new TPO.Services.Products.IMProductService().Get(IMProductID.Value).Description;
                }

                return result;
            }
        }
    }
}