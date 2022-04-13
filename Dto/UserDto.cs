using KpiNew.Entities;
using KpiNew.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KpiNew.Dto
{
    public class UserDto
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<RoleDto> Roles { get; set; } = new List<RoleDto>();
    }

  
    public class LoginUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class CreateUserRequestModel
    {

        [Required]
        [StringLength(maximumLength:20, MinimumLength = 3 )]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength:20, MinimumLength = 3 )]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = "")]
        public string Password { get; set; }
        public IList<int> ListOfRoles { get; set; } = new List<int>();

    }

    public class UpdateUserRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }
}

