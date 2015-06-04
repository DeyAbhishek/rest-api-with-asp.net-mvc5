using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;

namespace TPO.Services.RawMaterials
{
     public class RawMaterialVendorService : ServiceBase, ITpoService<RawMaterialVendorDto>
     {
         public int Add(RawMaterialVendorDto dto)
         {
            var entity = new RawMaterialVendor();
             try
             {
                 Mapper.Map(dto, entity);

                 _repository.Repository<RawMaterialVendor>().Insert(entity);
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

         public List<RawMaterialVendorDto> GetAll()
         {
             var entities = _repository.Repository<RawMaterialVendor>().GetAll().ToList();
             return Mapper.Map<List<RawMaterialVendor>, List<RawMaterialVendorDto>>(entities);
         }

         public RawMaterialVendorDto Get(int id)
         {
             var entity = _repository.Repository<RawMaterialVendor>().GetById(id);
             return Mapper.Map<RawMaterialVendor, RawMaterialVendorDto>(entity);
         }

         public List<RawMaterialVendorDto> GetByPlantId(int plantId)
         {
            
             var entities = _repository.Repository<RawMaterialVendor>().GetAllBy(v => v.PlantID == plantId).ToList();
             return Mapper.Map<List<RawMaterialVendor>, List<RawMaterialVendorDto>>(entities);

         }

         public void Delete(int id)
         {
             try
             {
                 var rm = _repository.Repository<RawMaterial>().GetAllBy(r => r.RawMaterialVendorID == id).ToList();
                 if (rm.Count != 0)
                     throw new InvalidOperationException("Unable to delete Vendor because the Vendor has provided Raw Materials.");

                 _repository.Repository<RawMaterialVendor>().Delete(id);
                 _repository.Save();
             }
             catch (Exception ex)
             {
                 LogException(ex);
                 throw;
             }
         }

         public void Update(RawMaterialVendorDto dto)
         {
            var entity = _repository.Repository<RawMaterialVendor>().GetById(dto.Id);
            Mapper.Map(dto, entity);

             try
             {
                 _repository.Repository<RawMaterialVendor>().Update(entity);
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
