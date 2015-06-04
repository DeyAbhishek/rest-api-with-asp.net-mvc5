using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Data;
using TPO.Services.Core;
using TPO.Common.DTOs;
using AutoMapper;

namespace TPO.Services.Scrim
{
    public class ProdLineRollConfigService : ServiceBase, ITpoService<ProdLineRollConfigDto>
    {
        public int Add(ProdLineRollConfigDto dto)
        {
            ProdLineRollConfig entity = Save(dto);
            CommitUnitOfWork();
            if (entity != null)
                return entity.ID;
            else
            {
                return -1;
            }
        }

        private ProdLineRollConfig Save(ProdLineRollConfigDto dto)
        {
            ProdLineRollConfig entity = null;
            try
            {
                if (dto.ID == 0)
                {
                    entity = new ProdLineRollConfig();
                    Mapper.Map(dto, entity);
                    _repository.Repository<ProdLineRollConfig>().Insert(entity);
                }
                else
                {
                    entity = GetById(dto.ID);
                    Mapper.Map(dto, entity);
                    _repository.Repository<ProdLineRollConfig>().Update(entity);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
            return entity;
        }

        public List<ProdLineRollConfigDto> GetAll()
        {
            return MapEntityList(_repository.Repository<ProdLineRollConfig>().GetAll().ToList());
        }

        public List<ProdLineRollConfigDto> GetAllByTypeId(int typeId)
        {
            return MapEntityList(_repository.Repository<ProdLineRollConfig>().GetAllBy(p => p.TypeID == typeId).ToList());
        }

        private List<ProdLineRollConfigDto> MapEntityList(List<ProdLineRollConfig> entities)
        {
            return Mapper.Map<List<ProdLineRollConfig>, List<ProdLineRollConfigDto>>(entities);
        }

        private ProdLineRollConfigDto MapEntity(ProdLineRollConfig entity)
        {
            return Mapper.Map<ProdLineRollConfig, ProdLineRollConfigDto>(entity);
        }

        public ProdLineRollConfigDto Get(int id)
        {
            return MapEntity(GetById(id));
        }

        private ProdLineRollConfig GetById(int id)
        {
            return _repository.Repository<ProdLineRollConfig>().GetById(id);
        }

        public void Delete(int id)
        {
            _repository.Repository<ProdLineRollConfig>().Delete(GetById(id));
            CommitUnitOfWork();
        }

        public void Update(ProdLineRollConfigDto dto)
        {
            Save(dto);
            CommitUnitOfWork();
        }

    }
}
