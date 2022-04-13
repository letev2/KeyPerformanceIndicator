using KpiNew.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KpiNew.Interfaces.Service
{
    public interface IRoleService
    {
        Task<BaseRespond<RoleDto>> AddRole(CreateRoleRequestModel model);
        Task<BaseRespond<RoleDto>> UpdateRole(int id, UpdateRoleRequestModel model);
        Task<BaseRespond<RoleDto>> DeleteRole(int id);
        Task<BaseRespond<RoleDto>> GetRoleById(int id);
        Task<BaseRespond<ICollection<RoleDto>>> GetAllRole();
    }
}
