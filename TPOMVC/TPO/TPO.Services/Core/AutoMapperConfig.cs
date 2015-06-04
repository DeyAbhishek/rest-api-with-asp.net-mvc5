using System.Data.SqlClient;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Data;

namespace TPO.Services.Core
{
    internal class AutoMapperConfig
    {
        public static void Initialize()
        {
            CreateRawMaterialsMapping();
            CreateLineScrapMapping();
            CreatePlantMapping();
            CreateProductsMapping();
            CreateUserMapping();
            CreateScrimMapping();
            CreateUnitOfMeasureMapping();
            CreateProductionLineMapping();
            CreateTpoCurrentRawMaterialMapping();
            CreateTpoCurrentScrimMapping();
            CreateTpoFormulationMapping();
            CreateFailPropertiesMapping();
            CreateReclaimWipMapping();
            CreateDownTimeMapping();
            CreateProductionShiftMapping();
            CreateWorkOrderMapping();
            CreateWorkOrderShiftDataFormulationMapping();
            CreateWorkOrderShiftDataMapping();
            CreateIMProductionMapping();
        }

        private static void CreateProductionShiftMapping()
        {
            Mapper.CreateMap<SupervisorOnShift, SupervisorOnShiftDto>().ReverseMap();
            Mapper.CreateMap<ProductionShiftDto, ProductionShift>();

            Mapper.CreateMap<ProductionShift, ProductionShiftDto>()
                .ForMember(dest => dest.TypeCode, opt => opt.ResolveUsing<ProductionShiftTypeCodeResolver>());

            Mapper.CreateMap<ProductionShiftUseDto, ProductionShiftUse>();

            Mapper.CreateMap<ProductionShiftUse, ProductionShiftUseDto>()
                .ForMember(dest => dest.ShiftCode, opt => opt.MapFrom(src => src.ProductionShift.Code));
        }

        private class ProductionShiftTypeCodeResolver : ValueResolver<ProductionShift, string>
        {
            protected override string ResolveCore(ProductionShift source)
            {
                return source.ProductionShiftType.Code;
            }
        }

        private static void CreateDownTimeMapping()
        {
            Mapper.CreateMap<DownTimeReasonDto, DownTimeReason>().ReverseMap();
            Mapper.CreateMap<DownTimeEquipmentDto, DownTimeEquipment>().ReverseMap();
            Mapper.CreateMap<DownTimeEquipmentGroupDto, DownTimeEquipmentGroup>().ReverseMap();
            Mapper.CreateMap<DownTimeTypeDto, DownTimeType>().ReverseMap();
            Mapper.CreateMap<DownTimeDto, DownTime>().ReverseMap();
        }

        private static void CreateUnitOfMeasureMapping()
        {
            Mapper.CreateMap<UnitOfMeasureDto, UnitOfMeasure>().ReverseMap();
            Mapper.CreateMap<UoMConversionDto, UnitOfMeasureConversion>().ReverseMap();
            Mapper.CreateMap<UnitOfMeasureTypeDto, UnitOfMeasureType>().ReverseMap();
            Mapper.CreateMap<UnitOfMeasureDefaultDto, UnitOfMeasureDefault>();

            Mapper.CreateMap<UnitOfMeasureDefault, UnitOfMeasureDefaultDto>()
                .ForMember(dest => dest.UoMCode, opt => opt.ResolveUsing<UomCodeResolver>())
                .ForMember(dest => dest.UoMTypeDescription, opt => opt.ResolveUsing<UomTypeResolver>())
                .ForMember(dest => dest.UomTypeCode, opt => opt.ResolveUsing<UomTypeCodeResolver>());
            
            Mapper.CreateMap<UnitOfMeasureDefaultDto, UnitOfMeasureDefault>();
   
        }

        private class UomTypeCodeResolver : ValueResolver<UnitOfMeasureDefault, string>
        {
            protected override string ResolveCore(UnitOfMeasureDefault source)
            {
                return source.UnitOfMeasureType.Code;
            }
        }

        private class UomCodeResolver : ValueResolver<UnitOfMeasureDefault, string>
        {
            protected override string ResolveCore(UnitOfMeasureDefault source)
            {
                return source.UnitOfMeasure.Code;
            }
        }

        private class UomTypeResolver : ValueResolver<UnitOfMeasureDefault, string>
        {
            protected override string ResolveCore(UnitOfMeasureDefault source)
            {
                return source.UnitOfMeasureType.Description;
            }
        }

        private static void CreateScrimMapping()
        {
            Mapper.CreateMap<ScrimTypeDto, ScrimType>().ReverseMap();
            Mapper.CreateMap<ScrimRollDto, ScrimRoll>().ReverseMap();
            Mapper.CreateMap<ScrimActionDto, ScrimAction>().ReverseMap();
            Mapper.CreateMap<ScrimActionReasonDto, ScrimActionReason>().ReverseMap();
            Mapper.CreateMap<ScrimActionTypeDto, ScrimActionType>().ReverseMap();
        }

