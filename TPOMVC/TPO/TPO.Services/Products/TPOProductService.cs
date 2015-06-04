using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;
using TPO.Services.Application;

namespace TPO.Services.Products
{
    public class TPOProductService : ServiceBase, ITpoService<TPOProductDto>
    {
        public int Add(TPOProductDto dto)
        {
            var entity = Mapper.Map<TPOProductDto, TPOProduct>(dto);
            //Need to set UseCELogo here
            entity.TPOProductCEDetail = new TPOProductCEDetail();
            entity.TPOProductCEDetail.UseCELogo = dto.UseCELogo;
            entity.TPOProductCapCoreDetail = new TPOProductCapCoreDetail();
            entity.TPOProductDimDetail = new TPOProductDimDetail();
            entity.TPOProductGrabDetail = new TPOProductGrabDetail();
            UnitOfMeasureService uomService = new UnitOfMeasureService();
            entity.TPOProductDimDetail.DimStabTempUoM = uomService.GetByTypeCode("Y").FirstOrDefault().ID;
            entity.ThickTestUoM = uomService.GetByTypeCode("T").FirstOrDefault().ID;
            entity.ThickTestUoM2 = uomService.GetByTypeCode("T").FirstOrDefault().ID;
            entity.WidthTestUoM = uomService.GetByTypeCode("D").FirstOrDefault().ID;
            entity.ForceTestUoM = uomService.GetByTypeCode("F").FirstOrDefault().ID;
            entity.PunctUoM = uomService.GetByTypeCode("W").FirstOrDefault().ID;
            try
            {
                _repository.Repository<TPOProduct>().Insert(entity);
                _repository.Save();
            }
            catch (DbEntityValidationException valEx)
            {
                HandleValidationException(valEx);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
            return entity.ID;
        }

        public List<TPOProductDto> GetAll()
        {
            var entities = _repository.Repository<TPOProduct>().GetAll().ToList();
            return Mapper.Map<List<TPOProduct>, List<TPOProductDto>>(entities);
        }


        public List<TPOProductDto> GetAllByProdLineId(int prodLineId)
        {
            var formulations =
                _repository.Repository<TPOFormulationLineProduct>().GetAllBy(f => f.ProdLineID == prodLineId);

            var entities = formulations.Select(p => p.TPOProduct).Distinct().ToList();
            return Mapper.Map<List<TPOProduct>, List<TPOProductDto>>(entities);
        }

        public List<TPOProductDto> GetByPlant(int plantID) 
        {
            Expression<Func<TPOProduct, bool>> filterExpression = p => p.PlantID == plantID;
            var entities = _repository.Repository<TPOProduct>().GetAllBy(filterExpression).ToList();
            return Mapper.Map<List<TPOProduct>, List<TPOProductDto>>(entities);
        }

        public List<TPOProductDto> GetReworkProducts(int productInID) 
        {
            List<TPOProductDto> data = new List<TPOProductDto>();

            var dto = Get(productInID);

            var productCode = dto.ProductCode.Substring(4, 3);
            switch (productCode) 
            {
                case "TPM": 
                    {
                        data.AddRange(GetReworkTPM(dto.ProductCode, dto.Thick, dto.PlantID));
                    }break;
                case "TPO": 
                    {
                        data.AddRange(GetReworkTPO(dto.ProductCode, dto.Thick, dto.PlantID));
                    } break;
                case "TPR":
                    {
                        data.AddRange(GetReworkTPR(dto.ProductCode, dto.Thick, dto.PlantID));
                    } break;
                default: 
                    {
                        if (dto.ProductCode.Substring(4, 1) == "V") 
                        {
                            data.AddRange(GetReworkV(dto.ProductCode, dto.Thick, dto.PlantID));
                        }
                    }break;
            }
            return data.OrderBy(p => p.ProductCode).ToList();
        }

        public TPOProductDto Get(int id)
        {
            var entity = _repository.Repository<TPOProduct>().GetById(id);
            return Mapper.Map<TPOProduct, TPOProductDto>(entity);
        }

        public TPOProductDto GetByWorkOrderID(int workOrderID) 
        {
            var entity = _repository.Repository<TPOProduct>().GetAllBy(tpo => tpo.WorkOrders.Where(wo => wo.ID == workOrderID).Count() > 0).FirstOrDefault();
            return Mapper.Map<TPOProduct, TPOProductDto>(entity);
            
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<TPOProduct>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(TPOProductDto dto)
        {
            try
            {
                var entity = _repository.Repository<TPOProduct>().GetById(dto.ID);
                entity = Mapper.Map(dto, entity);
                entity.TPOProductCEDetail.UseCELogo = dto.UseCELogo;
                _repository.Repository<TPOProduct>().Update(entity);
                _repository.Save();
            }
            catch (DbEntityValidationException valEx)
            {
               HandleValidationException(valEx);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        private List<TPOProductDto> GetReworkTPM(string productIn, double thick, int plantID) 
        {
            Expression<Func<TPOProduct, bool>> expression = p => p.PlantID == plantID
                                                    && p.Thick == thick
                                                    && p.ProductCode.Substring(4, 4) == productIn.Substring(4, 4)
                                                    && (p.ProductCode != productIn || (p.ProductCode == "W56TPO3024" || p.ProductCode == "W590030000"));
            var entities = _repository.Repository<TPOProduct>().GetAllBy(expression).ToList();
            return Mapper.Map<List<TPOProduct>, List<TPOProductDto>>(entities);
        }

        private List<TPOProductDto> GetReworkTPO(string productIn, double thick, int plantID)
        {
            Expression<Func<TPOProduct, bool>> expression = p => p.PlantID == plantID
                                                    && p.Thick == thick
                                                    && (p.ProductCode.Substring(4,3) == "TPO" || p.ProductCode.Substring(4,3) == "TPS" || p.ProductCode.Substring(0,3) == "W59")
                                                    && (p.ProductCode != productIn || (p.ProductCode == "W56TPO3024" || p.ProductCode == "W590030000"));
            var entities = _repository.Repository<TPOProduct>().GetAllBy(expression).ToList();
            return Mapper.Map<List<TPOProduct>, List<TPOProductDto>>(entities);
        }
        private List<TPOProductDto> GetReworkTPR(string productIn, double thick, int plantID)
        {
            Expression<Func<TPOProduct, bool>> expression = p => p.PlantID == plantID
                                                    && p.Thick == thick
                                                    && (p.ProductCode.Substring(4, 3) == "TPO" || p.ProductCode.Substring(4, 3) == "TPM")
                                                    && (p.ProductCode != productIn || (p.ProductCode == "W56TPO3024" || p.ProductCode == "W590030000"));
            var entities = _repository.Repository<TPOProduct>().GetAllBy(expression).ToList();
            return Mapper.Map<List<TPOProduct>, List<TPOProductDto>>(entities);
        }
        private List<TPOProductDto> GetReworkV(string productIn, double thick, int plantID)
        {
            Expression<Func<TPOProduct, bool>> expression = p => p.PlantID == plantID
                                                    && p.Thick == thick
                                                    && p.ProductCode.Substring(0,4) == "W56V"
                                                    && (p.ProductCode != productIn || (p.ProductCode == "W56TPO3024" || p.ProductCode == "W590030000"));
            var entities = _repository.Repository<TPOProduct>().GetAllBy(expression).ToList();
            return Mapper.Map<List<TPOProduct>, List<TPOProductDto>>(entities);
        }
    }
}
