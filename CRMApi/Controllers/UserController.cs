using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using CRMApi.Enums;
using CRMApi.Models;
using CRMApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CRMApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetMe()
        {
            var claims = HttpContext.User.Claims;
             // Iterate through claims
            foreach (var claim in claims)
            {
                Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
            }
            Console.WriteLine("claim");
            Console.WriteLine(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok();
        }
    }
    
}
