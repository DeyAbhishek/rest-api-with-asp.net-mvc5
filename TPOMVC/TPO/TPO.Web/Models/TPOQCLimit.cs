using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Models
{
    public class TPOQCLimit : BaseViewModel
    {
        #region Variables
        #endregion

        #region Properties
        public int PropertyID { get; set; }
        public string Code { get; set; }
        public string ProductDescription { get; set; }

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

        public CapCoreDetail TPOProductCapCoreDetail { get; set; }
        public CEDetail TPOProductCEDetail { get; set; }
        public DimDetail TPOProductDimDetail { get; set; }
        public GrabDetail TPOProductGrabDetail { get; set; }
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        #endregion

        #region Events
        #endregion

        #region Event Handlers
        #endregion

        #region Detail Classes
        public class CapCoreDetail
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
        public class CEDetail
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
        public class DimDetail
        {
            public int ID { get; set; }
            public double? DimStab80 { get; set; }
            public double? DimStabTemp { get; set; }
            public double? DimStabTempCE { get; set; }
            public int DimStabTempUoM { get; set; }
        }
        public class GrabDetail
        {
            public int ID { get; set; }
            public double? GrabTensMinMD { get; set; }
            public double? GrabElongMinMD { get; set; }
            public double? GrabTearMin { get; set; }
            public double? GrabTensMinTD { get; set; }
            public double? GrabElongMinTD { get; set; }
            public double? GrabTearMinTD { get; set; }
        }
        #endregion
    }
}