using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;


namespace TPO.Services.Scrap
{
    public class TPOLineScrapTypeService : ServiceBase, ITpoService<TPOLineScrapTypeDto>
    {
        public int Add(TPOLineScrapTypeDto dto)
        {
            TPOLineScrapType entity = Mapper.Map<TPOLineScrapTypeDto, TPOLineScrapType>(dto);
            try
            {
                _repository.Repository<TPOLineScrapType>().Insert(entity);
                _repository.Save();
            }
            catch (DbEntityValidationException valEx)
            {
                HandleValidationException(valEx);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
            return entity.ID;
        }

        public List<TPOLineScrapTypeDto> GetAll()
        {
            var entities = _repository.Repository<TPOLineScrapType>().GetAll().ToList();
            return Mapper.Map<List<TPOLineScrapType>, List<TPOLineScrapTypeDto>>(entities);
        }

        public List<TPOLineScrapTypeDto> GetById(int id)
        {
            Expression<Func<TPOLineScrapType, bool>> filterExpression = p => p.ID == id;
            var entities = _repository.Repository<TPOLineScrapType>().GetAllBy(filterExpression).ToList();
            return Mapper.Map<List<TPOLineScrapType>, List<TPOLineScrapTypeDto>>(entities);
        }

        public TPOLineScrapTypeDto Get(int id)
        {
            var entity = _repository.Repository<TPOLineScrapType>().GetById(id);
            return Mapper.Map<TPOLineScrapType, TPOLineScrapTypeDto>(entity);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<TPOLineScrapType>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(TPOLineScrapTypeDto dto)
        {
            try
            {
                var entity = _repository.Repository<TPOLineScrapType>().GetById(dto.ID);
                Mapper.Map(dto, entity);
                _repository.Repository<TPOLineScrapType>().Update(entity);
                _repository.Save();
            }
            catch (DbEntityValidationException valEx)
            {
                HandleValidationException(valEx);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }
   
    }
}
