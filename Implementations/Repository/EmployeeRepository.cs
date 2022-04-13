using KpiNew.Context;
using KpiNew.Dto;
using KpiNew.Entities;
using KpiNew.Enum;
using KpiNew.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KpiNew.Implementations.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationContext context)
        {
            _context = context;       
        }

        public async Task<IList<EmployeeDto>> CalculateAllEmployeeRatingForMonthly()
        {
            return await _context.Employees.Include(a => a.EmployeeKpis).
            Select(a => new EmployeeDto
            {
                FullName = $"{a.FirstName} {a.LastName}",
                SumTotal = a.EmployeeKpis.Sum(a => a.KpiRating)
            }).ToListAsync();
        }

        public async Task<IList<EmployeeDto>> CalculateAllEmployeeRatingForYearly()
        {
            var year = DateTime.UtcNow.Year;
            return await _context.Employees.Include(a => a.EmployeeKpis).
          Select(a => new EmployeeDto
          {
              FullName = $"{a.FirstName} {a.LastName}",
              SumTotal = a.EmployeeKpis.Where(a => a.Year==year).Sum(a => a.KpiRating)
          }).ToListAsync();
        }

        public async Task<EmployeeDto> CalculateIndividualEmployeePerformanceForEachMonth(int id, Month month, int year)
        {
          
            var x = await _context.Employees.Include(a => a.EmployeeKpis).ThenInclude(a =>a.Kpi)
            .Where(r => r.Id == id).Select(e => new EmployeeDto
            {
                FullName = $"{e.FirstName}{e.LastName}",
                SumTotal = e.EmployeeKpis.Where(k => k.Year == year && k.Month == month).Sum(a => a.KpiRating),
              
            }).FirstOrDefaultAsync();
            return x;
        }

        public async Task<EmployeeDto> CalculateIndividualEmployeePerformanceForEachYear(int id, int year)
        {
           
            return await _context.Employees.Include(a => a.EmployeeKpis).ThenInclude(a=>a.Kpi).Where(i =>i.Id==id).Select(e => new EmployeeDto
            {

                FullName = $"{e.FirstName}{e.LastName}",
                SumTotal = e.EmployeeKpis.Where(k => k.Year == year ).Sum(a => a.KpiRating)
            }).FirstOrDefaultAsync();
             
              
        }

        public async Task<Employee> Get(int id)
        {
            return await _context.Employees
                  .Include(a => a.Department)
               .Include(k => k.EmployeeKpis).ThenInclude(e => e.Kpi)
                   .Where(b => b.IsDeleted == false)
                  .SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Employee> Get(Expression<Func<Employee, bool>> expression)
        {
            return await _context.Employees
              .Include(a => a.Department)
               .Include(k => k.EmployeeKpis).ThenInclude(e => e.Kpi)
               .Where(b => b.IsDeleted == false)
              .SingleOrDefaultAsync(expression);
        }

        public async Task<IList<Employee>> GetAll()
        {
            return await _context.Employees
               .Include(a => a.Department)
               .Include(k => k.EmployeeKpis).ThenInclude(e => e.Kpi)
               .Where(a => a.IsDeleted == false)
               .ToListAsync();
               
               
        }

        public async Task<IList<Employee>> GetSelected(IList<int> ids)
        {
            return await _context.Employees
                 .Include(a => a.Department)
                .Include(k => k.EmployeeKpis).ThenInclude(e => e.Kpi)
                  .Where(b => b.IsDeleted == false)
                 .Where(a => ids.Contains(a.Id))
                 .ToListAsync();
        }

        public async Task<IList<Employee>> GetSelected(Expression<Func<Employee, bool>> expression)
        {
            return await _context.Employees
              .Include(a => a.Department)
               .Include(k => k.EmployeeKpis).ThenInclude(e => e.Kpi)
                .Where(b => b.IsDeleted == false)
              .Where(expression)
              .ToListAsync();
        }
    }

}