        private static void CreateRawMaterialsMapping()
        {
            Mapper.CreateMap<RawMaterialReceivedDto, RawMaterialReceived>().ReverseMap();
            Mapper.CreateMap<RawMaterialDto, RawMaterial>();

            Mapper.CreateMap<RawMaterial, RawMaterialDto>()
                .ForMember(dest => dest.VendorName, opt => opt.ResolveUsing<VendorNameResolver>());
           
            Mapper.CreateMap<RawMaterialQcDto, RawMaterialQC>().ReverseMap();
            Mapper.CreateMap<RawMaterialSpecificGravityDto, RawMaterialSpecificGravity>().ReverseMap();
            Mapper.CreateMap<RawMaterialQcSpecificGravityDetailDto, RawMaterialSpecificGravityDetail>().ReverseMap();
            Mapper.CreateMap<RawMaterialTestDto, RawMaterialTest>().ReverseMap();
            Mapper.CreateMap<RawMaterialQcRedHoldDto, RawMaterialQCRedHold>().ReverseMap();
            Mapper.CreateMap<RawMaterialVendorDto, RawMaterialVendor>().ReverseMap();
            Mapper.CreateMap<RawMaterialReceivedSizeLimitDto, RawMaterialReceivedSizeLimit>().ReverseMap();
            Mapper.CreateMap<RawMaterialActionDto, RawMaterialAction>().ReverseMap();
            Mapper.CreateMap<TestLimitTypeDto, TestLimitType>().ReverseMap();
            Mapper.CreateMap<QCVisualInspectionTypeDto, QCVisualInspectionType>().ReverseMap();
        }

        private class VendorNameResolver : ValueResolver<RawMaterial, string>
        {
            protected override string ResolveCore(RawMaterial source)
            {
                return source.RawMaterialVendor.Vendor;
            }
        }

        private static void CreateProductsMapping()
        {
            Mapper.CreateMap<IMProductDto, IMProduct>().ReverseMap();
            Mapper.CreateMap<IMProductTypeDto, IMProductType>().ReverseMap();
            Mapper.CreateMap<TPOProductDto, TPOProduct>().ReverseMap();
            Mapper.CreateMap<TPOQCLimitDto, TPOProduct>().ReverseMap();
            Mapper.CreateMap<ProductionLinesDto, ProdLine>().ReverseMap();
            Mapper.CreateMap<TPOProductStandardCostDto, TPOProductStandardCost>().ReverseMap();
            Mapper.CreateMap<TPOQCLimitDto.TPOProductCapCoreDetailDto, TPOProductCapCoreDetail>().ReverseMap();
            Mapper.CreateMap<TPOQCLimitDto.TPOProductCEDetailDto, TPOProductCEDetail>().ReverseMap();
            Mapper.CreateMap<TPOQCLimitDto.TPOProductDimDetailDto, TPOProductDimDetail>().ReverseMap();
            Mapper.CreateMap<TPOQCLimitDto.TPOProductGrabDetailDto, TPOProductGrabDetail>().ReverseMap();
            Mapper.CreateMap<TPOProductDto.TPOProductLabelDetailDto, TPOProductLabelDetail>().ReverseMap();
            Mapper.CreateMap<ProductionBudgetDto, ProductionBudget>().ReverseMap();

            Mapper.CreateMap<TPOCProductRollDto, TPOCProductRoll>();
            Mapper.CreateMap<TPOCProductRoll, TPOCProductRollDto>()
                .ForMember(dest => dest.ShiftCode, opt => opt.MapFrom(src => src.ProductionShift.Code))
                .ForMember(dest => dest.WorkOrderCode, opt => opt.MapFrom(src => src.WorkOrder.Code))
                .ForMember(dest => dest.ProductCode, opt => opt.MapFrom(src => src.TPOProduct.ProductCode))
                .ForMember(dest => dest.LengthUoM, opt => opt.MapFrom(src => src.UnitOfMeasure.Code))
                .ForMember(dest => dest.WeightUoM, opt => opt.MapFrom(src => src.UnitOfMeasure1.Code));

            Mapper.CreateMap<TPOBatchDto, TPOBatch>().ReverseMap();
        }

        private static void CreatePlantMapping()
        {
            Mapper.CreateMap<PlantDto, Plant>().ReverseMap();
        }

