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
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public bool ExistById(int id)
        {
           return  _context.Roles
                 .Where(a => a.IsDeleted == false)
                 .Any(a => a.Id == id); 
        }

        public  bool ExistByName(string name)
        {
            return _context.Roles
                 .Where(a => a.IsDeleted == false)
                 .Any(a => a.Name == name);
        }

        public async Task<Role> Get(int id)
        {
            return await _context.Roles
              .Include(a => a.UserRoles)
              .ThenInclude(u => u.User)
                 .Where(a => a.IsDeleted == false)
              .SingleOrDefaultAsync(a=> a.Id == id);

        }

        public async Task<Role> Get(Expression<Func<Role, bool>> expression)
        {
            return await _context.Roles
               .Include(a => a.UserRoles)
               .ThenInclude(u => u.User)
                 .Where(a => a.IsDeleted == false)
               .SingleOrDefaultAsync(expression);
        }

        public async Task<IList<Role>> GetAll()
        {
            return await _context.Roles
               .Include(a => a.UserRoles)
               .ThenInclude(u => u.User)
              .Where(a => a.IsDeleted == false)
               .ToListAsync();
        }

        public async Task<Role> GetById(int id)
        {
            return await _context.Roles
                 .Where(a => a.IsDeleted == false && id == a.Id)
                .SingleOrDefaultAsync();
        }

        public async Task<Role> GetByName(string name)
        {
            return await _context.Roles
                 .Where(a => a.IsDeleted == false)
                 .SingleOrDefaultAsync(a => a.Name == name);
        }

        public async Task<IList<Role>> GetSelected(IList<int> ids)
        {
            return await _context.Roles
               .Include(a => a.UserRoles).
               ThenInclude(u => u.User)
                 .Where(a => a.IsDeleted == false)
               .Where(a => ids.Contains(a.Id))
               .ToListAsync();
        }

        public async Task<IList<Role>> GetSelected(Expression<Func<Role, bool>> expression)
        {
            return await _context.Roles
              .Include(a => a.UserRoles)
              .ThenInclude(u => u.User)
                 .Where(a => a.IsDeleted == false)
              .Where(expression)
              .ToListAsync();
        }
    }
}
