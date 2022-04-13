using KpiNew.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KpiNew.Interfaces.Service
{
    public interface IAdminService
    {
        Task<BaseRespond<AdminDto>> AddAdmin(CreateAdminRequestModel model);
        Task<BaseRespond<AdminDto>> UpdateAdmin(int id, UpdateAdminRequestModel model);
        Task<BaseRespond<AdminDto>> DeleteAdmin(int id);
        Task<BaseRespond<AdminDto>> GetAdminById(int id);
        Task<BaseRespond<AdminDto>> GetAdminByEmail(string email);
        Task<BaseRespond<ICollection<AdminDto>>> GetAllAdmin();
    }
}
