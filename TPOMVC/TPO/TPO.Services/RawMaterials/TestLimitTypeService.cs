using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Data;
using TPO.Services.Core;
using TPO.Common.DTOs;
using AutoMapper;

namespace TPO.Services.RawMaterials
{
    public class TestLimitTypeService : ServiceBase, ITpoService<TestLimitTypeDto>
    {
        public int Add(TestLimitTypeDto dto)
        {
            TestLimitType entity = Save(dto);
            if (entity != null)
                return entity.ID;
            else
            {
                return -1;
            }
        }

        private TestLimitType Save(TestLimitTypeDto dto)
        {
            TestLimitType entity = null;
            try
            {
                if (dto.ID == 0)
                {
                    entity = new TestLimitType();
                    Mapper.Map(dto, entity);
                    _repository.Repository<TestLimitType>().Insert(entity);
                }
                else
                {
                    entity = GetById(dto.ID);
                    Mapper.Map(dto, entity);
                    _repository.Repository<TestLimitType>().Update(entity);
                }
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

            return entity;
        }

        public List<TestLimitTypeDto> GetAll()
        {
            return MapEntityList(_repository.Repository<TestLimitType>().GetAll().ToList());
        }

        private List<TestLimitTypeDto> MapEntityList(List<TestLimitType> entities)
        {
            return Mapper.Map<List<TestLimitType>, List<TestLimitTypeDto>>(entities);
        }

        private TestLimitTypeDto MapEntity(TestLimitType entity)
        {
            return Mapper.Map<TestLimitType, TestLimitTypeDto>(entity);
        }

        public TestLimitTypeDto Get(int id)
        {
            return MapEntity(GetById(id));
        }

        private TestLimitType GetById(int id)
        {
            return _repository.Repository<TestLimitType>().GetById(id);
        }

        public void Delete(int id)
        {
            _repository.Repository<TestLimitType>().Delete(GetById(id));
            CommitUnitOfWork();
        }

        public void Update(TestLimitTypeDto dto)
        {
            Save(dto);
        }

    }
}
