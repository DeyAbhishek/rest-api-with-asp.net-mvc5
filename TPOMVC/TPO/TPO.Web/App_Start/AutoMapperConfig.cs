using System;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Web.Models;

namespace TPO.Web
{
    public class AutoMapperConfig
    {
        public AutoMapperConfig()
        {
            Initialize();
        }

        private static bool _isInitialized = false;

        public static void Initialize()
        {
            if (_isInitialized)
            {
                return;
            }

            SetupPlantMap();
            SetupRawMaterialsMap();
            SetupScrimMap();
            SetupUoMMap();
            SetupTpoFormulationMap();
            SetupProductsMap();
            SetupReclaimMap();
            SetUpScrapCodesMap();
            SetupUserMapping();
            SetupDownTimeMap();
            SetupProductionLinesMap();
            SetupProductionShiftMap();
            SetupWorkOrderMap();
            SetupIMProductionMap();

            _isInitialized = true;
        }

        private static void SetupProductionShiftMap()
        {
            Mapper.CreateMap<ProductionShiftDto, ProductionShiftModel>()
                .ForMember(dest => dest.StartTimeStr, opt => opt.MapFrom(src => src.StartTime.ToString(@"hh\:mm")))
                .ForMember(dest => dest.EndTimeStr, opt => opt.MapFrom(src => src.EndTime.ToString(@"hh\:mm")));

            Mapper.CreateMap<ProductionShiftModel, ProductionShiftDto>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.StartTimeStr)))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.EndTimeStr)));

            Mapper.CreateMap<ProductionShiftUseDto, ProductionShiftUseModel>().ReverseMap();
            Mapper.CreateMap<SupervisorOnShiftDto, SupervisorOnShiftModel>().ReverseMap();
        }

        private static void SetupDownTimeMap()
        {
            Mapper.CreateMap<DownTimeReasonDto, DownTimeReason>().ReverseMap();
            Mapper.CreateMap<DownTimeEquipmentGroupDto, DownTimeEquipmentGroup>().ReverseMap();
            Mapper.CreateMap<DownTimeEquipmentDto, DownTimeEquipment>().ReverseMap();
            Mapper.CreateMap<DownTimeTypeDto, DownTimeTypeDto>().ReverseMap();
            Mapper.CreateMap<DownTimeDto, DownTimeModel>().ReverseMap();
        }

        private static void SetupUoMMap()
        {
            Mapper.CreateMap<UnitOfMeasureDto, UnitOfMeasureModel>().ReverseMap();
            Mapper.CreateMap<UnitOfMeasureTypeDto, UnitOfMeasureTypeModel>().ReverseMap();
            Mapper.CreateMap<UnitOfMeasureDefaultDto, UnitOfMeasureDefaultViewModel>().ReverseMap();
        }

        private static void SetupScrimMap()
        {
            Mapper.CreateMap<ScrimTypeDto, ScrimTypeModel>().ReverseMap();
            Mapper.CreateMap<ScrimRollDto, ScrimRollModel>()
                .ReverseMap();
            Mapper.CreateMap<ScrimActionDto, ScrimActionModel>().ReverseMap();
            Mapper.CreateMap<ScrimActionReasonDto, ScrimActionReasonModel>().ReverseMap();
            Mapper.CreateMap<ScrimActionTypeDto, ScrimActionTypeModel>().ReverseMap();
            Mapper.CreateMap<TPOCurrentScrimDto, TPOCurrentScrimModel>().ReverseMap();
        }

        private static void SetupPlantMap()
        {
            Mapper.CreateMap<PlantDto, Plant>().ReverseMap();
        }

        private static void SetupProductsMap()
        {
            Mapper.CreateMap<TPOQCLimit, TPOQCLimitDto>().ReverseMap();
            Mapper.CreateMap<TPOProduct, TPOProductDto>().ReverseMap();
            Mapper.CreateMap<IMProduct, IMProductDto>().ReverseMap();
            Mapper.CreateMap<IMProductTypeDto, IMProductTypeModel>().ReverseMap();
            Mapper.CreateMap<TPOProductStandardCostDto, TPOProductStandardCost>().ReverseMap();
            Mapper.CreateMap<ProdLinesPerformProdDto, ProdLinesPerformanceTargetProductModel>().ReverseMap();
            Mapper.CreateMap<ProdLinesPerformDto, ProdLinePerformanceTargetModel>()
                .ForMember(dest => dest.OEE, opt => opt.MapFrom(src => src.OEE*100.0))
                .ForMember(dest => dest.SchedFactor, opt => opt.MapFrom(src => src.SchedFactor*100.0))
                .ForMember(dest => dest.Uptime, opt => opt.MapFrom(src => src.Uptime * 100.0))
                .ForMember(dest => dest.Yield, opt => opt.MapFrom(src => src.Yield*100.0));

            Mapper.CreateMap<ProdLinePerformanceTargetModel, ProdLinesPerformDto>()
                .ForMember(dest => dest.OEE, opt => opt.MapFrom(src => src.OEE / 100.0))
                .ForMember(dest => dest.SchedFactor, opt => opt.MapFrom(src => src.SchedFactor / 100.0))
                .ForMember(dest => dest.Uptime, opt => opt.MapFrom(src => src.Uptime / 100.0))
                .ForMember(dest => dest.Yield, opt => opt.MapFrom(src => src.Yield / 100.0));

            Mapper.CreateMap<ProductionLinesDto, ProductionLinesModel>().ReverseMap();

            Mapper.CreateMap<TPOQCLimit.CapCoreDetail, TPOQCLimitDto.TPOProductCapCoreDetailDto>().ReverseMap();
            Mapper.CreateMap<TPOQCLimit.CEDetail, TPOQCLimitDto.TPOProductCEDetailDto>().ReverseMap();
            Mapper.CreateMap<TPOQCLimit.DimDetail, TPOQCLimitDto.TPOProductDimDetailDto>().ReverseMap();
            Mapper.CreateMap<TPOQCLimit.GrabDetail, TPOQCLimitDto.TPOProductGrabDetailDto>().ReverseMap();
            Mapper.CreateMap<TPOProduct.LabelDetail, TPOProductDto.TPOProductLabelDetailDto>().ReverseMap();
            Mapper.CreateMap<TPOCProductRollDto, TPOCProductRollModel>().ReverseMap();
        }

        private static void SetupTpoFormulationMap()
        {
            Mapper.CreateMap<TPOFormulationDto, TPOFormulationModel>().ReverseMap();
            Mapper.CreateMap<TPOExtruderDto, TPOExtruderModel>().ReverseMap();
            Mapper.CreateMap<TPOFormulationRawMaterialDto, TPOFormulationRawMaterialModel>().ReverseMap();
            Mapper.CreateMap<TPOFormulationLineProductDto, TPOFormulationLineProductModel>().ReverseMap();
        }

        private static void SetupRawMaterialsMap()
        {
            Mapper.CreateMap<Common.DTOs.RawMaterialReceivedDto, Models.RawMaterialReceived>().ReverseMap();
            Mapper.CreateMap<Common.DTOs.RawMaterialDto, Models.RawMaterial>().ReverseMap();
            Mapper.CreateMap<Common.DTOs.RawMaterialQcDto, Models.RawMaterialQc>().ReverseMap();
            Mapper.CreateMap<Common.DTOs.RawMaterialVendorDto, Models.RawMaterialVendorModel>().ReverseMap();
            Mapper.CreateMap<Common.DTOs.RawMaterialReceivedSizeLimitDto, Models.RawMaterialReceivedSizeLimitModel>().ReverseMap();
            Mapper.CreateMap<TestLimitTypeDto, TestLimitTypeModel>().ReverseMap();
            Mapper.CreateMap<RawMaterialTestDto, RawMaterialTestModel>().ReverseMap();
            Mapper.CreateMap<RawMaterialSpecificGravityDto, RawMaterialQcSpecificGravity>().ReverseMap();
            Mapper.CreateMap<RawMaterialQcSpecificGravityDetailDto, RawMaterialQcSpecificGravityDetail>().ReverseMap();
            Mapper.CreateMap<RawMaterialQcRedHoldDto, RawMaterialQCRedHoldViewModel>().ReverseMap();
            Mapper.CreateMap<QCVisualInspectionTypeDto, QCVisualInspectionTypeModel>().ReverseMap();
            Mapper.CreateMap<TPOCurrentRawMaterialDto, TPOCurrentRawMaterialModel>().ReverseMap();
        }

        public static void SetupReclaimMap()
        {
            Mapper.CreateMap<TPOReclaimWIPDto, TPOReclaimWIPModel>().ReverseMap();
            Mapper.CreateMap<TPOReclaimActionDto, TPOReclaimActionModel>().ReverseMap();

        }

        public static void SetupReworkMap() 
        {
            Mapper.CreateMap<ReworkProductionEntryDto, ReworkProductionEntryModel>().ReverseMap();
        }

        private static void SetUpScrapCodesMap() 
        {
            Mapper.CreateMap<TPOLineScrapCodeGroupDto, TPOLineScrapCodeGroup>().ReverseMap();
            Mapper.CreateMap<TPOLineScrapCode, TPOLineScrapCodeDto>().ReverseMap();
            Mapper.CreateMap<TPOLineScrapDto, LineScrapModel>().ReverseMap();
            Mapper.CreateMap<TPOLineScrapTypeDto, TPOLineScrapTypeModel>().ReverseMap();
            Mapper.CreateMap<TPOReworkRollDto, TPOReworkRollModel>().ReverseMap();
        }

        private static void SetupUserMapping()
        {
            Mapper.CreateMap<UserDto, UserModel>()
                //.ForMember(dest => dest.SecurityTasks, opt => opt.MapFrom(src => src.webpages_UsersInRoles.Each(s =>s.))
                .ReverseMap();

            Mapper.CreateMap<UserPlantDto, UserPlantModel>().ReverseMap();
            Mapper.CreateMap<RoleDto, RoleModel>().ReverseMap();
            Mapper.CreateMap<RoleAssignmentDto, RoleAssignmentModel>().ReverseMap();
            Mapper.CreateMap<UserDto, SecurityAddModel>().ReverseMap();
            Mapper.CreateMap<UserDto, SecurityEditModel>().ReverseMap();

        }

        private static void SetupProductionLinesMap() 
        {
            Mapper.CreateMap<ProductionLinesDto, ProductionLineManagementModel>().ReverseMap();
            Mapper.CreateMap<ProdLineTypeDto, ProdLineTypeModel>().ReverseMap();
            Mapper.CreateMap<ProdLineRollConfigDto, ProdLineRollConfigViewModel>().ReverseMap();

            Mapper.CreateMap<ProdDateChangeDto, ProdDateChangeModel>().ReverseMap();
            Mapper.CreateMap<ProductionShiftTypeDto, ProductionShiftTypeModel>().ReverseMap();
            Mapper.CreateMap<ProductionLineScheduleDto, ProductionLineScheduleModel>().ReverseMap();
            Mapper.CreateMap<ProductionBudgetDto, TPOMonthlyProductionBudgetModel>().ReverseMap();

            Mapper.CreateMap<ProdCommentDto, ProdCommentModel>().ReverseMap();
        }

        private static void SetupWorkOrderMap() 
        {
            Mapper.CreateMap<WorkOrderDto, WorkOrderModel>().ReverseMap();
        }

        private static void SetupIMProductionMap()
        {
            Mapper.CreateMap<IMProdDto, IMProdModel>().ReverseMap();
        }
    }
}