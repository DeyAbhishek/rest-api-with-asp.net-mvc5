using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Model.Production;

namespace TPO.BL.Repositories.ProductionLine
{
    public class ProductionLineRepository : IProductionLineRepository
    {
        public List<ProductionLineModel> GetProductionLines()
        {
            // MOCK DATA FOR SAP
            return new List<ProductionLineModel>
            {
                new ProductionLineModel() {Code = "Shoals TPO"},
                new ProductionLineModel() {Code = "Coating"},
                new ProductionLineModel() {Code = "Erema"}
            };
        }

        public List<ProductionLineModel> GetProductionLines(int plantId)
        {
            // Mock data for SAP.
            return new List<ProductionLineModel>
            {
                new ProductionLineModel() {Code = "Shoals TPO"},
                new ProductionLineModel() {Code = "Coating"},
                new ProductionLineModel() {Code = "Erema"}
            };   
        }
    }
}
