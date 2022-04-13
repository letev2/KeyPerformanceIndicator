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
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IEmployeeKpiRepository _employeeKpiRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public AdminService(IAdminRepository adminRepository, IUserRepository userRepository, 
            IRoleRepository roleRepository, IEmployeeKpiRepository employeeKpiRepository, IEmployeeRepository employeeRepository)
        {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _employeeKpiRepository = employeeKpiRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<BaseRespond<AdminDto>> AddAdmin(CreateAdminRequestModel model)
        {
            var adminExist = await _adminRepository.Get(a => a.Email == model.Email);
            if (adminExist != null)
            {
                return new BaseRespond<AdminDto>
                {
                    Message = $"Admin with {model.FirstName} already exist",
                    Success = false,
                };
            }
            else
            {
                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                };
                var roles = await _roleRepository.GetByName("Admin");
                var userrole = new UserRole
                {
                    RoleId = roles.Id,
                    Role = roles,
                    UserId = user.Id,
                    User = user,
                };
                user.UserRoles.Add(userrole);
                var admin = new Admin
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    DashBoard = model.DashBoard,
                    User = user,
                    AdminImage = model.adminPhoto,
                    UserId = user.Id,
                
                };
                await _userRepository.Create(user);
                await _adminRepository.Create(admin);
                return new BaseRespond<AdminDto>
                {
                    Success = true,
                    Message = "Admin Create Successfully",
                    Data = new AdminDto
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Address = model.Address,
                        DateOfBirth = model.DateOfBirth,
                        Gender = model.Gender,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        AdminImage = model.adminPhoto,
                        DashBoard = model.DashBoard,
                    }

                };
            }



        }

        public async Task<BaseRespond<AdminDto>> DeleteAdmin(int id)
        {
            var admin = await _adminRepository.Get(id);
            if (admin == null)
            {
                return new BaseRespond<AdminDto>
                {
                    Message = "Admin not found",
                    Success = false,
                };

            }
            admin.IsDeleted = true;
           
            _adminRepository.SaveChanges();

            return new BaseRespond<AdminDto>
            {
                Success = true,
                Message = $"Admin with {admin.FirstName} was delete Successfully",
                
            };
        }

        public async Task<BaseRespond<AdminDto>> GetAdminByEmail(string email)
        {
            var admin = await _adminRepository.GetAdminByEmail(email);
             if (admin == null)
            {
                return new BaseRespond<AdminDto>
                {
                    Message = "admin not found",
                    Success = false,
                };

            }
            return new BaseRespond<AdminDto>
            {
                Success = true,
                Message = "Admin retrived successfully",
                Data = new AdminDto
                {
                    Id = admin.Id,
                    FirstName = admin.FirstName,
                    LastName = admin.LastName,
                    Address = admin.Address,
                    Email = admin.Email,
                    DateOfBirth = admin.DateOfBirth,
                    Gender = admin.Gender,
                    PhoneNumber = admin.PhoneNumber,
                    AdminImage = admin.AdminImage,
                    DashBoard = admin.DashBoard,
                    
                }
            };
        }

        public async Task<BaseRespond<AdminDto>> GetAdminById(int id)
        {
            var admin = await _adminRepository.Get(id);
            if (admin == null)
            {
                return new BaseRespond<AdminDto>
                {
                    Message = "admin not found",
                    Success = false,
                };

            }
            return new BaseRespond<AdminDto>
            {
                Success = true,
                Message = "Admin retrived successfully",
                Data = new AdminDto
                {
                    Id = admin.Id,
                    FirstName = admin.FirstName,
                    LastName = admin.LastName,
                    Address = admin.Address,
                    Email = admin.Email,
                    DateOfBirth = admin.DateOfBirth,
                    Gender = admin.Gender,
                    PhoneNumber = admin.PhoneNumber,
                    AdminImage = admin.AdminImage,
                    DashBoard = admin.DashBoard,
                    
                }
            };
        }

        public async Task<BaseRespond<ICollection<AdminDto>>> GetAllAdmin()
        {
            var admin = await _adminRepository.GetAll();
            var admins = admin.Select(x => new AdminDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Address = x.Address,
                DateOfBirth = x.DateOfBirth,
                AdminImage = x.AdminImage,
                Email = x.Email,
                Gender = x.Gender,
                PhoneNumber = x.PhoneNumber,
                DashBoard = x.DashBoard,
                
            }).ToList();

            return new BaseRespond<ICollection<AdminDto>>
            {
                Success = true,
                Data = admins,
                Message = "Admin Retrieved",
            };
        }

        public async Task<BaseRespond<AdminDto>> UpdateAdmin(int id, UpdateAdminRequestModel model)
        {
            var admin = await _adminRepository.Get(id);
            if (admin == null)
            {
                throw new Exception($"admin with {model.Email} does not exixt");
            }
            else
            {
                admin.FirstName = model.FirstName;
                admin.LastName = model.LastName;
                admin.Address = model.Address;
                admin.DateOfBirth = model.DateOfBirth;
                admin.Email = model.Email;
                admin.PhoneNumber = model.PhoneNumber;

                
                await _adminRepository.Update(admin);

                return new BaseRespond<AdminDto>
                {
                    Success = true,
                    Message = $"{admin.Email} Successfully Update",
                    Data = new AdminDto
                    {
                        FirstName = admin.FirstName,
                        LastName = admin.LastName,
                        Address = admin.Address,
                        DateOfBirth = admin.DateOfBirth,
                        Email = admin.Email,
                        PhoneNumber = admin.PhoneNumber,
                        
                        
                    }
                };

            }
        }
    }
   
}

