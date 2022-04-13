using KpiNew.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace KpiNew.Dto
{
    public class AdminDto
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string DashBoard { get; set; }
        public string Password { get; set; }
        public string AdminImage { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public int UserId { get; set; }
        
    }


    public class CreateAdminRequestModel
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage ="")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = "")]
        public string Password { get; set; }
        public string DashBoard { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "")]
        public string Email { get; set; }
        public string adminPhoto { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public int UserId { get; set; }
        public string UserImage { get; set; }

    }

    public class UpdateAdminRequestModel
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
       [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Address { get; set; }
       
        [Required]
        [DataType(DataType.EmailAddress)]   
        public string Email { get; set; }
        public string AdminImage { get; set; }
        public DateTime DateOfBirth { get; set; }
        
    }
}
