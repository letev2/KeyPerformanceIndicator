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
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Admin> Get(int id)
        {
            return await _context.Admins
                .Include(a => a.User)
                .Where(b => b.IsDeleted == false)
                .SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Admin> Get(Expression<Func<Admin, bool>> expression)
        {
            return await _context.Admins
                .Include(a => a.User)
                .Where(b => b.IsDeleted == false)
                .SingleOrDefaultAsync(expression);
        }

        public async Task<Admin> GetAdminByEmail(string email)
        { return await _context.Admins.Include(a => a.User)
           .Where(a => a.Email == email).SingleOrDefaultAsync(); 
        }

        public async Task<IList<Admin>> GetAll()
        {
            return await _context.Admins
                .Include(a => a.User)
                .Where(a => a.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<IList<Admin>> GetSelected(IList<int> ids)
        {
            return await _context.Admins
                .Include(a => a.User)
                .Where(b => b.IsDeleted == false)
                .Where(a => ids.Contains(a.Id))
                .ToListAsync();
        }

        public async Task<IList<Admin>> GetSelected(Expression<Func<Admin, bool>> expression)
        {
            return await _context.Admins
                .Include(a => a.User)
                .Where(b => b.IsDeleted == false)
                .Where(expression)
                .ToListAsync();
        }
    }
}
