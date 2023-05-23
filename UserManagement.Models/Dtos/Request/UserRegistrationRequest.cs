using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models.Enums;

namespace UserManagement.Models.Dtos.Request
{
    public class UserRegistrationRequest
    {
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string MobileNumber { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string LastName { get; set; }
        [Required]
        public string Role { get; set; }

        [Required]
        public UserType UserTypeId { get; set; }

        public string Department { get; set; }

        public string MatricNumber { get; set; }
    }

}

