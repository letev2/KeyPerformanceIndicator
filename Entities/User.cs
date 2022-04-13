using KpiNew.Enum;
using System;
using System.Collections.Generic;

namespace KpiNew.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Admin Admin { get; set; }
        public Employee  Employee { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
