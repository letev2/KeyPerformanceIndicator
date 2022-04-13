using KpiNew.Enum;
using System;

namespace KpiNew.Entities
{
    public class Admin : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string DashBoard { get; set; }
        public string AdminImage { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        
    }
}
