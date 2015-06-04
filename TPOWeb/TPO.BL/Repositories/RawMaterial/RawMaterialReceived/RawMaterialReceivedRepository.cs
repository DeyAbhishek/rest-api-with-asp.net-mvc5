using System;
using System.Collections.Generic;
using System.Linq;
using TPO.BL.Repositories.RawMaterial.RawMaterialReceivedRepository;

namespace TPO.BL.Repositories.RawMaterial.RawMaterialReceived
{
    public class RawMaterialReceivedRepository : IRawMaterialReceivedRepository
    {
        public RawMaterialReceivedRepository()
        {

        }

        #region Gst Possible Raw Material Codes

        public List<Int32> GetAvailableRawMaterialIds()
        {
            using (var context = new DL.Models.TPOMVCApplicationEntities())
            {
                var allItems = (from a in context.RawMaterialReceiveds
                                select a.RawMaterialID)
                    .Distinct();
                return allItems.ToList();
            }
        }

        public List<Int32> GetAvailableRawMaterialIds(int plantId)
        {
            using (var context = new DL.Models.TPOMVCApplicationEntities())
            {
                var allItems = (from a in context.RawMaterialReceiveds
                                where a.PlantID == plantId
                                select a.RawMaterialID)
                    .Distinct();
                return allItems.ToList();
            }
        }
        #endregion


        public List<Domain.DTO.RawMaterialReceivedDTO> GetAll()
        {
            using (var context = new DL.Models.TPOMVCApplicationEntities())
            {
                var allItems = (from a in context.RawMaterialReceiveds
                                select a);

                return CreateList(allItems);
            }
        }

        public void Add(Domain.DTO.RawMaterialReceivedDTO rawMaterialReceived)
        {
            using (var context = new DL.Models.TPOMVCApplicationEntities())
            {
                var newRawMaterialRecieved = MapToDBO(new DL.Models.RawMaterialReceived(), rawMaterialReceived);
                context.RawMaterialReceiveds.Add(newRawMaterialRecieved);
                context.SaveChanges();
            }
        }

        public void Update(Domain.DTO.RawMaterialReceivedDTO rawMaterialReceived)
        {
            using (var context = new DL.Models.TPOMVCApplicationEntities())
            {
                var entity = (from d in context.RawMaterialReceiveds
                              where d.ID == rawMaterialReceived.ID
                              select d).SingleOrDefault();
                //TODO: Add mapper
                entity.DateEntered = rawMaterialReceived.DateEntered;
                entity.EnteredBy = rawMaterialReceived.EnteredBy;
                entity.LastModified = rawMaterialReceived.LastModified;
                entity.LotNumber = rawMaterialReceived.LotNumber;
                entity.ModifiedBy = rawMaterialReceived.ModifiedBy;
                entity.PlantID = rawMaterialReceived.PlantID;
                entity.RawMaterialID = rawMaterialReceived.RawMaterialID;
                entity.QuantityNotReceived = rawMaterialReceived.QuantityNotReceived;
                entity.QuantityReceived = rawMaterialReceived.QuantityReceived;
                entity.QuantityShipped = rawMaterialReceived.QuantityShipped;
                entity.QuantityUsedThisLot = rawMaterialReceived.QuantityUsedThisLot;
                entity.CoA = rawMaterialReceived.CoA;
                entity.ReceivedSizeLimitID = rawMaterialReceived.ReceivedSizeLimitID;
                entity.UOMID = rawMaterialReceived.UoMID;
                
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new DL.Models.TPOMVCApplicationEntities())
            {
                var del = (from d in context.RawMaterialReceiveds
                           where d.ID == id
                           select d).SingleOrDefault();
                context.RawMaterialReceiveds.Remove(del);
                context.SaveChanges();
            }
        }

        public List<Domain.DTO.RawMaterialReceivedDTO> GetAll(int plantId)
        {
            using (var context = new DL.Models.TPOMVCApplicationEntities())
            {
                var allItems = (from a in context.RawMaterialReceiveds
                                where a.PlantID == plantId
                                select a);

                return CreateList(allItems);
            }
        }

        public List<Domain.DTO.RawMaterialReceivedDTO> GetAll(int plantId, int materialId)
        {
            using (var context = new DL.Models.TPOMVCApplicationEntities())
            {
                var allItems = (from a in context.RawMaterialReceiveds
                                where a.PlantID == plantId && a.RawMaterialID == materialId
                                select a);

                return CreateList(allItems);
            }
        }

        //TODO: Move to common
        private Domain.DTO.RawMaterialReceivedDTO MapToDTO(DL.Models.RawMaterialReceived dbo)
        {
            //TODO: Add mapper
            var dto = new Domain.DTO.RawMaterialReceivedDTO();
            dto.DateEntered = dbo.DateEntered;
            dto.EnteredBy = dbo.EnteredBy;
            dto.ID = dbo.ID;
            dto.LastModified = dbo.LastModified;
            dto.ModifiedBy = dbo.ModifiedBy;
            dto.PlantID = dbo.PlantID;
            dto.PlantCode = dbo.Plant.Code;
            dto.RawMaterialID = dbo.RawMaterialID;
            dto.LotNumber = dbo.LotNumber;
            dto.RawMaterialCode = dbo.RawMaterial.Code;
            dto.QuantityNotReceived = dbo.QuantityNotReceived;
            dto.QuantityReceived = dbo.QuantityReceived;
            dto.QuantityShipped = dbo.QuantityShipped;
            dto.QuantityUsedThisLot = dbo.QuantityUsedThisLot;
            dto.CoA = dbo.CoA;
            dto.ReceivedSizeLimitID = dbo.ReceivedSizeLimitID;
            dto.UoMID = dbo.UOMID;
            dto.UoMCode = dbo.UnitOfMeasure.Code;
            return dto;
        }

        //TODO: Move to common
        private DL.Models.RawMaterialReceived MapToDBO(DL.Models.RawMaterialReceived entity, Domain.DTO.RawMaterialReceivedDTO dto)
        {
            //TODO: Add mapper
            entity.DateEntered = dto.DateEntered;
            entity.EnteredBy = dto.EnteredBy;
            entity.LastModified = dto.LastModified;
            entity.LotNumber = dto.LotNumber;
            entity.ModifiedBy = dto.ModifiedBy;
            entity.PlantID = dto.PlantID;
            entity.RawMaterialID = dto.RawMaterialID;
            entity.ID = dto.ID;
            entity.QuantityNotReceived = dto.QuantityNotReceived;
            entity.QuantityReceived = dto.QuantityReceived;
            entity.QuantityShipped = dto.QuantityShipped;
            entity.QuantityUsedThisLot = dto.QuantityUsedThisLot;
            entity.CoA = dto.CoA;
            entity.ReceivedSizeLimitID = dto.ReceivedSizeLimitID;
            entity.UOMID = dto.UoMID;
            return entity;
        }

        //TODO: Move to common
        private List<Domain.DTO.RawMaterialReceivedDTO> CreateList(IQueryable allItems)
        {
            List<Domain.DTO.RawMaterialReceivedDTO> returnList = new List<Domain.DTO.RawMaterialReceivedDTO>();

            foreach (DL.Models.RawMaterialReceived dbo in allItems)
            {
                returnList.Add(MapToDTO(dbo));
            }

            return returnList;
        }

        public Domain.DTO.RawMaterialReceivedDTO GetById(int id)
        {
            using (var context = new DL.Models.TPOMVCApplicationEntities())
            {
                var item = (from a in context.RawMaterialReceiveds
                            where a.ID == id
                            select a).SingleOrDefault();

                return MapToDTO(item);
            }
        }
    }
}
