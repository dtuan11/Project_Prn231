using API.DTO;
using API.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/users
        /*[HttpGet]
        public async Task<IActionResult> GetUsers()
        {
*//*
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }*/

        /*var user = await _userRepository.GetUserByIdAsync(int.Parse(userId));
        if (user.User == null || user.User.RoleId != 0)
        {
            return Forbid();
        }*//*

        var users = await _userRepository.GetAllUsersAsync();
        return Ok(users);
    }*/
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();

            var userDtos = users.Select(u => new UserDto
            {
                UserId = u.UserId,
                UserName = u.UserName,
                Address = u.Address,
                Phone = u.Phone,
                Email = u.Email,
                Avatar = u.Avatar,
                RoleId = u.RoleId,
                RoleName = u.Role != null ? u.Role.RoleName : "Unknown", 
                Active = u.Active
            }).ToList();
            return Ok(userDtos);
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateUser(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            user.Active = false; 
            await _userRepository.UpdateUserAsync(user);

            return Ok(new { message = "User deactivated successfully" });
        }

        [HttpPut("{id}/updaetroleadmin")]
        public async Task<IActionResult> UpdateRoleAdmin(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            user.RoleId = 0;
            await _userRepository.UpdateUserAsync(user);

            return Ok(new { message = "User change role to Admin successfully" });
        }

        [HttpPut("{id}/updaetroleuser")]
        public async Task<IActionResult> UpdateRoleUser(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            user.RoleId = 1;
            await _userRepository.UpdateUserAsync(user);

            return Ok(new { message = "User change role to Admin successfully" });
        }

        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateUser(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            user.Active = true;
            await _userRepository.UpdateUserAsync(user);

            return Ok(new { message = "User deactivated successfully" });
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var (user, readings) = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new { User = user, Readings = readings });
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromForm] UserUpdateRequest request)
        {
            var user = (await _userRepository.GetUserByIdAsync(id)).User;
            if (user == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(request.NewPassword))
            {
                var currentPasswordHash = HashPassword(request.CurrentPassword);
                if (!currentPasswordHash.Equals(user.Password))
                {
                    return BadRequest("Current password is incorrect.");
                }

                if (request.NewPassword != request.ConfirmPassword)
                {
                    return BadRequest("New password and confirmation do not match.");
                }

                user.Password = HashPassword(request.NewPassword);
            }

            if (request.File != null)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var filePath = Path.Combine(folderPath, request.File.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.File.CopyToAsync(stream);
                }

                user.Avatar = "/images/" + request.File.FileName;
            }

            user.Email = request.Email;
            user.Address = request.Address;
            user.Phone = request.Phone;

            await _userRepository.UpdateUserAsync(user);
            return Ok(user);
        }

        // GET: api/users/statistics/book-views
        [HttpGet("statistics/book-views")]
        public async Task<IActionResult> GetBookViewStatistics()
        {
            var (titles, viewCounts) = await _userRepository.GetBookViewStatisticsAsync();
            return Ok(new { Titles = titles, ViewCounts = viewCounts });
        }

        // GET: api/users/statistics/user-book-views
        [HttpGet("statistics/user-book-views")]
        public async Task<IActionResult> GetUserBookViewStatistics()
        {
            var (usernames, viewCounts) = await _userRepository.GetUserBookViewStatisticsAsync();
            return Ok(new { Usernames = usernames, ViewCounts = viewCounts });
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }

    public class UserUpdateRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public IFormFile File { get; set; }
    }
}
