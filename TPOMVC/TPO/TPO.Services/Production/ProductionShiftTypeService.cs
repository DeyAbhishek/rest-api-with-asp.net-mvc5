using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;

namespace TPO.Services.Production
{
    public class ProductionShiftTypeService : ServiceBaseGeneric<ProductionShiftTypeDto, ProductionShiftType>, ITpoService<ProductionShiftTypeDto>
    {

    }
}
