using KpiNew.Dto;
using KpiNew.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KpiNew.Interfaces.Service
{
    public interface IEmployeeService
    {
        Task<BaseRespond<EmployeeDto>> AddEmployee(CreateEmployeeRequestModel model);
        Task<BaseRespond<EmployeeDto>> UpdateEmployee( int id, UpdateEmployeeRequestModel model);
        Task<BaseRespond<EmployeeDto>> DeleteEmployee(int id);
        Task<BaseRespond<EmployeeDto>> GetEmployeeById(int id);
        Task<BaseRespond<ICollection<EmployeeDto>>> GetAllEmployee();
        Task<BaseRespond<EmployeeKpiDto>> Performance(RatingPerformance model);
        Task<BaseRespond<EmployeeDto>> GetEmployeeByEmail(string email);
        Task<BaseRespond<EmployeeDto>> GetEmployeeByDepartment(int departmentId);
        Task<BaseRespond<EmployeeDto>> CalculateEmployeeKpiForMonth(EmployeeRatingPerformance model, int id);
        Task<BaseRespond<EmployeeDto>> CalculateEmployeeKpiForYear(EmployeeRatingPerformance model, int id);
        Task<BaseRespond<IList<EmployeeDto>>> GetAllEmployeeRatingForMonth();
        Task<BaseRespond<IList<EmployeeDto>>> CalculateAllEmployeeRatingYearly();
       

    }
}
