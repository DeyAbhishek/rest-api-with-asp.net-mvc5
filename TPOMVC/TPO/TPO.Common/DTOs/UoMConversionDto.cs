
namespace TPO.Common.DTOs
{
    public class UoMConversionDto
    {
        public int SourceUnitOfMeasureId { get; set; }
        public int TargetUnitOfMeasureId { get; set; }
        public decimal ConversionAmount { get; set; }
    }
}
