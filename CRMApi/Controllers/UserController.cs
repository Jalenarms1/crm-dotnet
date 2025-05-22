using System.Net.Mail;
using System.Text;
using CRMApi.Models;
using CRMApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User user)
        {

            Console.WriteLine($"{user.Email} hit");

            var hasher = new PasswordHasher<object>();

            var hashedPassword = hasher.HashPassword(new object(), user.Password);
            Console.WriteLine($"Hashed pass: {hashedPassword}");

            var foundUser = await _userService.GetUserByEmail(user.Email);

            if (foundUser is null)
            {
                return NotFound("no user found");
            }

            if (hasher.VerifyHashedPassword(new object(), foundUser.Password, user.Password) == PasswordVerificationResult.Failed)
            {
                return BadRequest("invalid credentials");
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> NewUser([FromBody] User user)
        {
            try
            {
                var email = new MailAddress(user.Email);
                if (email.Address != user.Email)
                {
                    throw new Exception();
                }

                var existingUser = await _userService.GetUserByEmail(user.Email);
                if (existingUser is not null)
                {
                    return BadRequest("existing user with the current email provided");
                }

                Console.WriteLine($"{user.Name}");

                var hashedPassword = new PasswordHasher<object>().HashPassword(new object(), user.Password);

                user.Password = hashedPassword;

                await _userService.CreateUser(user);

                return Ok();

            }
            catch
            {
                return BadRequest("please enter a valid email address");
            }
        }
    }
    
}
