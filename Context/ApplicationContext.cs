
using KpiNew.Entities;
using Microsoft.EntityFrameworkCore;

namespace KpiNew.Context
{
    public class ApplicationContext : DbContext
    {
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }  
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Kpi> Kpis { get; set; }
        public DbSet<EmployeeKpi> EmployeeKpis { get; set; }
        public DbSet<Department> Departments { get; set; }
        

    



    }
}

