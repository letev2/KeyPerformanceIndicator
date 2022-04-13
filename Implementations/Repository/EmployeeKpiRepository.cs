using KpiNew.Context;
using KpiNew.Dto;
using KpiNew.Entities;
using KpiNew.Implementations.Repository;
using KpiNew.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KpiNew.Implementations
{
    public class EmployeeKpiRepository : BaseRepository<EmployeeKpi>, IEmployeeKpiRepository
    {
        public EmployeeKpiRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<EmployeeKpi> Get(int id)
        {
            return await _context.EmployeeKpis.FindAsync(id);
        }

        public async Task<EmployeeKpi> Get(Expression<Func<EmployeeKpi, bool>> expression)
        {
            return await _context.EmployeeKpis.SingleOrDefaultAsync(expression);
        }

        public async Task<IList<EmployeeKpi>> GetAll()
        {
            return await _context.EmployeeKpis.ToListAsync();
        }

        public async Task<IList<EmployeeDto>> CalculateAllEmployeeRatingForYearly()
        {
            IList<EmployeeDto> employeeDtos = new List<EmployeeDto>(){ };
           
            var year = DateTime.UtcNow.Year;
            var employeeIds = await _context.Employees.Select(e => e.Id).ToListAsync();
            var employeeKpis = await _context.EmployeeKpis.Include(e => e.Employee)
              .ToListAsync();
            
            foreach(var employeeId in employeeIds)
            {
                var employeeName = employeeKpis.Where(e => e.EmployeeId == employeeId).Select(e => String.Join(" ", e.Employee.FirstName, e.Employee.LastName)).FirstOrDefault();
                var sumTotal = employeeKpis.Where(e => e.EmployeeId == employeeId).Sum(e => e.KpiRating);
                var employeeDto = new EmployeeDto
                {
                    FullName = employeeName,
                    SumTotal = sumTotal,
                };
                employeeDtos.Add(employeeDto);
            }
        
            return employeeDtos;
        
        }


    }
}
