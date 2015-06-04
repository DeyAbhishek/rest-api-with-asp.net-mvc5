using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TPO.DL.Models;
using TPO.Domain.DTO;

namespace TPO.BL.Repositories.RawMaterial.CurrentRawMaterial
{
    public class CurrentRawMaterialRepository : ICurrentRawMaterialRepository
    {
        
        public CurrentRawMaterialRepository()
        {
        }

        public List<CurrentRawMaterialDTO> GetAll()
        {
            using (var context = new TPOMVCApplicationEntities())
            {
                var allItems = (from a in context.TPOCurrentRawMaterials
                    select a);

                return CreateList(allItems);
            }
        }

        

        public List<CurrentRawMaterialDTO> GetAll(int plantId)
        {
            using (var context = new TPOMVCApplicationEntities())
            {
                var allItems = (from a in context.TPOCurrentRawMaterials
                    where a.PlantID == plantId
                    select a);

                return CreateList(allItems);
            }
        }

        public List<CurrentRawMaterialDTO> GetAll(int plantId, string lineId)
        {
            using (var context = new TPOMVCApplicationEntities())
            {
                var allItems = (from a in context.TPOCurrentRawMaterials
                    where a.PlantID == plantId
                          && a.LineID == lineId
                    select a);

                return CreateList(allItems);
            }
        }

        public CurrentRawMaterialDTO GetById(int id)
        {
            using (var context = new TPOMVCApplicationEntities())
            {
                var thisItem = (from a in context.TPOCurrentRawMaterials
                    where a.ID == id
                    select a);
                return thisItem.Any() ? MapToDTO(thisItem.FirstOrDefault()) : null;
            }
        }

        public void Add(CurrentRawMaterialDTO dto)
        {
            try
            {
                using (var context = new TPOMVCApplicationEntities())
                {
                    var newEntity = new TPOCurrentRawMaterial();
                    newEntity.RawMaterialReceivedID = dto.RawMaterialReceivedId;
                    newEntity.LineID = dto.LineId;
                    newEntity.PlantID = dto.PlantId;
                    newEntity.DateEntered = dto.DateEntered;
                    newEntity.EnteredBy = dto.EnteredBy;
                    newEntity.ModifiedBy = dto.ModifiedBy;
                    newEntity.LastModified = dto.LastModified;
                    newEntity.Plant = context.Plants.Where(p => p.ID == dto.PlantId).FirstOrDefault();
                    newEntity.RawMaterialReceived =
                        context.RawMaterialReceiveds.Where(m => m.ID == dto.RawMaterialReceivedId).FirstOrDefault();
                    context.TPOCurrentRawMaterials.Add(newEntity);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        

        public void Update(CurrentRawMaterialDTO dto)
        {
            try
            {
                using (var context = new TPOMVCApplicationEntities())
                {
                    var toUpdate = context.TPOCurrentRawMaterials.Find(dto.Id);
                    
                    toUpdate.LineID = dto.LineId;
                    toUpdate.PlantID = dto.PlantId;
                    // do not allow edit to initial data
                    //toUpdate.DateEntered = dto.DateEntered;
                    //if (!string.IsNullOrWhiteSpace(dto.EnteredBy))
                    //{
                    //    toUpdate.EnteredBy = dto.EnteredBy;
                    //}
                    if (!string.IsNullOrWhiteSpace(dto.ModifiedBy))
                    {
                        toUpdate.ModifiedBy = dto.ModifiedBy;
                    }
                    toUpdate.LastModified = DateTime.Now;

                    toUpdate.Plant = context.Plants.Where(p => p.ID == dto.PlantId).FirstOrDefault();

                    toUpdate.RawMaterialReceivedID = dto.RawMaterialReceivedId;
                    toUpdate.RawMaterialReceived =
                        context.RawMaterialReceiveds.Where(m => m.ID == dto.RawMaterialReceivedId).FirstOrDefault();

                    context.Entry(toUpdate).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (var context = new TPOMVCApplicationEntities())
                {
                    var toDelete = context.TPOCurrentRawMaterials.Find(id);
                    context.TPOCurrentRawMaterials.Remove(toDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        #region Mapping
        //TODO: Move to common
        private static List<CurrentRawMaterialDTO> CreateList(IEnumerable<TPOCurrentRawMaterial> items)
        {
            return items.Select(MapToDTO).ToList();
        }

        private static CurrentRawMaterialDTO MapToDTO(TPOCurrentRawMaterial dbo)
        {
            //TODO: Add mapper
            var dto = new CurrentRawMaterialDTO
            {
                Id = dbo.ID,
                PlantId = dbo.PlantID,
                RawMaterialReceivedId = dbo.RawMaterialReceivedID,
                RawMaterialReceivedCode = dbo.RawMaterialReceived.RawMaterial.Code,
                QuantityReceived = dbo.RawMaterialReceived.QuantityReceived,
                QuantityShipped = dbo.RawMaterialReceived.QuantityShipped,
                QuantityUsed = dbo.RawMaterialReceived.QuantityUsedThisLot,
                LotId = dbo.RawMaterialReceived.LotNumber,
                LineId = dbo.LineID,
                DateEntered = dbo.DateEntered,
                EnteredBy = dbo.EnteredBy,
                LastModified = dbo.LastModified,
                ModifiedBy = dbo.ModifiedBy
            };
            return dto;
        }

        //private static TPOCurrentRawMaterial MapFromDto(CurrentRawMaterialDTO dto)
        //{
        //    var entity = new TPOCurrentRawMaterial
        //    {
        //        ID = dto.Id,
        //        LineID = dto.LineId,
        //        PlantID = dto.PlantId,
        //        RawMaterialReceivedID = dto.RawMaterialReceivedId,
                
        //        DateEntered = dto.DateEntered,
        //        EnteredBy = dto.EnteredBy,
        //        LastModified = dto.LastModified,
        //        ModifiedBy = dto.ModifiedBy
             
        //    };
        //    return entity;
        //}

        #endregion

    }
}
