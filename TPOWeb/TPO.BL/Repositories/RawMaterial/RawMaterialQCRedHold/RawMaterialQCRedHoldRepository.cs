using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using TPO.DL.Models;
using TPO.Domain.DTO;

namespace TPO.BL.Repositories.RawMaterial.RawMaterialQCRedHold
{
    public class RawMaterialQCRedHoldRepository : IRawMaterialQCRedHoldRepository
    {
        private TPO.DL.Models.TPOMVCApplicationEntities dbContext;
        public RawMaterialQCRedHoldRepository()
        { 
            dbContext = new DL.Models.TPOMVCApplicationEntities();
        }

        public RawMaterialQCRedHoldDTO GetByID(int id)
        {
            using (var context = new TPOMVCApplicationEntities())
            { 
                var thisItem = (from a in context.RawMaterialQCRedHolds
                                    where a.ID == id
                                    select a);
                return thisItem.Any() ? MapToDTO(thisItem.FirstOrDefault()) : null;
            }
        }
        public RawMaterialQCRedHoldDTO GetByQCID(int id)
        {
            using (var context = new TPOMVCApplicationEntities()) 
            {
                var thisItem = (from a in context.RawMaterialQCRedHolds
                                where a.RawMaterialQCID == id
                                select a);
                return thisItem.Any() ? MapToDTO(thisItem.FirstOrDefault()) : null;
            }
        }
        public int Add(RawMaterialQCRedHoldDTO dto)
        {
            int returnID = -1;
            try 
            {
                using (var context = new TPOMVCApplicationEntities())
                {
                    var newEntity = new TPO.DL.Models.RawMaterialQCRedHold();
                    newEntity.RawMaterialQCID = dto.RawMaterialQCID;
                    newEntity.PlantID = dto.PlantID;
                    newEntity.RawMaterialReceivedID = dto.RawMaterialReceivedID;
                    newEntity.QCTechID = dto.QCTechID;
                    newEntity.SupervisorID = dto.SupervisorID;
                    newEntity.LeadOperatorID = dto.LeadOperatorID;
                    newEntity.RedDate = dto.RedDate;
                    newEntity.FailPropertyID = dto.FailPropertyID;
                    newEntity.Zone = dto.Zone;
                    newEntity.RedComments = dto.RedComments;
                    newEntity.RedCorrectionAction = dto.RedCorrectionAction;
                    newEntity.HoldDate = dto.HoldDate;
                    newEntity.HoldLotNumber = dto.HoldLotNumber;
                    newEntity.HoldComments = dto.HoldComments;
                    newEntity.ManagerID = dto.ManagerID;
                    newEntity.ManagerDate = dto.ManagerDate;
                    newEntity.ManagerComments = dto.ManagerComments;
                    newEntity.PrimeBoxCar = dto.PrimeBoxCar;
                    newEntity.PrimeUOM = dto.PrimeUOM;
                    newEntity.ReworkBoxCar = dto.ReworkBoxCar;
                    newEntity.ReworkUOM = dto.ReworkUOM;
                    newEntity.ScrapBoxCar = dto.ScrapBoxCar;
                    newEntity.ScrapUOM = dto.ScrapUOM;
                    newEntity.DateEntered = dto.DateEntered ?? DateTime.Now;
                    newEntity.EnteredBy = dto.EnteredBy;
                    newEntity.LastModified = dto.LastModified ?? DateTime.Now;
                    newEntity.ModifiedBy = dto.ModifiedBy;
                    context.RawMaterialQCRedHolds.Add(newEntity);
                    context.SaveChanges();
                    //need to return the new record id
                    //easy to do once we update to stored procs
                    returnID = newEntity.ID;
                }
            }
            catch (DbEntityValidationException ex) 
            {
                throw;
            }
            return returnID;
        }
        public void Update(RawMaterialQCRedHoldDTO dto) 
        {
            try
            {
                using (var context = new TPOMVCApplicationEntities())
                {
                    var toUpdate = context.RawMaterialQCRedHolds.Find(dto.ID);
                    toUpdate.RawMaterialQCID = dto.RawMaterialQCID;
                    toUpdate.PlantID = dto.PlantID;
                    toUpdate.RawMaterialReceivedID = dto.RawMaterialReceivedID;
                    toUpdate.QCTechID = dto.QCTechID;
                    toUpdate.SupervisorID = dto.SupervisorID;
                    toUpdate.LeadOperatorID = dto.LeadOperatorID;
                    toUpdate.RedDate = dto.RedDate;
                    toUpdate.FailPropertyID = dto.FailPropertyID;
                    toUpdate.Zone = dto.Zone;
                    toUpdate.RedComments = dto.RedComments;
                    toUpdate.RedCorrectionAction = dto.RedCorrectionAction;
                    toUpdate.HoldDate = dto.HoldDate;
                    toUpdate.HoldLotNumber = dto.HoldLotNumber;
                    toUpdate.HoldComments = dto.HoldComments;
                    toUpdate.ManagerID = dto.ManagerID;
                    toUpdate.ManagerDate = dto.ManagerDate;
                    toUpdate.ManagerComments = dto.ManagerComments;
                    toUpdate.PrimeBoxCar = dto.PrimeBoxCar;
                    toUpdate.PrimeUOM = dto.PrimeUOM;
                    toUpdate.ReworkBoxCar = dto.ReworkBoxCar;
                    toUpdate.ReworkUOM = dto.ReworkUOM;
                    toUpdate.ScrapBoxCar = dto.ScrapBoxCar;
                    toUpdate.ScrapUOM = dto.ScrapUOM;
                    toUpdate.DateEntered = dto.DateEntered ?? DateTime.Now;
                    toUpdate.EnteredBy = dto.EnteredBy;
                    toUpdate.LastModified = dto.LastModified ?? DateTime.Now;
                    toUpdate.ModifiedBy = dto.ModifiedBy;
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
            try {
                using (var context = new TPOMVCApplicationEntities())
                {
                    var toDelete = context.RawMaterialQCRedHolds.Find(id);
                    context.RawMaterialQCRedHolds.Remove(toDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static RawMaterialQCRedHoldDTO MapToDTO(TPO.DL.Models.RawMaterialQCRedHold dbo)
        {
            RawMaterialQCRedHoldDTO dto = new RawMaterialQCRedHoldDTO 
            { 
                ID = dbo.ID,
                RawMaterialQCID = dbo.RawMaterialQCID ?? -1,
                PlantID = dbo.PlantID,
                RawMaterialReceivedID = dbo.RawMaterialReceivedID,
                QCTechID = dbo.QCTechID,
                SupervisorID = dbo.SupervisorID,
                LeadOperatorID = dbo.LeadOperatorID,
                RedDate = dbo.RedDate,
                FailPropertyID = dbo.FailPropertyID,
                Zone = dbo.Zone,
                RedComments = dbo.RedComments,
                RedCorrectionAction = dbo.RedCorrectionAction,
                HoldDate = dbo.HoldDate,
                HoldLotNumber = dbo.HoldLotNumber,
                HoldComments = dbo.HoldComments,
                ManagerID = dbo.ManagerID,
                ManagerDate = dbo.ManagerDate,
                ManagerComments = dbo.ManagerComments,
                PrimeBoxCar = (float)dbo.PrimeBoxCar,
                PrimeUOM = (float)dbo.PrimeUOM,
                ReworkBoxCar = (float)dbo.ReworkBoxCar,
                ReworkUOM = (float)dbo.ReworkUOM,
                ScrapBoxCar = (float)dbo.ScrapBoxCar,
                ScrapUOM = (float)dbo.ScrapUOM,
                DateEntered = dbo.DateEntered,
                EnteredBy = dbo.EnteredBy,
                LastModified = dbo.LastModified,
                ModifiedBy = dbo.ModifiedBy                
            };
            return dto;
        }
    }
}
