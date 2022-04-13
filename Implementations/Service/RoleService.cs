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
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<BaseRespond<RoleDto>> AddRole(CreateRoleRequestModel model)
        {
            var roleExist = await _roleRepository.Get(a => a.Name == model.Name);
            if (roleExist != null)
            {
                return new BaseRespond<RoleDto>
                {
                    Message = "Admin not found",
                    Success = false,
                };
            }

            else
            {
                var role = new Role
                {
                    Name = model.Name,
                    Description = model.Description,
                   
                };

                await _roleRepository.Create(role);
                return new BaseRespond<RoleDto>
                {
                    Success = true,
                    Message = "Role Create Successfully",
                    Data = new RoleDto
                    {
                        Name = role.Name,
                        Description = role.Description
                    }
                };
            }



        }

        public async Task<BaseRespond<RoleDto>> DeleteRole(int id)
        {
            var role = await _roleRepository.Get(id);
            if (role == null)
            {
                return new BaseRespond<RoleDto>
                {
                    Message = "Role not found",
                    Success = true,

                };
            }
            role.IsDeleted = true;
            _roleRepository.SaveChanges();

            return new BaseRespond<RoleDto>
            {
                Success = true,
                Message = $"Role with {role.Name} was delete Successfully",
              
            };
        }

        public async Task<BaseRespond<ICollection<RoleDto>>> GetAllRole()
        {
            var role = await _roleRepository.GetAll();
            var roles = role.Select(a => new RoleDto
            {
                Name = a.Name,
                Description = a.Description
            }).ToList();
            return new BaseRespond<ICollection<RoleDto>>
            {
                Success = true,
                Data = roles,
                Message = "Role Retrieved"
            };
        }

        public async Task<BaseRespond<RoleDto>> GetRoleById(int id)
        {
            var role = await _roleRepository.Get(id);
            if (role == null)
                {return new BaseRespond<RoleDto>
                {
                    Message = $"role does not exixt",
                    Success = false,
                };
            }
            return new BaseRespond<RoleDto>
            {
                Success = true,
                Data = new RoleDto
                {
                    Name = role.Name,
                    Description = role.Description
                },
                Message = "Role Retrieved"
            };
        }

        public async Task<BaseRespond<RoleDto>> UpdateRole(int id, UpdateRoleRequestModel model)
        {
            var role = await _roleRepository.Get(id);
            if (role == null)
            {
                return new BaseRespond<RoleDto>
                {
                    Message = $"Role with {model.Name} does not exixt",
                    Success = false,
                };
            }
            else
            {
                role.Name = model.Name;
                role.Description = model.Description;
                await _roleRepository.Update(role);

                return new BaseRespond<RoleDto>
                {
                    Success = true,
                    Message = $"{role.Name} Successfully Update",
                    Data = new RoleDto
                    {
                        Name = role.Name,
                        Description = role.Description,
                    }
                };
            }

        }
    }
}
