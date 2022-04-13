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
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Department> Get(int id)
        {
            return await _context.Departments
                 .Include(a => a.Employee)
                  .Where(b => b.IsDeleted == false)
                 .SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Department> Get(Expression<Func<Department, bool>> expression)
        {
            return await _context.Departments
               .Include(a => a.Employee)
                .Where(b => b.IsDeleted == false)
               .SingleOrDefaultAsync(expression);
        }

        public async Task<IList<Department>> GetAll()
        {
            return await _context.Departments
                .Include(a => a.Employee)
                .Where(a =>a.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<IList<Department>> GetSelected(IList<int> ids)
        {
            return await _context.Departments
               .Include(a => a.Employee)
                .Where(b => b.IsDeleted == false)
               .Where(a => ids.Contains(a.Id))
               .ToListAsync();
        }

        public async Task<IList<Department>> GetSelected(Expression<Func<Department, bool>> expression)
        {
            return await _context.Departments
               .Include(a => a.Employee)
                .Where(b => b.IsDeleted == false)
               .Where(expression)
               .ToListAsync();
        }
    }
}
