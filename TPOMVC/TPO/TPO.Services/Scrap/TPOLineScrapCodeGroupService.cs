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
    public class TPOLineScrapCodeGroupService : ServiceBase, ITpoService<TPOLineScrapCodeGroupDto>
    {
        public int Add(TPOLineScrapCodeGroupDto dto)
        {
            TPOLineScrapCodeGroup entity = Mapper.Map<TPOLineScrapCodeGroupDto, TPOLineScrapCodeGroup>(dto);

            try
            {
                _repository.Repository<TPOLineScrapCodeGroup>().Insert(entity);
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

        public List<TPOLineScrapCodeGroupDto> GetAll()
        {
            var entities = _repository.Repository<TPOLineScrapCodeGroup>().GetAll().ToList();
            return Mapper.Map<List<TPOLineScrapCodeGroup>, List<TPOLineScrapCodeGroupDto>>(entities);
        }

        public List<TPOLineScrapCodeGroupDto> GetByPlant(int id)
        {
            Expression<Func<TPOLineScrapCodeGroup, bool>> filterExpression = p => p.PlantID == id;
            var entities = _repository.Repository<TPOLineScrapCodeGroup>().GetAllBy(filterExpression).ToList();
            return Mapper.Map<List<TPOLineScrapCodeGroup>, List<TPOLineScrapCodeGroupDto>>(entities);
        } 

        public TPOLineScrapCodeGroupDto Get(int id)
        {
            var entity = _repository.Repository<TPOLineScrapCodeGroup>().GetById(id);
            return Mapper.Map<TPOLineScrapCodeGroup, TPOLineScrapCodeGroupDto>(entity);
        }

        public void Delete(int id)
        {
            try
            {
                var items =  _repository.Repository<TPOLineScrapCode>().GetAllBy(c => c.GroupID == id);
                foreach (TPOLineScrapCode item in items)
                { 
                    _repository.Repository<TPOLineScrapCode>().Delete(item.ID);
                }
                _repository.Repository<TPOLineScrapCodeGroup>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(TPOLineScrapCodeGroupDto dto)
        {
            try
            {
                var entity = _repository.Repository<TPOLineScrapCodeGroup>().GetById(dto.ID);
                Mapper.Map(dto, entity);
                _repository.Repository<TPOLineScrapCodeGroup>().Update(entity);
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
