using System;
using System.Collections.Generic;
using TPO.Domain.DTO;

namespace TPO.BL.Repositories.RawMaterial.RawMaterialQCRedHold
{
    public interface IRawMaterialQCRedHoldRepository
    {
        RawMaterialQCRedHoldDTO GetByID(int id);
        RawMaterialQCRedHoldDTO GetByQCID(int id);
        int Add(RawMaterialQCRedHoldDTO rawMaterialQCRedHold);
        void Update(RawMaterialQCRedHoldDTO rawMaterialQCRedHold);
        void Delete(int id);
    }
}
