using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;
using TPO.Services.RawMaterials;

namespace TPO.Services.Scrim
{
    public class TPOCurrentScrimService : ServiceBase, ITpoService<TPOCurrentScrimDto>
    {
        public int Add(TPOCurrentScrimDto dto)
        {
            int result = 0;

            var entity = new TPOCurrentScrim();
            
            try
            {
                Mapper.Map(dto, entity);
                entity.ScrimPos = "NA";
                _repository.Repository<TPOCurrentScrim>().Insert(entity);
                _repository.Save();
                result = entity.ID;
            }
            catch (DbEntityValidationException valEx)
            {
                var sb = new StringBuilder();

                foreach (var failure in valEx.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), valEx
                    ); // Add the original exception as the innerException
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }

            return result;
        }

        public List<TPOCurrentScrimDto> GetAll()
        {
            var entities = _repository.Repository<TPOCurrentScrim>().GetAll().ToList();
            return Mapper.Map<List<TPOCurrentScrim>, List<TPOCurrentScrimDto>>(entities);
        }

        public TPOCurrentScrimDto Get(int id)
        {
            var entity = _repository.Repository<TPOCurrentScrim>().GetById(id);
            return Mapper.Map<TPOCurrentScrim, TPOCurrentScrimDto>(entity);
        }

        public List<TPOCurrentScrimDto> GetByLineID(int lineID) 
        {
            var entities = _repository.Repository<TPOCurrentScrim>().GetAllBy(s => s.ProdLineID == lineID).ToList();
            return Mapper.Map<List<TPOCurrentScrim>, List<TPOCurrentScrimDto>>(entities);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<TPOCurrentScrimDto>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(TPOCurrentScrimDto dto)
        {

            var entity = _repository.Repository<TPOCurrentScrim>().GetById(dto.ID);

            entity.ScrimPos = string.IsNullOrWhiteSpace(entity.ScrimPos) ? "NA" : entity.ScrimPos;
            entity.ProdLineID = dto.ProdLineID;
            entity.Scrim1TypeID = dto.Scrim1TypeID;
            entity.Scrim2TypeID = dto.Scrim2TypeID;
            entity.FleeceTypeID = dto.FleeceTypeID;
            entity.Scrim1RollID = dto.Scrim1RollID;
            entity.Scrim2RollID = dto.Scrim2RollID;
            entity.FleeceRollID = dto.FleeceRollID;
            entity.LastModified = dto.LastModified;
            entity.ModifiedBy = dto.ModifiedBy;
            
            try
            { 

                _repository.Repository<TPOCurrentScrim>().Update(entity);
                _repository.Save();
            }
            catch (DbEntityValidationException valEx)
            {
                var sb = new StringBuilder();

                foreach (var failure in valEx.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), valEx
                    ); // Add the original exception as the innerException
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public int UpdateTPOCurrentRawMaterial(TPOCurrentRawMaterialDto tpoCurrentRawMaterialDto)
        {
            var tpoCurrentRawMaterialEntity = _repository.Repository<TPOCurrentRawMaterial>().GetById(tpoCurrentRawMaterialDto.ID);
            try
            {
                tpoCurrentRawMaterialDto.ModifiedBy = CurrentUserName;
                tpoCurrentRawMaterialDto.LastModified = DateTime.Now;

                Mapper.Map(tpoCurrentRawMaterialDto, tpoCurrentRawMaterialEntity);

                _repository.Repository<TPOCurrentRawMaterial>().Update(tpoCurrentRawMaterialEntity);

                _repository.Save();
            }
            catch (DbEntityValidationException valEx)
            {
                var sb = new StringBuilder();

                foreach (var failure in valEx.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), valEx
                    ); // Add the original exception as the innerException
            }
            catch (Exception ex)
            {

            }
            return tpoCurrentRawMaterialEntity.ID;
        }


        public int AddTPOCurrentRawMaterial(TPOCurrentRawMaterialDto tpoCurrentRawMaterialDto)
        {
            var tpoCurrentRawMaterialEntity = new TPOCurrentRawMaterial();
            try
            {
                tpoCurrentRawMaterialDto.ModifiedBy = CurrentUserName;
                tpoCurrentRawMaterialDto.EnteredBy = CurrentUserName;
                tpoCurrentRawMaterialDto.LastModified = DateTime.Now;
                tpoCurrentRawMaterialDto.DateEntered = DateTime.Now;

                Mapper.Map(tpoCurrentRawMaterialDto, tpoCurrentRawMaterialEntity);

                _repository.Repository<TPOCurrentRawMaterial>().Insert(tpoCurrentRawMaterialEntity);

                _repository.Save();
            }
            catch (DbEntityValidationException valEx)
            {
                var sb = new StringBuilder();

                foreach (var failure in valEx.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), valEx
                    ); // Add the original exception as the innerException
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
            return tpoCurrentRawMaterialEntity.ID;
        } 
    }
}
