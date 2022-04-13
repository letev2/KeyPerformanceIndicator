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
    public class DepartmentService : IDepartmentService
    {

        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public DepartmentService(IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
        }
        public async Task<BaseRespond<DepartmentDto>> AddDepartment(CreateDepartmentRequestModel model)
        {
           var departmentExist = await _departmentRepository.Get(d => d.Name == model.Name);
            if (departmentExist != null)
            {
                return new BaseRespond<DepartmentDto>
                {
                    Message = $" Department with name {model.Name} already exist",
                    Success = false,
                };
                 
            }
            else
            {
                var departments = new Department
                {
                    Name = model.Name,
                    Description = model.Description,

                };

               var department = await _departmentRepository.Create(departments);
                return new BaseRespond<DepartmentDto>
                {
                    Success = true,
                    Message = "Department Create Successfully",
                    Data = new DepartmentDto
                    {
                        Name = department.Name,
                        Description = department.Description,
                        Id = department.Id,
                      
                    }
                    
                };
            }


        }

        public async Task<BaseRespond<DepartmentDto>> DeleteDepartment(int id)
        {
            var department = await _departmentRepository.Get(id);
            if (department == null)
            {
                return new BaseRespond<DepartmentDto>
                {
                    Message = "Department not found",
                    Success = false,
                };
                    
            }
            department.IsDeleted = true;
            _departmentRepository.SaveChanges();

            return new BaseRespond<DepartmentDto>
            {
                Success = true,
                Message = $"Department with {department.Name} was delete Successfully",
            };
            
        }

        public async Task<BaseRespond<ICollection<DepartmentDto>>> GetAllDepartment()
        {
            var department = await _departmentRepository.GetAll();
            var departments = department.Select(a => new DepartmentDto
            {
                Name = a.Name,
                Description = a.Description,
                Id = a.Id,
                Employee = a.Employee.Select(a => new EmployeeDto
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Address = a.Address,
                    City = a.City,
                    Email = a.Email,
                    EmployeeImage = a.EmployeeImage,
                    DateOfBirth = a.DateOfBirth,
                    Gender = a.Gender,
                    PhoneNumber = a.PhoneNumber,
                    DepartmentId = a.DepartmentId,


                }).ToList()

            }).ToList();
            return new BaseRespond<ICollection<DepartmentDto>>
            {
                Success = true,
                Data = departments,
                Message = "Department Retrieved"
            };
            
           
        }

        public async Task<BaseRespond<DepartmentDto>> GetDepartmentById(int id)
        {
            var department = await _departmentRepository.Get(id);
            if (department == null)
            {
                return new BaseRespond<DepartmentDto>
                {
                    Message = "Department not found",
                    Success = false,
                };
            }
            return new BaseRespond<DepartmentDto>
            {
                Success = true,
                Message = "Department Retrieved",
                Data = new DepartmentDto
                {
                    Name = department.Name,
                    Description = department.Description,
                    Employee = department.Employee.Select(a => new EmployeeDto
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        Address = a.Address,
                        City = a.City,
                        Email = a.Email,
                        EmployeeImage = a.EmployeeImage,
                        DateOfBirth = a.DateOfBirth,
                        Department = new DepartmentDto 
                        {
                          Name = a.Department.Name,
                          Description = a.Department.Description
                        },
                        Gender = a.Gender,
                        PhoneNumber = a.PhoneNumber,
                        DepartmentId = a.DepartmentId,


                    }).ToList()

                },
                
            };
        }

        public async Task<BaseRespond<DepartmentDto>> GetDepartmentByName(string name)
        {
            var department = await _departmentRepository.Get(a => a.Name == name);
            if (department == null)
            {
                return new BaseRespond<DepartmentDto>
                {
                    Message = "Department not found",
                    Success = false,
                };

            }
            else
            {
                return new BaseRespond<DepartmentDto>
                {
                    Success = true,
                    Message = "Department Retrieved",
                    Data = new DepartmentDto
                    {
                        Name = department.Name,
                        Description = department.Description,
                        Employee = department.Employee.Select(a => new EmployeeDto
                        {
                            Id = a.Id,
                            FirstName = a.FirstName,
                            LastName = a.LastName,
                            Address = a.Address,
                            City = a.City,
                            Email = a.Email,
                            EmployeeImage = a.EmployeeImage,
                            DateOfBirth = a.DateOfBirth,
                            Department = new DepartmentDto
                            {
                                Name = a.Department.Name,
                                Description = a.Department.Description
                            },
                            Gender = a.Gender,
                            PhoneNumber = a.PhoneNumber,
                            DepartmentId = a.DepartmentId,
                  
                        }).ToList()

                    }
                };
            }
        }

        public async Task<BaseRespond<DepartmentDto>> UpdateDepartment(int id, UpdateDepartmentRequestModel model)
        {
            var department = await _departmentRepository.Get(id);
            if (department == null)
            {
                return new BaseRespond<DepartmentDto>
                {
                    Message = $"Department with {model.Name} does not exixt",
                    Success = false,
                };
            }
            else
            {
                department.Name = model.Name;
                department.Description = model.Description;
                await _departmentRepository.Update(department);

                return new BaseRespond<DepartmentDto>
                {
                    Success = true,
                    Message = $"Department with {department.Name} Successfully Update",
                    Data = new DepartmentDto
                    {
                        Name = department.Name,
                        Description = department.Description,
                       
                    }
                };
            }
            
        }
    }
}
