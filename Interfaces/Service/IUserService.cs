using KpiNew.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KpiNew.Interfaces.Service
{
    public interface IUserService
    { Task<BaseRespond<UserDto>> GetUserById(int id);
        Task<BaseRespond<ICollection<UserDto>>> GetAllUser();
        Task<BaseRespond<UserDto>> GetUserByEmail(string email);
        Task<BaseRespond<UserDto>> Login(LoginUserDto model);


    }
}
