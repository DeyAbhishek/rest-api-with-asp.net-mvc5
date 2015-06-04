using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.BL.RawMaterials
{
    public class RawMaterial
    {
        public List<TPO.Model.RawMaterials.RawMaterialModel> GetRawMaterials()
        {
            List<TPO.Model.RawMaterials.RawMaterialModel> RawMaterialQCID = new List<Model.RawMaterials.RawMaterialModel>();
            RawMaterialQCID.Add(new TPO.Model.RawMaterials.RawMaterialModel(){ RawMaterialId = 1, RawMaterialCode = "CA10A", RawMaterialName = "Resin"});
            RawMaterialQCID.Add(new TPO.Model.RawMaterials.RawMaterialModel() { RawMaterialId = 2, RawMaterialCode = "Cal Sterate FN PWD", RawMaterialName = "Compounding Raw" });
            RawMaterialQCID.Add(new TPO.Model.RawMaterials.RawMaterialModel() { RawMaterialId = 3, RawMaterialCode = "CM2157 – Tan", RawMaterialName = "Color" });
            return RawMaterialQCID;
        }
    }
}
