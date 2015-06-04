using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Security.AccessControl;
using System.Web;
using TPO.Common.DTOs;
using TPO.Services.Application;

namespace TPO.Web.Models
{
    public class RawMaterialQc : BaseViewModel
    {
        RawMaterialTestModel _rawMaterialTestModel;


        #region Read-Only Properties
        public decimal AshContentTestMinimum { get; set; }

        public decimal AshContentTestMaximum { get; set; }

        public decimal CarbonBlackTestMinimum { get; set; }

        public decimal CarbonBlackTestMaximum { get; set; }

        public decimal ColorTestMinimum { get; set; }

        public decimal ColorTestMaximum { get; set; }

        public decimal MeltFlowTestMinimum { get; set; }

        public decimal MeltFlowTestMaximum { get; set; }

        public decimal MoistureTestMinimum { get; set; }

        public decimal MoistureTestMaximum { get; set; }

        public bool DisplayColorTest
        {
            get { return RawMaterialTest != null && RawMaterialTest.UseColorTest; }
        }
        public bool DisplayMeltFlowTest
        {
            get { return RawMaterialTest != null && RawMaterialTest.UseMFTest; }
        }
        public bool DisplayAshContentTest
        {
            get { return RawMaterialTest != null && RawMaterialTest.UseACTest; }
        }
        public bool DisplayMoistureTest
        {
            get { return RawMaterialTest != null && RawMaterialTest.UseMoistTest; }
        }
        public bool DisplayCarbonBlackTest
        {
            get { return RawMaterialTest != null && RawMaterialTest.UseCBTest; }
        }
        public bool DisplaySpecificGravityTest
        {
            get { return RawMaterialTest != null && RawMaterialTest.UseSpecGrav; }
        }
        public bool DisplayVisualInspectionTest
        {
            get { return RawMaterialTest != null && RawMaterialTest.UseVisual; }
        }

        //public bool IsEditMode { get; set; }

        #endregion

        public RawMaterialTestModel RawMaterialTest
        {
            get { return _rawMaterialTestModel; }
            set
            {
                _rawMaterialTestModel = (value ?? new RawMaterialTestModel());
                AshContentTestMaximum = _rawMaterialTestModel.ACLimit2;
                AshContentTestMinimum = _rawMaterialTestModel.ACLimit1;
                CarbonBlackTestMaximum = _rawMaterialTestModel.CBLimit2;
                CarbonBlackTestMinimum = _rawMaterialTestModel.CBLimit1;
                ColorTestMaximum = _rawMaterialTestModel.ColorLimit2;
                ColorTestMinimum = _rawMaterialTestModel.ColorLimit1;
                MeltFlowTestMaximum = _rawMaterialTestModel.MFLimit2;
                MeltFlowTestMinimum = _rawMaterialTestModel.MFLimit1;
                MoistureTestMaximum = _rawMaterialTestModel.MoistLimit2;
                MoistureTestMinimum = _rawMaterialTestModel.MoistLimit1;
            }
        }

        [DisplayName("QC Tech:")]
        public int? QCTechUserID { get; set; }

        public int RawMaterialReceivedID { get; set; }

        [DisplayName("Lot Number:")]
        public string LotID { get; set; }
        [DisplayName("Box / Car Tested:")]
        public string BoxCarTested { get; set; }

        [DisplayName("Comments:")]
        public string Comments { get; set; }
        [DisplayName("Raw Material:")]
        public string RawMaterialCode { get; set; }

        [DisplayName("Lot Test:")]
        public System.DateTime DateEntered { get; set; }
        [DisplayName("QC Test:")]
        public string EnteredBy { get; set; }
        [DisplayName("Test Date and Time:")]
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal ACCoA { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal ACFS { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal CBCoA { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal CBFS { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal ColorCoA { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal ColorFS { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal MFCoA { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal MFFS { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal MoistCoA { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal MoistFS { get; set; }

        public decimal SpecGrav { get; set; }

        public int QCVisualInspectionID { get; set; }

        public string QcTechName
        {
            get { return (QCTechUserID == null ? "" : new SecurityService().Get(QCTechUserID ?? 0).FullName); }
        }
        
        #region Constructors
        public RawMaterialQc()
            : base()
        {
            
        }
        #endregion

    }
}