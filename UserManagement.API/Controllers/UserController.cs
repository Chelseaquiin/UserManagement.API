using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Models.Dtos.Request;
using UserManagement.Models.Dtos.Response;
using UserManagement.Services.Interfaces;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService adminService)
        {
            _userService = adminService;
        }

        [AllowAnonymous]
        [HttpPost("Sign-Up", Name = "Create-User")]
        public async Task<IActionResult> CreateUser(UserRegistrationRequest request)
        {
            var response = await _userService.CreateUser(request);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPut("Edit-User", Name = "Update-User")]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
        {
            var response = await _userService.UpdateUser(request);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpDelete("Delete-User", Name = "Delete-user")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            await _userService.DeleteUser(email);
            return Ok();
        }


    }
}
