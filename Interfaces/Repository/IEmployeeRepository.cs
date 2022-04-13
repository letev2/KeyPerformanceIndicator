using KpiNew.Dto;
using KpiNew.Entities;
using KpiNew.Enum;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KpiNew.Interfaces.Repository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        public Task<Employee> Get(int id);
        public Task<Employee> Get(Expression<Func<Employee, bool>> expression);
        public Task<IList<Employee>> GetSelected(IList<int> ids);
        public Task<IList<Employee>> GetSelected(Expression<Func<Employee, bool>> expression);
        public Task<IList<Employee>> GetAll();
        public Task<IList<EmployeeDto>> CalculateAllEmployeeRatingForMonthly();
        public Task<IList<EmployeeDto>> CalculateAllEmployeeRatingForYearly();
        public Task<EmployeeDto> CalculateIndividualEmployeePerformanceForEachMonth(int id, Month month, int year);
        public Task<EmployeeDto> CalculateIndividualEmployeePerformanceForEachYear(int id, int year);

    }
}
