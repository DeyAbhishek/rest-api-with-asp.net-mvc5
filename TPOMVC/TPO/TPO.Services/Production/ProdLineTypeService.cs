using AutoMapper;
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
    public class ProdLineTypeService : ServiceBase, ITpoService<ProdLineTypeDto>
    {
        public int Add(ProdLineTypeDto dto)
        {
            throw new NotImplementedException();
        }

        public List<ProdLineTypeDto> GetAll()
        {
            var entities = _repository.Repository<ProdLineType>().GetAll().ToList();
            return Mapper.Map<List<ProdLineType>, List<ProdLineTypeDto>>(entities);
        }

        public ProdLineTypeDto Get(int id)
        {
            var entity = _repository.Repository<ProdLineType>().GetById(id);
            return Mapper.Map<ProdLineType, ProdLineTypeDto>(entity);
        }

        public ProdLineTypeDto GetByLineID(int lineID) 
        {
            var entity = _repository.Repository<ProdLine>().GetById(lineID);
            return Mapper.Map<ProdLineType, ProdLineTypeDto>(entity.ProdLineType);
        }

        public ProdLineTypeDto GetByLineCode(string code)
        {
            var entity = _repository.Repository<ProdLineType>().GetAll().Where(q=>q.ProdLineTypeCode == code).FirstOrDefault();
            return Mapper.Map<ProdLineType, ProdLineTypeDto>(entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ProdLineTypeDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