        private static void CreateUserMapping()
        {
            Mapper.CreateMap<UserDto, User>().ReverseMap();
            Mapper.CreateMap<UserPlantDto, UserPlant>().ReverseMap();
            Mapper.CreateMap<RoleDto, webpages_Roles>().ReverseMap();
            Mapper.CreateMap<RoleAssignmentDto, RoleAssignment>().ReverseMap();

        }

        private static void CreateLineScrapMapping() 
        {
            Mapper.CreateMap<TPOLineScrapCodeGroupDto, TPOLineScrapCodeGroup>().ReverseMap();
            Mapper.CreateMap<TPOLineScrapCodeDto, TPOLineScrapCode>().ReverseMap();
            Mapper.CreateMap<TPOLineScrapDto, TPOLineScrap>().ReverseMap();
            Mapper.CreateMap<TPOLineScrapTypeDto, TPOLineScrapType>().ReverseMap();
            Mapper.CreateMap<TPOReworkRollDto, TPOReworkRoll>().ReverseMap();
        }

        private static void CreateProductionLineMapping()
        {
            // ProdLinesPerformProd
            Mapper.CreateMap<ProductionLinesDto, ProdLine>().ReverseMap();
            Mapper.CreateMap<ProdLinesPerformProdDto, ProdLinesPerformProd>();

            Mapper.CreateMap<ProdLinesPerformProd, ProdLinesPerformProdDto>()
                .ForMember(dest => dest.ProductName, opt => opt.ResolveUsing<ProductNameResolver>());
            
            Mapper.CreateMap<ProdLinesPerformDto, ProdLinesPerform>().ReverseMap();
            Mapper.CreateMap<ProdLineRollConfig, ProdLineRollConfigDto>().ReverseMap();
            Mapper.CreateMap<ProdLineType, ProdLineTypeDto>().ReverseMap();

            Mapper.CreateMap<ProdDateChangeDto, ProdDateChange>().ReverseMap();
            Mapper.CreateMap<ProductionShiftTypeDto, ProductionShiftType>().ReverseMap();
        
            Mapper.CreateMap<ProductionLineSchedule, ProductionLineScheduleDto>().ReverseMap();
            Mapper.CreateMap<ProdComment, ProdCommentDto>().ReverseMap();
        }

        private class ProductNameResolver : ValueResolver<ProdLinesPerformProd, string>
        {
            protected override string ResolveCore(ProdLinesPerformProd source)
            {
                return source.TPOProduct.ProductCode;
            }
        }

        private static void CreateTpoCurrentRawMaterialMapping()
        {
            Mapper.CreateMap<TPOCurrentRawMaterialDto, TPOCurrentRawMaterial>();

            Mapper.CreateMap<TPOCurrentRawMaterial, TPOCurrentRawMaterialDto>()
                .ForMember(dest => dest.LotNumber, opt => opt.MapFrom(src => src.RawMaterialReceived.LotNumber));

                
        }

        private static void CreateTpoCurrentScrimMapping()
        {
            Mapper.CreateMap<TPOCurrentScrimDto, TPOCurrentScrim>().ReverseMap();
        }

        private static void CreateTpoFormulationMapping()
        {
            Mapper.CreateMap<TPOFormulationDto, TPOFormulation>().ReverseMap();
            Mapper.CreateMap<TPOExtruderDto, TPOExtruder>().ReverseMap();
            Mapper.CreateMap<TPOFormulationRawMaterialDto, TPOFormulationRawMaterial>().ReverseMap();
            Mapper.CreateMap<TPOFormulationLineProductDto, TPOFormulationLineProduct>().ReverseMap();
        }

        private static void CreateFailPropertiesMapping()
        {
            Mapper.CreateMap<FailPropertyDto, FailProperty>().ReverseMap();
        }
    
        private static void CreateReclaimWipMapping()
        {
            Mapper.CreateMap<TPOReclaimActionDto, TPOReclaimAction>().ReverseMap();
            Mapper.CreateMap<TPOReclaimWIPDto, TPOReclaimWIP>().ReverseMap();
            Mapper.CreateMap<TPOReclaimActionTypeDto, TPOReclaimActionType>().ReverseMap();
        }

        private static void CreateWorkOrderMapping() 
        {
            Mapper.CreateMap<WorkOrder, WorkOrderDto>().ReverseMap();
        }

        private static void CreateWorkOrderShiftDataFormulationMapping()
        {
            Mapper.CreateMap<WorkOrderShiftDataFormulation, WorkOrderShiftDataFormulationDto>().ReverseMap();
        }

        private static void CreateWorkOrderShiftDataMapping()
        {
            Mapper.CreateMap<WorkOrderShiftData, WorkOrderShiftDataDto>().ReverseMap();
        }

        private static void CreateIMProductionMapping()
        {
            Mapper.CreateMap<IMProd, IMProdDto>().ReverseMap();
        }
    }
}
