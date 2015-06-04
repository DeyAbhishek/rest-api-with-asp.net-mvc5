using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Services.Core;
using TPO.Common.DTOs;
using TPO.Data;
using System.Data.Entity.Validation;
using AutoMapper;

namespace TPO.Services.Scrim
{
    public class ScrimActionService : ServiceBaseGeneric<ScrimActionDto, ScrimAction>, ITpoService<ScrimActionDto>
    {
    }
}
