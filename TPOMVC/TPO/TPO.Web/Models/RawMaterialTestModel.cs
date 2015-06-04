using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPO.Common.DTOs;
using TPO.Services.RawMaterials;

namespace TPO.Web.Models
{
    public class RawMaterialTestModel : BaseViewModel
    {
        #region Constructors
        public RawMaterialTestModel() : base() 
        {
            using (TestLimitTypeService service = new TestLimitTypeService())
            {
                _testLimitTypes = Mapper.Map<List<TestLimitTypeDto>, List<TestLimitTypeModel>>(service.GetAll());
            }
        }
        #endregion

        public TestLimitTypeModel ColorLimitType { get; set; }
        public TestLimitTypeModel MeltFlowLimitType { get; set; }
        public TestLimitTypeModel AshContentLimitType { get; set; }
        public TestLimitTypeModel MoistureLimitType { get; set; }
        public TestLimitTypeModel CarbonBlackLimitType { get; set; }

        [DisplayName("Testing Frequency:")]
        public string TestFrequency { get; set; }
        public int RawMaterialId { get; set; }

        [DisplayName("Limit Type:")]
        public int? ColorLimitTypeID { get; set; }
        [DisplayName("Limit Type:")]
        public int? MFLimitTypeID { get; set; }
        [DisplayName("Limit Type:")]
        public int? ACLimitTypeID { get; set; }
        [DisplayName("Limit Type:")]
        public int? MoistLimitTypeID { get; set; }
        [DisplayName("Limit Type:")]
        public int? CBLimitTypeID { get; set; }

        [DisplayName("Min:")]
        public decimal ColorLimit1 { get; set; }
        [DisplayName("Max:")]
        public decimal ColorLimit2 { get; set; }
        [DisplayName("Min:")]
        public decimal MFLimit1 { get; set; }
        [DisplayName("Max:")]
        public decimal MFLimit2 { get; set; }
        [DisplayName("Min:")]
        public decimal ACLimit1 { get; set; }
        [DisplayName("Max:")]
        public decimal ACLimit2 { get; set; }
        [DisplayName("Min:")]
        public decimal MoistLimit1 { get; set; }
        [DisplayName("Max:")]
        public decimal MoistLimit2 { get; set; }
        [DisplayName("Min:")]
        public decimal CBLimit1 { get; set; }
        [DisplayName("Max:")]
        public decimal CBLimit2 { get; set; }

        [DisplayName("Color, Y1")]
        public bool UseColorTest { get; set; }
        [DisplayName("Melt Flow")]
        public bool UseMFTest { get; set; }
        [DisplayName("Ash Content %")]
        public bool UseACTest { get; set; }
        [DisplayName("Moisture %")]
        public bool UseMoistTest { get; set; }
        [DisplayName("Carbon Black %")]
        public bool UseCBTest { get; set; }
        [DisplayName("Specific Gravity")]
        public bool UseSpecGrav { get; set; }
        [DisplayName("Visual Inspection")]
        public bool UseVisual { get; set; }

        private readonly List<TestLimitTypeModel> _testLimitTypes;
 
        public IEnumerable<SelectListItem> TestLimitTypes
        {
            get { return new SelectList(_testLimitTypes, "ID", "Description"); }
        }
    }
}