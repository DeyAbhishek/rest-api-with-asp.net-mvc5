using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Domain.DTO;

namespace TPO.BL.Repositories.RawMaterial.RawMaterialReceivedRepository
{
    public interface IRawMaterialReceivedRepository
    {
        List<Int32> GetAvailableRawMaterialIds();
        List<Int32> GetAvailableRawMaterialIds(int plantId);
        List<RawMaterialReceivedDTO> GetAll();
        List<RawMaterialReceivedDTO> GetAll(int plantId);
        List<RawMaterialReceivedDTO> GetAll(int plantId,int materialId);
        RawMaterialReceivedDTO GetById(int id);
        void Add(RawMaterialReceivedDTO rawMaterialReceived);
        void Update(RawMaterialReceivedDTO rawMaterialReceived);
        void Delete(int id);
    }
}
