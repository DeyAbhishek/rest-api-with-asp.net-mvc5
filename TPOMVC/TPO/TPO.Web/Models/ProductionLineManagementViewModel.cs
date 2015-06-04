using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPO.Common.DTOs;

namespace TPO.Web.Models
{
    public class ProductionLineManagementViewModel : BaseViewModel
    {
        public ProdLineTypeDto ProdLineType { get; set; }
        public ProdLineRollConfigDto ProdLineRollConfig { get; set; }
        public List<ProductionLinesDto> ProdLine { get; set; }
    }
}