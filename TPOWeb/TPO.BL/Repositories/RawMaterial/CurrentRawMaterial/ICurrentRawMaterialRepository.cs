using System;
using System.Collections.Generic;
using TPO.Domain.DTO;

namespace TPO.BL.Repositories.RawMaterial.CurrentRawMaterial
{
    public interface ICurrentRawMaterialRepository
    {
        List<CurrentRawMaterialDTO> GetAll();
        List<CurrentRawMaterialDTO> GetAll(int plantId);
        List<CurrentRawMaterialDTO> GetAll(int plantId, string lineId);
        CurrentRawMaterialDTO GetById(int id);
        void Add(CurrentRawMaterialDTO currentRawMaterial);
        void Update(CurrentRawMaterialDTO currentRawMaterial);
        void Delete(int id);
    }
}