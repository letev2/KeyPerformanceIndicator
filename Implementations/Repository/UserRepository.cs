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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<User> Get(int id)
        {
           return await _context.Users.Include(e =>e.Employee)
                .Include(a =>a.Admin)
                .Include(a => a.UserRoles)
                .ThenInclude(u => u.Role)
                 .Where(a => a.IsDeleted == false)
                .SingleOrDefaultAsync(i => i.Id == id);
        }

        public async Task<User> Get(Expression<Func<User, bool>> expression)
        {
            return await _context.Users
                 .Include(a => a.UserRoles)
                 .ThenInclude(u => u.Role)
                 .Where(a => a.IsDeleted == false)
                 .SingleOrDefaultAsync(expression);

                
        }

        public async Task<IList<User>> GetAll()
        {
            return await _context.Users
                .Include(a => a.UserRoles)
                .ThenInclude(u => u.Role)
                .Where(a => a.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.Include(a => a.UserRoles)
                .ThenInclude(u => u.Role).Where(a => a.IsDeleted == false)
                .Where(a => a.Email == email).FirstOrDefaultAsync();
        }

        public async Task<IList<User>> GetSelected(IList<int> ids)
        {
            return await _context.Users
              .Include(a => a.UserRoles)
              .ThenInclude(u => u.Role)
                 .Where(a => a.IsDeleted == false)
              .Where(a => ids.Contains(a.Id))
              .ToListAsync();
        }

        public async Task<IList<User>> GetSelected(Expression<Func<User, bool>> expression)
        {
            return await _context.Users
              .Include(a => a.UserRoles)
              .ThenInclude(u => u.Role)
                 .Where(a => a.IsDeleted == false)
              .Where(expression)
              .ToListAsync();
        }
    }
}
