using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Model.Production;

namespace TPO.BL.Production
{
    public class ProductionLine
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        #region Retrieval Methods
        public List<ProductionLineModel> GetProductionLines() 
        {
            List<ProductionLineModel> data = new List<ProductionLineModel>();
            data.Add(new ProductionLineModel() { Code = "Shoals TPO" });
            data.Add(new ProductionLineModel() { Code = "Coating" });
            data.Add(new ProductionLineModel() { Code = "Erema" });
            return data;
        }
        #endregion

        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods

        #region Bind Methods
        #endregion

        #endregion

        #region Events
        #endregion

        #region Event Handlers
        #endregion
    }


    public class ProductionLine1
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        #region Retrieval Methods
        public List<ProductionLineModel> GetProductionLines()
        {
            List<ProductionLineModel> data = new List<ProductionLineModel>();
            data.Add(new ProductionLineModel() { Code = "CA10A" });
            data.Add(new ProductionLineModel() { Code = "Cal Sterate FN PWD" });
            data.Add(new ProductionLineModel() { Code = "CM2157 – Tan" });
            return data;
        }
        #endregion

        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods

        #region Bind Methods
        #endregion

        #endregion

        #region Events
        #endregion

        #region Event Handlers
        #endregion
    }
}
