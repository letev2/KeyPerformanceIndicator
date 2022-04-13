using KpiNew.Enum;
using System;
using System.Collections.Generic;

namespace KpiNew.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }  
        public string EmployeeImage { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public double SumTotal {get; set;}
        public User User { get; set; }
        public ICollection<EmployeeKpi> EmployeeKpis { get; set; } = new List<EmployeeKpi>();
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
