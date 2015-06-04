using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using TPO.BL.Repositories.RawMaterial.RawMaterialQCSpecificGravity;

namespace TPO.BL.RawMaterials
{
    public class RawMaterialQCSpecificGravity
    {
        private static RawMaterialQCSpecificGravityRepository _repository = new RawMaterialQCSpecificGravityRepository();
        public static void Add(Domain.DTO.RawMaterialQCSpecificGravityDTO RawMaterialQCSpecificGravityDTO)
        {
            try
            {
                //TODO: set actual modified by from session
                RawMaterialQCSpecificGravityDTO.ModifiedBy = RawMaterialQCSpecificGravityDTO.EnteredBy;
                RawMaterialQCSpecificGravityDTO.LastModified = DateTime.Now;
                _repository.Add(RawMaterialQCSpecificGravityDTO);
            }
            catch (Exception)
            {
                //TODO: handle error
                // add return result object
                return;
            }
        }

        public static Domain.DTO.RawMaterialQCSpecificGravityDTO Get(int Id)
        {
            return _repository.GetByID(Id);
        }

        /*
        public static void Update(Domain.DTO.RawMaterialQCSpecificGravityDTO dto)
        {
            dto.LastModified = DateTime.Now;
            dto.ModifiedBy = "acorrington"; //TODO: pull from current user
            _repository.Update(dto);
        }
        */
        public Domain.DTO.RawMaterialQCSpecificGravityDTO GetByRawMaterialQcId(int id)
        {
            return _repository.GetByQCID(id);
        }
        
    }
}