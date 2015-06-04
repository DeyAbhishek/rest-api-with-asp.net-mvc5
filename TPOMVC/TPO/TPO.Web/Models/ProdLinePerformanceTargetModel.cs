using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Services.Application;

namespace TPO.Web.Models
{
    public class ProdLinePerformanceTargetModel : BaseViewModel
    {
        [Display]
        public bool CanShow { get; set; }
        public int LocID { get; set; }
        public string LineCode { get; set; }
        [DisplayName("Target Uptime %:")]
        [Range(0,100)]
        public double Uptime { get; set; }
        [DisplayName("Target Yield %:")]
        [Range(0, 100)]
        public double Yield { get; set; }
        [DisplayName("Target Throughput:")]
        public double Throughput { get; set; }
        //public string TPLabel { get; set; }
        [DisplayName("Target OEE %:")]
        [Range(0, 100)]
        public double OEE { get; set; }
        [DisplayName("Set Throughput By:")]
        public string TPTUse { get; set; }
        public System.DateTime DateChange { get; set; }
        [DisplayName("Schedule Factor %:")]
        [Range(0, 100)]
        public double SchedFactor { get; set; }
        public int ProdLineID { get; set; }
        public string ProdLineDescCode { get; set; }

        public System.DateTime DateEntered { get; set; }
        public string EnteredBy { get; set; }
        public System.DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
        public int? ThroughputUoMID { get; set; }



        private class TP
        {
            public string Code { get; set; }
            public string Description { get; set; }
        }
        private readonly List<TP> _tpList = new List<TP>()
        {
            new TP() { Code = "P", Description = "Product"},
            new TP() { Code = "L", Description = "Line"},
        };

        public IEnumerable<SelectListItem> TPList
        {
            get { return new SelectList(_tpList, "Code", "Description"); }
        }

        private List<UnitOfMeasureModel> _uomList;

        public IEnumerable<SelectListItem> UomList
        {
            get
            {
                if (_uomList == null)
                {
                    _uomList = new List<UnitOfMeasureModel>();
                    using (UnitOfMeasureService service = new UnitOfMeasureService())
                    {
                        string[] codes = {"A", "C", "L", "W"};
                        foreach (string code in codes)
                        {
                            var dto = service.GetByTypeCode(code);
                            _uomList.AddRange(Mapper.Map<List<UnitOfMeasureDto>, List<UnitOfMeasureModel>>(dto));
                        }
                    }
                }
                return new SelectList(_uomList, "ID", "Code");
            }
        }
    }
}