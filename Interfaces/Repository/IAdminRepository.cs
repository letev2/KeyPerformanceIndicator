using KpiNew.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KpiNew.Interfaces.Repository
{
    public interface IAdminRepository : IRepository<Admin>
    {
        public Task<Admin> Get(int id);
        public Task<Admin> Get(Expression<Func<Admin, bool>> expression);
        public Task<IList<Admin>> GetSelected(IList<int> ids);
        public Task<IList<Admin>> GetSelected(Expression<Func<Admin, bool>> expression);
        public Task<IList<Admin>> GetAll();
         public Task<Admin> GetAdminByEmail(string email);
        

    }
}
