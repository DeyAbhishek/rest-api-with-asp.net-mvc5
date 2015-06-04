using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Domain.DTO;

namespace TPO.BL.Repositories.RawMaterial.RawMaterialQCSpecificGravity
{
    public interface IRawMaterialQCSpecificGravityRepository
    {
        RawMaterialQCSpecificGravityDTO GetByID(int id);
        RawMaterialQCSpecificGravityDTO GetByQCID(int id);
        void Add(RawMaterialQCSpecificGravityDTO RawMaterialQCSpecificGravity);
        void Update(RawMaterialQCSpecificGravityDTO RawMaterialQCSpecificGravity);
    }
}
