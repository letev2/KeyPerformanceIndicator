using KpiNew.Entities;
using KpiNew.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KpiNew.Dto
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }
        public DepartmentDto Department { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string EmployeeImage { get; set; }
        public ICollection<KpiDto> Kpis { get; set; } = new List<KpiDto>();
        public double SumTotal { get; set; }
    }


    public class CreateEmployeeRequestModel
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string LastName { get; set; }

        public string Address { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "")]
        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "")]
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "")]
        public string Email { get; set; }

        [DataType(DataType.Password, ErrorMessage = "")]
        public string Password { get; set; }
        public int DepartmentId { get; set; }
        public int UserId { get; set; }
        public string EmployeeImage { get; set; }
        public IList<int> ListOfKpis { get; set; } = new List<int>();
    }

    public class LoginRespondeModel : BaseRespond<UserDto>
    {
        public string Token { get; set; }
    }
    

    public class UpdateEmployeeRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string EmployeeImage { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class EmployeeRatingPerformance
    {
        
        public Month Month {get;set;}
        public int Year {get;set;}
        

    }

    public class RatingPerformance
    {
        public int EmployeeId { get; set; }
        public int KpiId { get; set; }
        public Month Month { get; set; }
        public int Year { get; set; }
        public double KpiRating { get; set; }
    }

    public class EmployeeKpiDto
    {
        public int Id {get; set;}
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int KpiId { get; set; }
        public Kpi Kpi { get; set; }
        public double KpiRating { get; set; }
        public Month Month { get; set; }
        public int Year { get; set; }
    }



}
