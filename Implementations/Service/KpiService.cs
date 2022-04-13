using KpiNew.Dto;
using KpiNew.Entities;
using KpiNew.Interfaces.Repository;
using KpiNew.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KpiNew.Implementations.Service
{
    public class KpiService : IKpiService
    {
        protected readonly IKpiRepository _kpiRepository;
        public KpiService(IKpiRepository kpiRepository)
        {
            _kpiRepository = kpiRepository;
        }

        public async Task<BaseRespond<KpiDto>> AddKpi(CreateKpiRequestModel model)
        {
            var kpiExist = await _kpiRepository.Get(d => d.Name == model.Name);
            if (kpiExist != null || model.Rating > 10)
            {
                return new BaseRespond<KpiDto>
                {
                    Message = $" Kpi with name {model.Name} already exist",
                    Success = false,
                };
            }
            else
            {
                var kpis = new Kpi
                {
                    Name = model.Name,
                    Rating = model.Rating,
                    
                };
                
                
               var kpi = await _kpiRepository.Create(kpis);
                return new BaseRespond<KpiDto>
                {
                    Success = true,
                    Message = "Kpi Create Successfully",
                    Data = new KpiDto
                    {
                        Id = kpi.Id,
                        Name = kpi.Name,
                        Rating = kpi.Rating,

                    }
                };

            }


        }
        public async Task<BaseRespond<KpiDto>> DeleteKpi(int id)
        {    
            
            var kpi = await _kpiRepository.Get(id);
            if (kpi == null)

            {
                return new BaseRespond<KpiDto>
                {
                    Message = "Kpi not found",
                    Success = false,
                };
            }
            kpi.IsDeleted = true;
            _kpiRepository.SaveChanges();
            return new BaseRespond<KpiDto>
            {
                Success = true,
                Message = $"Kpi with {kpi.Name} was delete Successfully",
              
            };
        }

        public async Task<BaseRespond<ICollection<KpiDto>>> GetAllKpi()
        {
            var kpi = await _kpiRepository.GetAll();
            var kpis = kpi.Select(a => new KpiDto
            {
                Id = a.Id,
                Name = a.Name,
                Rating = a.Rating,
               
            }).ToList();
            
            return new BaseRespond<ICollection<KpiDto>>
            {
                Success = true,
                Data = kpis,
                Message = "kpi Retrieved"
            };

        }

        public async Task<BaseRespond<IList<KpiDto>>> GetEmployeeKpiList(int id)
        {
            var kpi = await _kpiRepository.GetEmployeeKpiList(id);

            var kpis = kpi.Select(a => new KpiDto
            {
                Id = a.Id,
                Name = a.Name,
                Rating = a.Rating,

            }).ToList();

            return new BaseRespond<IList<KpiDto>>
            {
                Success = true,
                Data = kpis,
                Message = "Kpi Retrieved"
            };

        }

        public async Task<BaseRespond<KpiDto>> GetKpiById(int id)
        {
            var kpi = await _kpiRepository.Get(id);
            if (kpi == null)
            {
                return new BaseRespond<KpiDto>
                {
                    Message = "$Kpi does not exixt",
                    Success = false,
                };
              
            }

            return new BaseRespond<KpiDto>
            {
                Success = true,
                Data = new KpiDto
                {
                    Id = kpi.Id,
                    Name = kpi.Name,
                    Rating = kpi.Rating,


                },
                Message = "Kpi Retrieved"
            };
        }

        public async Task<BaseRespond<KpiDto>> UpdateKpi(int id, UpdateKpiRequestModel model)
        {
            var kpi = await _kpiRepository.Get(id);
            if (kpi == null)
            {
                return new BaseRespond<KpiDto>
                {
                    Message = $"Kpi with {model.Name} does not exixt",
                    Success = false,
                };
            }
            else
            {
                kpi.Name = model.Name;
                kpi.Rating = model.Rating;
                await _kpiRepository.Update(kpi);

                return new BaseRespond<KpiDto>
                {
                    Success = true,
                    Message = $"{kpi.Name} Successfully Updated",
                    Data = new KpiDto
                    {
                        Name = kpi.Name,
                        Rating = kpi.Rating,
                       
                    },
                };
            }
        }
    }
}
