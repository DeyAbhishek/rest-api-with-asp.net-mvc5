using System;

namespace TPO.Common.DTOs
{
    public class TPOProductDto
    {
        public int ID { get; set; }
        public int PlantID { get; set; }
        public string ProductCode { get; set; }
        public string ProductDesc { get; set; }
        public int ProductLabelID { get; set; }

        public double Thick { get; set; }
        public int ThickUnit { get; set; }
        public double Width { get; set; }
        public double MasRollLength { get; set; }
        public double ContRollLength { get; set; }
        public int LengthWidthUnit { get; set; }
        public int AreaUnit { get; set; }
        public int WeightUnit { get; set; }
        public int ScrimTypeID { get; set; }
        public int QtyMade { get; set; }
        public double WtperArea { get; set; }
        public int RollsPerLotPal { get; set; }
        public int RollsPerLot { get; set; }
        
        public bool Active { get; set; }
        public bool UseCELogo { get; set; }
        public bool IsREPEL { get; set; }

        public string EnteredBy { get; set; }
        public DateTime DateEntered { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime LastModified { get; set; }

        
        
        
        public TPOProductLabelDetailDto TPOProductLabelDetail { get; set; }

        #region Detail Classes
        
        
        
        public class TPOProductLabelDetailDto
        {
            public int ID { get; set; }
            public string Label1 { get; set; }
            public string Label2 { get; set; }
            public string Label3 { get; set; }
        }

        #endregion
    }
}
