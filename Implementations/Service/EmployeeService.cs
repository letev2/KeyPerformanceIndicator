using KpiNew.Dto;
using KpiNew.Entities;
using KpiNew.Enum;
using KpiNew.Interfaces.Repository;
using KpiNew.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KpiNew.Implementations.Service
{
    public class EmployeeService : IEmployeeService
    {
        protected readonly IEmployeeRepository _employeeRepository;
        protected readonly IUserRepository _userRepository;
        protected readonly IRoleRepository _roleRepository;
        protected readonly IKpiRepository _kpiRepository;
        protected readonly IDepartmentRepository _departmentRepository;
        protected readonly IEmployeeKpiRepository _employeeKpiRepository;
        public EmployeeService(IEmployeeRepository employeeRepository, IUserRepository userRepository, IKpiRepository kpiRepository,
            IDepartmentRepository departmentRepository, IEmployeeKpiRepository employeeKpiRepository, IRoleRepository roleRepository)
        {
            _employeeRepository = employeeRepository;
           _userRepository = userRepository;
            _roleRepository = roleRepository;
            _kpiRepository = kpiRepository;
            _departmentRepository = departmentRepository;
            _employeeKpiRepository = employeeKpiRepository;
        }
        public async Task<BaseRespond<EmployeeDto>> AddEmployee(CreateEmployeeRequestModel model)
        {
            var employeeExist = await _employeeRepository.Get(d => d.Email == model.Email);
            if (employeeExist != null)
            {
                return new BaseRespond<EmployeeDto>
                {
                    Message = $" Employee with Email already exist",
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
                var roles = await _roleRepository.GetByName("Employee");
                var userRole = new UserRole
                {
                    RoleId = roles.Id,
                    Role = roles,
                    UserId = user.Id,
                    User = user,
                };
                user.UserRoles.Add(userRole);

                var newUser = await _userRepository.Create(user);
                
                var employee = new Employee
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    DateOfBirth = model.DateOfBirth,
                    City = model.City,
                    DepartmentId = model.DepartmentId,
                    Department = await _departmentRepository.Get(model.DepartmentId),
                    Gender = model.Gender,
                    Email = model.Email,
                    UserId = user.Id,
                    User = user,
                    PhoneNumber = model.PhoneNumber,
                    EmployeeType = model.EmployeeType,       
                    

                };
                var kpi = await _kpiRepository.GetSelected(model.ListOfKpis);
                foreach (var item in kpi)
                {
                    var employeekpi = new EmployeeKpi
                    {
                        Employee = employee,
                        EmployeeId = employee.Id,
                        Kpi = item,
                        KpiId = item.Id,
                        
                    };
                    employee.EmployeeKpis.Add(employeekpi);
                }
              var employees = await _employeeRepository.Create(employee);


                return new BaseRespond<EmployeeDto>
                {
                    Success = true,
                    Message = "Employee Create Successfully",
                    Data = new EmployeeDto
                    {
                        FirstName = employees.FirstName,
                        LastName = employees.LastName,
                        Address = employees.Address,
                        DateOfBirth = employees.DateOfBirth,
                        City = employees.City,
                        DepartmentId = employees.DepartmentId,
                        EmployeeImage = employees.EmployeeImage,
                        Gender = employees.Gender,
                        Email = employees.Email,
                        PhoneNumber = employees.PhoneNumber,
                        EmployeeType = employees.EmployeeType,
                        

                    }

                };
            }


        }

        public async Task<BaseRespond<IList<EmployeeDto>>> CalculateAllEmployeeRatingYearly()
        {
            var employee = await _employeeKpiRepository.CalculateAllEmployeeRatingForYearly();
            return new BaseRespond<IList<EmployeeDto>>
            {
                Success = true,
                Message = "Success",
                Data = employee
            };
        }

        public async Task<BaseRespond<EmployeeDto>> CalculateEmployeeKpiForMonth(EmployeeRatingPerformance model, int id)
        {
            var getUser = await _userRepository.Get(id);
            var employee = await _employeeRepository.CalculateIndividualEmployeePerformanceForEachMonth(getUser.Employee.Id,model.Month,model.Year);
            return new BaseRespond<EmployeeDto>
            {
                Success = true,
                Message = "Success",
                Data = employee
            };

           
        }

        public async Task<BaseRespond<EmployeeDto>> CalculateEmployeeKpiForYear(EmployeeRatingPerformance model, int id)
        {
            var getUser = await _userRepository.Get(id);
            var employee = await _employeeRepository.CalculateIndividualEmployeePerformanceForEachYear(getUser.Employee.Id,model.Year);
            return new BaseRespond<EmployeeDto>
            {
                Success = true,
                Message = "Success",
                Data = employee
            };
        }

        public async Task<BaseRespond<EmployeeDto>> DeleteEmployee(int id)
        {
            var employee = await _employeeRepository.Get(id);
            if (employee == null)
            {
                return new BaseRespond<EmployeeDto>
                {
                    Message = "Employee not found",
                    Success = false,
                };

            }
            employee.IsDeleted = true;
            _employeeRepository.SaveChanges();

            return new BaseRespond<EmployeeDto>
            {
                Success = true,
                Message = $"Employee with {employee.Email} was delete Successfully",
            };
              
        }

        public async Task<BaseRespond<ICollection<EmployeeDto>>> GetAllEmployee()
        {
            var employee = await _employeeRepository.GetAll();
            var employees = employee.Select(a => new EmployeeDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Address = a.Address,
                DateOfBirth = a.DateOfBirth,
                City = a.City,
                EmployeeImage = a.EmployeeImage,
                Gender = a.Gender,
                Email = a.Email,
                SumTotal = a.SumTotal,
                PhoneNumber = a.PhoneNumber,
                EmployeeType = a.EmployeeType,

                Department = new DepartmentDto
                {
                    Id = a.Department.Id,
                    Name = a.Department.Name,
                    Description = a.Department.Description,
                },
                Kpis = a.EmployeeKpis.Select(b => new KpiDto
                {
                    Name = b.Kpi.Name,
                    Rating = b.Kpi.Rating,
                   
                    
                }).ToList(),
                
               
                
            }).ToList();
          
           
            return new BaseRespond<ICollection<EmployeeDto>>
            {
                Success = true,
                Data = employees,
                Message = "Employee Retrieved"
            };

        }

        public async Task<BaseRespond<IList<EmployeeDto>>> GetAllEmployeeRatingForMonth()
        {
            var employee = await _employeeRepository.CalculateAllEmployeeRatingForMonthly();
            return new BaseRespond<IList<EmployeeDto>>
            {
                Success = true,
                Message = "Calculation solved",
                Data = employee
            };
        }

       
        public async Task<BaseRespond<EmployeeDto>> GetEmployeeByDepartment(int departmentId)
        {
            var employee = await _employeeRepository.Get(a => a.DepartmentId == departmentId);
            if (employee == null)
            {
                return new BaseRespond<EmployeeDto>
                {
                    Message = $"Employee with {employee.Id} does not exist ",
                    Success = false,
                };
            }

            else
            {
                return new BaseRespond<EmployeeDto>
                {
                    Success = true,
                    Data = new EmployeeDto
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Address = employee.Address,
                        DateOfBirth = employee.DateOfBirth,
                        City = employee.City,
                        DepartmentId = employee.DepartmentId,
                        EmployeeImage = employee.EmployeeImage,
                        Gender = employee.Gender,
                        Email = employee.Email,
                        SumTotal = employee.SumTotal,
                        PhoneNumber = employee.PhoneNumber,
                        EmployeeType = employee.EmployeeType,
                            
                        Department = new DepartmentDto
                        {
                            Id = employee.Department.Id,
                            Name = employee.Department.Name,
                            Description = employee.Department.Description,

                        },
                        Kpis = employee.EmployeeKpis.Select(b => new KpiDto
                        {
                            Name = b.Kpi.Name,
                            Rating = b.Kpi.Rating,


                        }).ToList()


                    },
                    Message = "Employee Retrieved"

                };
            }
        }

        public async Task<BaseRespond<EmployeeDto>> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.Get(id);
            if (employee == null)
            {
                return new BaseRespond<EmployeeDto>
                {
                    Message = $"Employee with {id} does not exist ",
                    Success = false,
                };
            }
            return new BaseRespond<EmployeeDto>
            {
                Success = true,
                Data = new EmployeeDto
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Address = employee.Address,
                    DateOfBirth = employee.DateOfBirth,
                    City = employee.City,
                    DepartmentId = employee.DepartmentId,
                    EmployeeImage = employee.EmployeeImage,
                    Gender = employee.Gender,
                    Email = employee.Email,
                    SumTotal = employee.SumTotal,
                    PhoneNumber = employee.PhoneNumber,
                    EmployeeType = employee.EmployeeType,

                    Department = new DepartmentDto
                     {
                         Id = employee.Department.Id,
                         Name = employee.Department.Name,
                         Description = employee.Department.Description,

                     },
                    Kpis = employee.EmployeeKpis.Select(b => new KpiDto
                    {
                        Name = b.Kpi.Name,
                        Rating = b.Kpi.Rating,
                        

                    }).ToList()


                },
              

                Message = "Employee Retrieved"
            };
        }

        public async Task<BaseRespond<EmployeeDto>> GetEmployeeByEmail(string email)
        {
            var employee = await _employeeRepository.Get(a => a.Email == email);

            if (employee == null)
            {
                return new BaseRespond<EmployeeDto>
                {
                    Success = false,
                    Message = $"department doesnot exist"
                };

            }

            else
            {
                return new BaseRespond<EmployeeDto>
                {
                    Success = true,
                    Data = new EmployeeDto
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Address = employee.Address,
                        DateOfBirth = employee.DateOfBirth,
                        City = employee.City,
                        DepartmentId = employee.DepartmentId,
                        EmployeeImage = employee.EmployeeImage,
                        Gender = employee.Gender,
                        Email = employee.Email,
                        SumTotal = employee.SumTotal,
                        PhoneNumber = employee.PhoneNumber,
                        EmployeeType = employee.EmployeeType,

                        Department = new DepartmentDto
                        {
                            Id = employee.Department.Id,
                            Name = employee.Department.Name,
                            Description = employee.Department.Description,

                        },
                        Kpis = employee.EmployeeKpis.Select(b => new KpiDto
                        {
                            Name = b.Kpi.Name,
                            Rating = b.Kpi.Rating,


                        }).ToList()


                    },


                    Message = "Employee Retrieved"
                };
            }
        }

        public async Task<BaseRespond<EmployeeKpiDto>> Performance(RatingPerformance model)
        {

            var employee = await _employeeRepository.Get(model.EmployeeId);
            var kpi = await _kpiRepository.Get(model.KpiId);

            if (employee == null || kpi == null)
            {
                return new BaseRespond<EmployeeKpiDto>
                {
                    Success = false,
                    Message = "Employee and Kpi not found",
                };
            }

            
            var employeekpi = new EmployeeKpi
            {
                
                Employee = employee,
                EmployeeId = employee.Id,
                Kpi = kpi,
                KpiId = kpi.Id,
                KpiRating = kpi.Rating * 10,
                Month = model.Month,
                Year = model.Year,
            };

            employee.EmployeeKpis.Add(employeekpi);
            var employees = await _employeeKpiRepository.Create(employeekpi);
            return new BaseRespond<EmployeeKpiDto>
            {
                Success = true,
                Data = new EmployeeKpiDto
                {
                    Id = employee.Id,
                    Employee = employee,
                    EmployeeId = employee.Id,
                    Kpi = kpi,
                    KpiId = kpi.Id,
                    KpiRating = employees.KpiRating,
                    Month = model.Month,
                    Year = model.Year,
                },
                Message = "Retrived Successfully",
            }; 
        }

        public async Task<BaseRespond<EmployeeDto>> UpdateEmployee(int id, UpdateEmployeeRequestModel model)
        {
            var employee = await _employeeRepository.Get(id);
            
            if (employee == null)
            {
                return new BaseRespond<EmployeeDto>
                {
                    Message = $"Employee with {model.Email} does not exixt",
                    Success = false,
                };
            }
            else
            {
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.Address = model.Address;
                employee.DateOfBirth = model.DateOfBirth;
                employee.City = model.City;
                employee.EmployeeImage = model.EmployeeImage;
                employee.Email = model.Email;
                employee.PhoneNumber = model.PhoneNumber;
                employee.EmployeeType = model.EmployeeType;


                await _employeeRepository.Update(employee);

                return new BaseRespond<EmployeeDto>
                {
                    Success = true,
                    Message = $"{employee.Email} Successfully Updated",
                    Data = new EmployeeDto
                    {
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Address = employee.Address,
                        DateOfBirth = employee.DateOfBirth,
                        City = employee.City,
                        EmployeeImage = employee.EmployeeImage,
                        Email = employee.Email,
                        PhoneNumber = employee.PhoneNumber,
                        EmployeeType = employee.EmployeeType,

                    }
                };

            }
        }

      
    }
}
