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
    public class UserService : IUserService
    {
        protected readonly IUserRepository _userRepository;


        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
           
        }

      
     
        public async Task<BaseRespond<ICollection<UserDto>>> GetAllUser()
        {
            var user = await _userRepository.GetAll();
            var users = user.ToList().Select(user => new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
               Password = user.Password,
                Roles = user.UserRoles.Select(b => new RoleDto
                {
                    Id = b.Role.Id,
                    Name = b.Role.Name
                }).ToList()


            }).ToList();
            return new BaseRespond<ICollection<UserDto>>
            {
                Success = true,
                Data = users,
                Message = "User Retrieved"
            };

        }

        public async Task<BaseRespond<UserDto>> GetUserByEmail(string email)
        {
            var user = await _userRepository.Get(d => d.Email == email);
            if (user == null)
            {
                return new BaseRespond<UserDto>
                {
                    Message = $" User with {email} does not exist",
                    Success = false,
                };
            }

            return new BaseRespond<UserDto>
            {
                Success = true,
                Message = $"User with {user.Email} was retrieved Successfully",
                Data = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Roles = user.UserRoles.Select(b => new RoleDto
                    {
                        Id = b.Role.Id,
                        Name = b.Role.Name
                    }).ToList()
                }
            };

        }

        public async Task<BaseRespond<UserDto>> GetUserById(int id)
        {
            var user = await _userRepository.Get(id);
            if (user == null)
            {
                return new BaseRespond<UserDto>
                {
                    Message = "User does not exist",
                    Success = false,
                };
                
            }
            return new BaseRespond<UserDto>
            {
                Success = true,
                Data = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Roles = user.UserRoles.Select(b => new RoleDto
                    {
                        Id = b.Role.Id,
                        Name = b.Role.Name
                    }).ToList()

                },
                Message = "User Retrieved"
            };

        }

      

        public async Task<BaseRespond<UserDto>> Login(LoginUserDto model)
        {
            var user = await _userRepository.GetByEmail(model.Email);

            if (user == null || user.Password !=model.Password )
             return new BaseRespond<UserDto>
            {
                Message = "Invalid Email Or Password",
                Success = false,
            };

            return new BaseRespond<UserDto>
            {
                Success = true,
                Message = "Login Successful",
                Data = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,
                    Roles = user.UserRoles.Select(b => new RoleDto
                    {
                        Id = b.Role.Id,
                        Name = b.Role.Name
                    }).ToList()

                }
            };
        }

       
        
    }
}
