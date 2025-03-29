using API.Models;
using API.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userRepository.LoginAsync(request.UserName, request.Password);
            if (user == null)
            {
                return BadRequest("Invalid username or password.");
            }

            if (!user.Active)
            {
                return BadRequest("Account is locked.");
            }

            // Kiểm tra session có được lưu hay không
            var sessionUserId = HttpContext.Session.GetString("userId");
            Console.WriteLine($"Session UserId: {sessionUserId}"); // Debug

            return Ok(new
            {
                Message = "Login Success",
                UserId = user.UserId.ToString()
            });
        }




        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(request.UserName);
            if (existingUser != null)
            {
                return BadRequest("Username already exists.");
            }

            if (request.Password != request.ConfirmPassword)
            {
                return BadRequest("Passwords do not match.");
            }

            var user = new User
            {
                UserName = request.UserName,
                Password = HashPassword(request.Password),
                Active = true,
                RoleId = 1
            };

            await _userRepository.AddUserAsync(user);
            HttpContext.Session.SetString("userId", user.UserId.ToString());
            return Ok(new { UserId = user.UserId });
        }

        // POST: api/auth/logout
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Ok();
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }

    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
