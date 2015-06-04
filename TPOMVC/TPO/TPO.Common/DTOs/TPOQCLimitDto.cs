using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.DTOs
{
    public class TPOQCLimitDto
    {
        public int ID { get; set; }
        public int PlantID { get; set; }
        public int PropertyID { get; set; }
        public string Code { get; set; }
        public string ProductDescription { get; set; }
        
        //public int ProductCapCoreId { get; set; }
        //public int ProductGrabID { get; set; }
        //public int ProductCEID { get; set; }
        //public int ProductDimId { get; set; }

        public int ThickTestUoM { get; set; }
        public int ThickTestUoM2 { get; set; }
        public int WidthTestUoM { get; set; }
        public int ForceTestUoM { get; set; }
        public int PunctUoM { get; set; }

        public bool UseSecThick { get; set; }
        public double? ThickMin { get; set; }
        public double? ThickMax { get; set; }
        public double? ThickAvg { get; set; }
        public double? WidthMin { get; set; }
        public double? WidthMax { get; set; }
        public double? ThickAvgMin { get; set; }
        public double? ThickAvgMax { get; set; }
        public double? GEWidthMin { get; set; }
        public double? GEWidthMax { get; set; }
        public double? GEThicknessMin { get; set; }
        public double? PlyAdhMin { get; set; }
        public double? PlyAdhAvg { get; set; }
        public double? WeldTestMin { get; set; }
        public double? WeldTestMax { get; set; }
        public double? PunctMinTd { get; set; }
        public double? DimStab { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime LastModified { get; set; }

        public TPOProductCapCoreDetailDto TPOProductCapCoreDetail { get; set; }
        public TPOProductDimDetailDto TPOProductDimDetail { get; set; }
        public TPOProductGrabDetailDto TPOProductGrabDetail { get; set; }
        public TPOProductCEDetailDto TPOProductCeDetail { get; set; }

        #region Detail Classes
        public class TPOProductCapCoreDetailDto
        {
            public int ID { get; set; }
            public double? CapThickMin { get; set; }
            public double? CapThickAvg { get; set; }
            public double? CapThickMax { get; set; }
            public double? CoreThickMin { get; set; }
            public double? CoreThickAvg { get; set; }
            public double? CoreThickMax { get; set; }
            public double? CapAshMin { get; set; }
            public double? CapAshMax { get; set; }
            public double? CoreAshMin { get; set; }
            public double? CoreAshMax { get; set; }
        }
        public class TPOProductDimDetailDto
        {
            public int ID { get; set; }
            public double? DimStab80 { get; set; }
            public double? DimStabTemp { get; set; }
            public double? DimStabTempCE { get; set; }
            public int DimStabTempUoM { get; set; }
        }
        public class TPOProductGrabDetailDto
        {
            public int ID { get; set; }
            public double? GrabTensMinMD { get; set; }
            public double? GrabElongMinMD { get; set; }
            public double? GrabTearMin { get; set; }
            public double? GrabTensMinTD { get; set; }
            public double? GrabElongMinTD { get; set; }
            public double? GrabTearMinTD { get; set; }
        }
        public class TPOProductCEDetailDto
        {
            public int ID { get; set; }
            public int? CEForceTestUoM { get; set; }
            public double? CETensMinMD { get; set; }
            public double? CEElongMinMD { get; set; }
            public double? CETensMinTD { get; set; }
            public double? CEElongMinTD { get; set; }
            public double? CETearMinMD { get; set; }
            public double? CETearMinTD { get; set; }
        }
        #endregion
    }
}
