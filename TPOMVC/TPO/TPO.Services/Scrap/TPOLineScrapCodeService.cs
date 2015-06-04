using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;

namespace TPO.Services.Scrap
{
    public class TPOLineScrapCodeService : ServiceBase, ITpoService<TPOLineScrapCodeDto>
    {
        public int Add(TPOLineScrapCodeDto dto)
        {
            TPOLineScrapCode entity = Mapper.Map<TPOLineScrapCodeDto, TPOLineScrapCode>(dto);
            try
            {
                _repository.Repository<TPOLineScrapCode>().Insert(entity);
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

        public List<TPOLineScrapCodeDto> GetAll()
        {
            var entities = _repository.Repository<TPOLineScrapCode>().GetAll().ToList();
            return Mapper.Map<List<TPOLineScrapCode>, List<TPOLineScrapCodeDto>>(entities);
        }

        public List<TPOLineScrapCodeDto> GetByCodeGroup(int id)
        {
            Expression<Func<TPOLineScrapCode, bool>> filterExpression = p => p.GroupID == id;
            var entities = _repository.Repository<TPOLineScrapCode>().GetAllBy(filterExpression).ToList();
            return Mapper.Map<List<TPOLineScrapCode>, List<TPOLineScrapCodeDto>>(entities);
        } 

        public TPOLineScrapCodeDto Get(int id)
        {
            var entity = _repository.Repository<TPOLineScrapCode>().GetById(id);
            return Mapper.Map<TPOLineScrapCode, TPOLineScrapCodeDto>(entity);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<TPOLineScrapCode>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(TPOLineScrapCodeDto dto)
        {
            try
            {
                var entity = _repository.Repository<TPOLineScrapCode>().GetById(dto.ID);
                Mapper.Map(dto, entity);
                _repository.Repository<TPOLineScrapCode>().Update(entity);
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
