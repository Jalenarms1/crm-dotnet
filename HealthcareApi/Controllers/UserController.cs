using System.Security.Claims;
using CarelinkModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetMe()
        {
            var user = HttpContext.User;
            Console.WriteLine(user);

            if (user?.Identity is not {IsAuthenticated: true})
            {
                return Unauthorized();
            }

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            var role = user.FindFirst(ClaimTypes.Role)?.Value;
            var name = user.FindFirst("name")?.Value;

            Console.WriteLine($"UserId: {userId}");
            Console.WriteLine($"Email: {email}");
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Role: {role}");

            var retUser = new User { Email = email, Name = name, Role = Enum.Parse<UserRoleEnum>(role) };

            return Ok(retUser);

        }
    }
}
