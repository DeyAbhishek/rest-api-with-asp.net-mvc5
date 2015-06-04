using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;

namespace TPO.Services.Production
{
    public class SupervisorOnShiftService : ServiceBaseGeneric<SupervisorOnShiftDto, SupervisorOnShift>, ITpoService<SupervisorOnShiftDto>
    {
        public SupervisorOnShiftDto GetByPlantLineShiftDate(int plantId, int lineId, int shiftId, DateTime productionDate)
        {
            
                  return MapEntity( _repository.Repository<SupervisorOnShift>().GetAllBy(
                                                s => s.PlantID == plantId &&
                                                     s.LineID == lineId &&
                                                     s.ShiftID == shiftId &&
                                                     s.ProductionDate == productionDate ).FirstOrDefault()
                                );
     
        }
    }
}
