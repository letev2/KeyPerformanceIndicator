using KpiNew.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KpiNew.Interfaces.Service
{
    public interface IKpiService
    {
        Task<BaseRespond<KpiDto>> AddKpi(CreateKpiRequestModel model);
        Task<BaseRespond<KpiDto>> UpdateKpi(int id, UpdateKpiRequestModel model);
        Task<BaseRespond<KpiDto>> DeleteKpi(int id);
        Task<BaseRespond<KpiDto>> GetKpiById(int id);
        Task<BaseRespond<ICollection<KpiDto>>> GetAllKpi();
        Task<BaseRespond<IList<KpiDto>>> GetEmployeeKpiList(int id);



    }
}
