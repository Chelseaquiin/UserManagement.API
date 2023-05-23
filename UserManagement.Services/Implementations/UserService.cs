using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Data.Interfaces;
using UserManagement.Models.Dtos.Request;
using UserManagement.Models.Dtos.Response;
using UserManagement.Models.Entities;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        
        public UserService(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            
            _userManager = _serviceFactory.GetService<UserManager<ApplicationUser>>();
            _roleManager = _serviceFactory.GetService<RoleManager<ApplicationRole>>();
            _unitOfWork = _serviceFactory.GetService<IUnitOfWork>();
            
        }

        public async Task<AccountResponse> UpdateUser(UpdateUserRequest request)
        {
            ApplicationUser existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser == null)
                throw new InvalidOperationException("User not found.");

            var user = _userManager.FindByIdAsync(existingUser.Id);

            existingUser.FirstName = request.Firstname;
            existingUser.LastName = request.LastName;
            existingUser.Email = request.Email;
            existingUser.UserTypeId = request.UserTypeId;

            try
            {
                var updateUser = await _userManager.UpdateAsync(existingUser);
                var addRole = await _userManager.AddToRoleAsync(existingUser, request.UserTypeId.ToString());
                return new AccountResponse()
                {
                    UserId = existingUser.Id,
                    UserName = existingUser.UserName,
                    Success = true,
                    Message = "Role updated successfully"
                };
            }
            catch (Exception)
            {
                return new AccountResponse()
                {
                    UserId = existingUser.Id,
                    UserName = existingUser.UserName,
                    Success = false,
                    Message = "Update failed"
                };
            }

        }

        public async Task<AccountResponse> CreateUser(UserRegistrationRequest request)
        {

            ApplicationUser existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
                throw new InvalidOperationException($"User already exists with Email {request.Email}");

            existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
                throw new InvalidOperationException($"User already exists with username {request.UserName}");

            var roleName = "User";
            var userRole = await _roleManager.FindByNameAsync(roleName);

            ApplicationUser user = new()
            {

                Email = request.Email.ToLower(),
                UserName = request.UserName.Trim().ToLower(),
                FirstName = request.Firstname.Trim(),
                LastName = request.LastName.Trim(),
                PhoneNumber = request.MobileNumber,
                Active = true,
                UserTypeId = userRole.Type
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);


            if (!result.Succeeded)
            {
                var message = $"Failed to create user: {(result.Errors.FirstOrDefault())?.Description}";
                throw new InvalidOperationException(message);

            }

            return new AccountResponse
            {
                UserId = user.Id,
                UserName = user.UserName,
                Success = true,
                Message = "your account has been created"

            };

        }


        public async Task DeleteUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            await _userManager.DeleteAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
