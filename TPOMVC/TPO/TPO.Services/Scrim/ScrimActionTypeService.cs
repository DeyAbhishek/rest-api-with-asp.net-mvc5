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

namespace TPO.Services.Scrim
{
    public class ScrimActionTypeService : ServiceBase, ITpoService<ScrimActionTypeDto>
    {
        public int Add(ScrimActionTypeDto dto)
        {
            ScrimActionType entity = Save(dto);
            if (entity != null)
                return entity.ID;
            else
            {
                return -1;
            }
        }

        private ScrimActionType Save(ScrimActionTypeDto dto)
        {
            ScrimActionType entity = null;
            try
            {
                if (dto.ID == 0)
                {
                    entity = new ScrimActionType();
                    Mapper.Map(dto, entity);
                    _repository.Repository<ScrimActionType>().Insert(entity);
                }
                else
                {
                    entity = GetById(dto.ID);
                    Mapper.Map(dto, entity);
                    _repository.Repository<ScrimActionType>().Update(entity);
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

        public List<ScrimActionTypeDto> GetAll()
        {
            return MapEntityList(_repository.Repository<ScrimActionType>().GetAll().ToList());
        }

        public List<ScrimActionTypeDto> GetAllByPlantId(int plantId)
        {
            return MapEntityList(_repository.Repository<ScrimActionType>()
                .GetAllBy(s => s.PlantID == plantId).ToList());
        }

        private List<ScrimActionTypeDto> MapEntityList(List<ScrimActionType> entities)
        {
            return Mapper.Map<List<ScrimActionType>, List<ScrimActionTypeDto>>(entities);
        }

        private ScrimActionTypeDto MapEntity(ScrimActionType entity)
        {
            return Mapper.Map<ScrimActionType, ScrimActionTypeDto>(entity);
        }

        public ScrimActionTypeDto Get(int id)
        {
            return MapEntity(GetById(id));
        }

        private ScrimActionType GetById(int id)
        {
            return _repository.Repository<ScrimActionType>().GetById(id);
        }

        public void Delete(int id)
        {
            _repository.Repository<ScrimActionType>().Delete(GetById(id));
            CommitUnitOfWork();
        }

        public void Update(ScrimActionTypeDto dto)
        {
            Save(dto);
        }

    }
}
