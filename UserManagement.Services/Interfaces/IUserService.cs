using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models.Dtos.Request;
using UserManagement.Models.Dtos.Response;

namespace UserManagement.Services.Interfaces
{
    public interface IUserService
    {
        Task<AccountResponse> CreateUser(UserRegistrationRequest request);
        Task DeleteUser(string email);
        Task<AccountResponse> UpdateUser(UpdateUserRequest request);
    }
}
