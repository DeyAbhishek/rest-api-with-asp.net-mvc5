using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Model.Reference;
using TPO.Model.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TPO.Model.CustomAttributes;

namespace TPO.Model.RawMaterials
{
    public class RawMaterialQCModel : TPOModelBase
    {
        #region Variables
        RawMaterialTestModel _rawMaterialTestModel;
        #endregion

        #region Properties

        #region Read-Only Properties

        public bool DisplayColorTest 
        {
            get { return QCConfiguration != null ? QCConfiguration.UseColorTest : false; }
        }
        public bool DisplayMeltFlowTest 
        {
            get { return QCConfiguration != null ? QCConfiguration.UseMeltFlowTest : false; }
        }
        public bool DisplayAshContentTest 
        {
            get { return QCConfiguration != null ? QCConfiguration.UseAshContentTest : false; }
        }
        public bool DisplayMoistureTest 
        {
            get { return QCConfiguration != null ? QCConfiguration.UseMoistureTest : false; }
        }
        public bool DisplayCarbonBlackTest 
        {
            get { return QCConfiguration != null ? QCConfiguration.UseCarbonBlackTest : false; }
        }
        public bool DisplaySpecificGravityTest 
        {
            get { return QCConfiguration != null ? QCConfiguration.UseSpecificGravityTest : false; }
        }
        public bool DisplayVisualInspectionTest 
        {
            get { return QCConfiguration != null ? QCConfiguration.UseVisualInspectionTest : false; }
        }

        //public bool IsEditMode { get; set; }


        #endregion


        public RawMaterialTestModel QCConfiguration
        {
            get { return _rawMaterialTestModel;}
            set 
            {
                _rawMaterialTestModel = (value == null ? new RawMaterialTestModel() : value);
                AshContentTestMaximum = _rawMaterialTestModel.AshContentMaximum;
                AshContentTestMinimum = _rawMaterialTestModel.AshContentMinimum;
                CarbonBlackTestMaximum = _rawMaterialTestModel.CarbonBlackMaximum;
                CarbonBlackTestMinimum = _rawMaterialTestModel.CarbonBlackMinimum;
                ColorTestMaximum = _rawMaterialTestModel.ColorMaximum;
                ColorTestMinimum = _rawMaterialTestModel.ColorMinimum;
                MeltFlowTestMaximum = _rawMaterialTestModel.MeltFlowMaximum;
                MeltFlowTestMinimum = _rawMaterialTestModel.MeltFlowMinimum;
                MoistureTestMaximum = _rawMaterialTestModel.MoistureMaximum;
                MoistureTestMinimum = _rawMaterialTestModel.MoistureMinimum;
            }
        }

        public int PlantID { get; set; }

        [DisplayName("QC Tech")]
        public int QCTechID { get; set; }

        public Int32 RawMaterialReceivedID { get; set; }

        [DisplayName("Raw Material")]
        [Required(ErrorMessage = "The Raw Material field is required")]
        public string RawMaterialCode { get; set; }
        [DisplayName("Lot Number")]
        public string LotCode { get; set; }
        [DisplayName("Box / Car Tested")]
        public string BoxCar { get; set; }

        [Required(ErrorMessage = "The Comments field is required")]
        [DisplayName("Comments")]
        public string Comments { get; set; }

        [DisplayName("Lot Test")]
        public System.DateTime DateEntered { get; set; }
        [DisplayName("QC Test")]
        public string EnteredBy { get; set; }
        [DisplayName("Test Date and Time")]
        public System.DateTime LastModified { get; set; }

        [Required]
        [Range(0, 9999999999999999.99)]
        public decimal AshContentTestCoA { get; set; }

        [Required]
        [Range(0, 9999999999999999.99)]
        public decimal AshContentTestFS { get; set; }

        [Required]
        [Range(0, 9999999999999999.99)]
        public decimal CarbonBlackTestCoA { get; set; }

        [Required]
        [Range(0, 9999999999999999.99)]
        public decimal CarbonBlackTestFS { get; set; }

        [Required]
        [Range(0, 9999999999999999.99)]
        public decimal ColorTestCoA { get; set; }

        [Required]
        [Range(0, 9999999999999999.99)]
        public decimal ColorTestFS { get; set; }

        [Required]
        [Range(0, 9999999999999999.99)]
        public decimal MeltFlowTestCoA { get; set; }

        [Required]
        [Range(0, 9999999999999999.99)]
        public decimal MeltFlowTestFS { get; set; }

        [Required]
        [Range(0, 9999999999999999.99)]
        public decimal MoistureTestCoA { get; set; }

        [Required]
        [Range(0, 9999999999999999.99)]
        public decimal MoistureTestFS { get; set; }

        public decimal SpecificGravity { get; set; }

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
        
        public bool VisuallyInspected { get; set; }


        #endregion

        #region Constructors
        public RawMaterialQCModel() :base()
        {
            
        }
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
    }
}
