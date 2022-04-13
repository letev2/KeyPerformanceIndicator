using KpiNew.Context;
using KpiNew.Entities;
using KpiNew.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KpiNew.Implementations.Repository
{
    public class KpiRepository : BaseRepository<Kpi>, IKpiRepository
    {
        public KpiRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Kpi> Get(int id)
        {
            return await _context.Kpis
                  .Include(a => a.EmployeeKpis)
                 .Where(a => a.IsDeleted == false)
                  .SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Kpi> Get(Expression<Func<Kpi, bool>> expression)
        {
            return await _context.Kpis
                .Include(a => a.EmployeeKpis)
                 .Where(a => a.IsDeleted == false)
                .SingleOrDefaultAsync(expression);
        }

        public async Task<IList<Kpi>> GetAll()
        {
            return await _context.Kpis
              .Include(a => a.EmployeeKpis)
              .Where(a => a.IsDeleted == false)
              .ToListAsync();

        }

        public async Task<IList<Kpi>> GetEmployeeKpiList(int id)
        {
            return await _context.Kpis.Include(a => a.EmployeeKpis)
                .ThenInclude(e => e.Employee).Where(e => e.Id == id).ToListAsync();
        }

        public async Task<IList<Kpi>> GetSelected(IList<int> ids)
        {
            return await _context.Kpis
                 .Include(a => a.EmployeeKpis)
                 .Where(a => ids.Contains(a.Id))
                 .Where(a => a.IsDeleted == false)
                 .ToListAsync();
        }

        public async Task<IList<Kpi>> GetSelected(Expression<Func<Kpi, bool>> expression)
        {
            return await _context.Kpis
               .Include(a => a.EmployeeKpis)
                 .Where(a => a.IsDeleted == false)
               .Where(expression)
               .ToListAsync();
        }
    }
}
