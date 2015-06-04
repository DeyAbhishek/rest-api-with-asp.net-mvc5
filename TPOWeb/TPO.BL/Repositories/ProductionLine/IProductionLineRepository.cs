using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Model.Production;

namespace TPO.BL.Repositories.ProductionLine
{
    public interface IProductionLineRepository
    {
        List<ProductionLineModel> GetProductionLines();
        List<ProductionLineModel> GetProductionLines(int PlantId);
    }
}
