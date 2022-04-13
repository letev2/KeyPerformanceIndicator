using KpiNew.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KpiNew.Interfaces.Repository
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> GetByName(string name);
        bool ExistByName(string name);
        bool ExistById(int id);
        public Task<Role> Get(int id);
        public Task<Role> Get(Expression<Func<Role, bool>> expression);
        public Task<IList<Role>> GetSelected(IList<int> ids);
        public Task<IList<Role>> GetSelected(Expression<Func<Role, bool>> expression);
        public Task<IList<Role>> GetAll();


    }
}
