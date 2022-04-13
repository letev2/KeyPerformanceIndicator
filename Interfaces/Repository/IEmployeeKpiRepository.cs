using KpiNew.Dto;
using KpiNew.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KpiNew.Interfaces.Repository
{
    public interface IEmployeeKpiRepository : IRepository<EmployeeKpi>
    {
        public Task<EmployeeKpi> Get(int id);
        public Task<EmployeeKpi> Get(Expression<Func<EmployeeKpi, bool>> expression);
        public Task<IList<EmployeeKpi>> GetAll();
        public Task<IList<EmployeeDto>> CalculateAllEmployeeRatingForYearly();
    }
}
