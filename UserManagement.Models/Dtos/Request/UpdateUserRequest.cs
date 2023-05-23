using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models.Enums;

namespace UserManagement.Models.Dtos.Request
{
    public class UpdateUserRequest
    {
        public string Firstname { get; set; }

        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string MobileNumber { get; set; }

        public UserType UserTypeId { get; set; }

    }
}
