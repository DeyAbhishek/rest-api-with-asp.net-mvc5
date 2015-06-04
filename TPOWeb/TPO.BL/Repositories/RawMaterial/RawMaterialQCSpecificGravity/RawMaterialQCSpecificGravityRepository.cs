using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using TPO.DL.Models;
using TPO.Domain.DTO;
using TPO.Model.RawMaterials;

namespace TPO.BL.Repositories.RawMaterial.RawMaterialQCSpecificGravity
{
    public class RawMaterialQCSpecificGravityRepository: IRawMaterialQCSpecificGravityRepository
    {

        public RawMaterialQCSpecificGravityDTO GetByID(int id)
        {
            using (var context = new TPOMVCApplicationEntities())
            { 
                var thisItem = (from a in context.RawMaterialSpecificGravities
                                    where a.ID == id
                                    select a);
                return thisItem.Any() ? MapToDTO(thisItem.FirstOrDefault()) : null;
            }
        }
        public RawMaterialQCSpecificGravityDTO GetByQCID(int id)
        {
            using (var context = new TPOMVCApplicationEntities()) 
            {
                var thisItem = (from a in context.RawMaterialSpecificGravities
                                where a.RawMaterialQCID == id
                                select a);
                if (thisItem.Any())
                {
                    return MapToDTO(thisItem.FirstOrDefault());
                }

                var newEntity = new RawMaterialSpecificGravity
                {
                    RawMaterialQCID = id,
                    DenIso = 0.7851, 
                    DateEntered = DateTime.Now,
                    EnteredBy = "acorrington",
                    LastModified = DateTime.Now,
                    ModifiedBy = "acorrington"
                };
                newEntity.RawMaterialQC = (from a in context.RawMaterialQCs
                    where a.ID == id
                    select a).FirstOrDefault();
                for (int i = 0; i < 5; i++)
                {
                    var dry = new TPO.DL.Models.RawMaterialSpecificGravityDetail
                    {
                        Order = (i + 1),
                        Submerged = false,
                        DateEntered = DateTime.Now,
                        EnteredBy = "acorrington",
                        LastModified = DateTime.Now,
                        ModifiedBy = "acorrington"
                    };
                    var submerged = new TPO.DL.Models.RawMaterialSpecificGravityDetail
                    {
                        Order = (i + 1),
                        Submerged = true,
                        DateEntered = DateTime.Now,
                        EnteredBy = "acorrington",
                        LastModified = DateTime.Now,
                        ModifiedBy = "acorrington"
                    };
                    newEntity.RawMaterialSpecificGravityDetails.Add(dry);
                    newEntity.RawMaterialSpecificGravityDetails.Add(submerged);
                }

                context.RawMaterialSpecificGravities.Add(newEntity);
                context.SaveChanges();

                var createdItem = (from a in context.RawMaterialSpecificGravities
                                where a.RawMaterialQCID == id
                                select a);
                if (createdItem.Any())
                {
                    return MapToDTO(createdItem.FirstOrDefault());
                }
                throw new Exception("Error creating Specific Gravity record.");
            }
        }
        public void Add(RawMaterialQCSpecificGravityDTO dto)
        {
            try 
            {
                using (var context = new TPOMVCApplicationEntities())
                {
                    var newEntity = new TPO.DL.Models.RawMaterialSpecificGravity();
                    newEntity.RawMaterialQCID = dto.RawMaterialQCID;
                    

                    newEntity.DateEntered = dto.DateEntered ?? DateTime.Now;
                    newEntity.EnteredBy = dto.EnteredBy;
                    newEntity.LastModified = dto.LastModified ?? DateTime.Now;
                    newEntity.ModifiedBy = dto.ModifiedBy;
                    context.RawMaterialSpecificGravities.Add(newEntity);
                    context.SaveChanges();
                    //need to return the new record id
                }
            }
            catch (Exception ex) 
            {
                throw;
            }
        }
        public void Update(RawMaterialQCSpecificGravityDTO dto) 
        {
            try
            {
                using (var context = new TPOMVCApplicationEntities())
                {
                    
                    // Save Specific gravity
                    var toUpdate = context.RawMaterialSpecificGravities.Find(dto.ID);
                    toUpdate.RawMaterialQCID = dto.RawMaterialQCID;
                    toUpdate.LabTechUserID = dto.LabTechUserID;
                    toUpdate.DenIso = dto.DenIso;

                    toUpdate.DateEntered = dto.DateEntered ?? DateTime.Now;
                    toUpdate.EnteredBy = dto.EnteredBy;

                    toUpdate.ModifiedBy = dto.ModifiedBy;
                    toUpdate.LastModified = DateTime.Now;

                    context.Entry(toUpdate).State = EntityState.Modified;

                    foreach (var detail in toUpdate.RawMaterialSpecificGravityDetails)
                    {
                        var dtoDetail = dto.RawMaterialSpecificGravityDetails.Where(d => d.ID == detail.ID);
                        if (dtoDetail.Any())
                        {
                            detail.Value = dtoDetail.First().Value;
                            detail.LastModified = DateTime.Now;
                        }
                    }

                    // TODO: Details

                    // Set Specific Gravity on Parent QC record.
                    RawMaterialQC toUpdateRawMaterialQC = context.RawMaterialQCs.Find(dto.RawMaterialQCID);
                    toUpdateRawMaterialQC.SpecGrav = dto.AverageGravity;
                    context.Entry(toUpdateRawMaterialQC).State = EntityState.Modified;

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        
        }
 

        public static RawMaterialQCSpecificGravityDTO MapToDTO(TPO.DL.Models.RawMaterialSpecificGravity dbo)
        {
            RawMaterialQCSpecificGravityDTO dto = new RawMaterialQCSpecificGravityDTO 
            { 
                ID = dbo.ID,
                RawMaterialQCID = dbo.RawMaterialQCID,
                RawMaterialCode = dbo.RawMaterialQC.RawMaterialReceived.RawMaterial.Code,
                RawMaterialLotCode = dbo.RawMaterialQC.RawMaterialReceived.LotNumber,
                AverageGravity = dbo.RawMaterialQC.SpecGrav ?? 0.00,
                DenIso = dbo.DenIso,
                LabTechUserID = dbo.LabTechUserID,
                DateEntered = dbo.DateEntered,
                EnteredBy = dbo.EnteredBy,
                LastModified = dbo.LastModified,
                ModifiedBy = dbo.ModifiedBy,
                RawMaterialSpecificGravityDetails = new List<RawMaterialQCSpecificGravityDetailDTO>()
            };
            
            foreach (RawMaterialSpecificGravityDetail detailLine in dbo.RawMaterialSpecificGravityDetails)
            {
                RawMaterialQCSpecificGravityDetailDTO dtoDetail = new RawMaterialQCSpecificGravityDetailDTO()
                {
                    ID = detailLine.ID,
                    RawMaterialSpecGravID = detailLine.RawMaterialSpecGravID,
                    Order = detailLine.Order,
                    Submerged = detailLine.Submerged,
                    Value = detailLine.Value,
                    DateEntered = dbo.DateEntered,
                    EnteredBy = dbo.EnteredBy,
                    LastModified = dbo.LastModified,
                    ModifiedBy = dbo.ModifiedBy
                };

                dto.RawMaterialSpecificGravityDetails.Add(dtoDetail);
            }
            return dto;
        }
    }
}
