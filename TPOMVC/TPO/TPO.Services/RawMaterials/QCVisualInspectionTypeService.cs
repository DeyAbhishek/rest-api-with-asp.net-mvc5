using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;

namespace TPO.Services.RawMaterials
{
    public class QCVisualInspectionTypeService : ServiceBase, ITpoService<QCVisualInspectionTypeDto>
    {
        public int Add(QCVisualInspectionTypeDto dto)
        {
            throw new NotImplementedException();
        }

        public List<QCVisualInspectionTypeDto> GetAll()
        {
            var entities = _repository.Repository<QCVisualInspectionType>().GetAll().ToList();
            return Mapper.Map<List<QCVisualInspectionType>, List<QCVisualInspectionTypeDto>>(entities);
        }

        public QCVisualInspectionTypeDto Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(QCVisualInspectionTypeDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
