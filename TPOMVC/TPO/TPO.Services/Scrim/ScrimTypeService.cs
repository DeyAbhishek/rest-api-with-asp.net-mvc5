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
    public class ScrimTypeService : ServiceBase, ITpoService<ScrimTypeDto>
    {
        public int Add(ScrimTypeDto dto)
        {
            ScrimType entity = Save(dto);
            if (entity != null)
                return entity.ID;
            else
            {
                return -1;
            }
        }

        private ScrimType Save(ScrimTypeDto dto)
        {
            ScrimType entity = null;
            try
            {
                if (dto.ID == 0)
                {
                    entity = new ScrimType();
                    Mapper.Map(dto, entity);
                    _repository.Repository<ScrimType>().Insert(entity);
                }
                else
                {
                    entity = GetById(dto.ID);
                    Mapper.Map(dto, entity);
                    _repository.Repository<ScrimType>().Update(entity);
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

        public List<ScrimTypeDto> GetAll()
        {
            return MapEntityList(_repository.Repository<ScrimType>().GetAll().ToList());
        }

        public List<ScrimTypeDto> GetAllOfType(bool isLiner)
        {
            return MapEntityList(_repository.Repository<ScrimType>().GetAllBy(s => s.IsLiner == isLiner).ToList());
        }

        public List<ScrimTypeDto> GetAllByPlantId(int plantId, bool isLiner)
        {
            return MapEntityList(_repository.Repository<ScrimType>()
                .GetAllBy(s => s.PlantID == plantId && s.IsLiner == isLiner).ToList());
        }

        private List<ScrimTypeDto> MapEntityList(List<ScrimType> entities)
        {
            return Mapper.Map<List<ScrimType>, List<ScrimTypeDto>>(entities);
        }

        private ScrimTypeDto MapEntity(ScrimType entity)
        {
            return Mapper.Map<ScrimType, ScrimTypeDto>(entity);
        }

        public ScrimTypeDto Get(int id)
        {
            return MapEntity(GetById(id));
        }

        private ScrimType GetById(int id)
        {
            return _repository.Repository<ScrimType>().GetById(id);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<ScrimType>().Delete(GetById(id));
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(ScrimTypeDto dto)
        {
            Save(dto);
        }

    }
}
