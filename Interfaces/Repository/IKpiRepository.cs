using KpiNew.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KpiNew.Interfaces.Repository
{
    public interface IKpiRepository : IRepository<Kpi>
    {
        public Task<Kpi> Get(int id);
        public Task<Kpi> Get(Expression<Func<Kpi, bool>> expression);
        public Task<IList<Kpi>> GetSelected(IList<int> ids);
        public Task<IList<Kpi>> GetSelected(Expression<Func<Kpi, bool>> expression);
        public Task<IList<Kpi>> GetAll();
        public Task<IList<Kpi>> GetEmployeeKpiList(int id);
    }
}
