using KpiNew.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KpiNew.Interfaces.Service
{
    public interface IDepartmentService
    {
        Task<BaseRespond<DepartmentDto>> AddDepartment(CreateDepartmentRequestModel model);
        Task<BaseRespond<DepartmentDto>> UpdateDepartment(int id, UpdateDepartmentRequestModel model);
        Task<BaseRespond<DepartmentDto>> DeleteDepartment(int id);
        Task<BaseRespond<DepartmentDto>> GetDepartmentById(int id);
        Task<BaseRespond<ICollection<DepartmentDto>>> GetAllDepartment();
        Task<BaseRespond<DepartmentDto>> GetDepartmentByName(string name);
    }
}
