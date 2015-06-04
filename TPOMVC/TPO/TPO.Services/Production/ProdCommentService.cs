using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;


namespace TPO.Services.Production
{
    public class ProdCommentService : ServiceBaseGeneric<ProdCommentDto, ProdComment>, ITpoService<ProdCommentDto>
    {
        public ProdCommentDto GetByLineShiftWorkOrderDate(int productionLineId, int shiftId, int workOrderId, DateTime productionDate)
        {
            ProdCommentDto dto = new ProdCommentDto();
            try
            {
                var comment = _repository.Repository<ProdComment>().GetAll().Where(
                            c => c.LineID == productionLineId &&
                                 c.ShiftID == shiftId &&
                                 c.WorkOrderID == workOrderId &&
                                 c.ProductionDate == productionDate).FirstOrDefault();
                Mapper.Map(comment, dto);
            }
            catch(Exception exc)
            {
                throw exc;
            }

            return dto;
        }
    }
}
