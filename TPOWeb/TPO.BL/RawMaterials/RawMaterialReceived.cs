using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using TPO.BL.Repositories.RawMaterial.RawMaterialReceived;

namespace TPO.BL.RawMaterials
{
    public class RawMaterialReceived
    {
        private static RawMaterialReceivedRepository _repository = new RawMaterialReceivedRepository();
        public static void Add(Domain.DTO.RawMaterialReceivedDTO rawMaterialReceivedDTO)
        {
            try
            {
                //TODO: set actual modified by from session
                rawMaterialReceivedDTO.LastModified = DateTime.Now;
                _repository.Add(rawMaterialReceivedDTO);
            }
            catch (Exception)
            {
                //TODO: handle error
                // add return result object
                return;
            }
        }
        
        public static List<Domain.DTO.RawMaterialReceivedDTO> GetAll()
        {
            return _repository.GetAll();
        }

        public static List<Domain.DTO.RawMaterialReceivedDTO> GetAll(int plantId, int materialId)
        {
            if (materialId > 0)
            {
                return _repository.GetAll(plantId, materialId);
            }

            return _repository.GetAll(plantId);
        }

        public static Domain.DTO.RawMaterialReceivedDTO Get(int Id)
        {
            return _repository.GetById(Id);
        }

        public static void Delete(int Id)
        {
            _repository.Delete(Id);
        }

        public static void Update(Domain.DTO.RawMaterialReceivedDTO dto)
        {
            dto.LastModified = DateTime.Now;
            _repository.Update(dto);
        }
    }
}