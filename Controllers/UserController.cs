using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using web_api.Authentication;
using web_api.Dtos;
using web_api.Model;
namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("admins")]
        [Authorize(Roles = "Administrator")]
        public IActionResult AdminsEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.UserName}, you are an {currentUser.Role}");
        }


        [HttpGet]
        [Route("test")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserTest()
        {
            var user = await _userManager.GetUsersInRoleAsync("User");

            return Ok(user);
        }


        [HttpGet]
        [Route("users")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsers()
        {
            var user = await _userManager.Users.ToListAsync();

            return Ok(user);
        }

        [HttpGet]
        [Route("user")]
        public IActionResult GetUser()
        {
            var currentUser = GetCurrentUser();

            return Ok(new { currentUser.UserName, currentUser.Role });
        }

        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ChangePasswordUser([FromBody] ChangePasswordDto model)
        {
            var currentUser = GetCurrentUser();
            var user = await _userManager.FindByNameAsync(currentUser.UserName);
            if (!(await _userManager.CheckPasswordAsync(user, model.CurrentPassword))) return BadRequest(new { message = "Current password invalid" });
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.UpdatePassword);
            if (!result.Succeeded) return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Something wrong" });

            return Ok(new Response { Status = "Success", Message = "Password changed successfully!" });
        }

        private UserModel GetCurrentUser()
        {
            // var currentUserId = User.Claims.ToList().FirstOrDefault(x => x.Type == "id").Value;
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new UserModel
                {
                    UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
    }
}
