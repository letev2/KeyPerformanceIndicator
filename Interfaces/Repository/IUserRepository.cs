using KpiNew.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KpiNew.Interfaces.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> Get(int id);
        public Task<User> Get(Expression<Func<User, bool>> expression);
        public Task<IList<User>> GetSelected(IList<int> ids);
        public Task<IList<User>> GetSelected(Expression<Func<User, bool>> expression);
        public Task<IList<User>> GetAll();
        public Task<User> GetByEmail(string email);

    }
}
