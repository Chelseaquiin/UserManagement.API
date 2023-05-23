using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Models.Enums
{
    public enum UserType
    {
        User = 1,
        Admin
    }
    public static class UserTypeExtension
    {
        public static string? GetStringValue(this UserType userType)
        {
            return userType switch
            {
                UserType.User => "User",
                UserType.Admin => "Admin",
                _ => null
            };
        }
    }
}
