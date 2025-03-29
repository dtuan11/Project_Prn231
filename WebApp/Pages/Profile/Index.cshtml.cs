using API.Controllers;
using API.DTO.Response;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace WebApp.Pages.Profile
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;


        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7186");
        }

        public List<CategoryResponse> Categories { get; set; } = new List<CategoryResponse>();
        public string? UserId { get; set; } = default!;
        [BindProperty]
        public User UserModel { get; set; } = default!;
        [BindProperty]
        public IFormFile File { get; set; }
        public List<ReadingResponse> Readings { get; set; } = new List<ReadingResponse>();
        public User user { get; set; }

        public string Message { get; set; } = string.Empty;

        public async Task OnGetAsync(int id)
        {         

            var categoriesResponse = await _httpClient.GetAsync("api/categories");
            if (categoriesResponse.IsSuccessStatusCode)
            {
                Categories = await categoriesResponse.Content.ReadFromJsonAsync<List<CategoryResponse>>();
            }
            UserId = HttpContext.Session.GetString("userId");
            var readingResponse = await _httpClient.GetAsync("api/Profile/"+UserId+"/readings");
            if (readingResponse.IsSuccessStatusCode)
            {
                Readings = await readingResponse.Content.ReadFromJsonAsync<List<ReadingResponse>>();
            }
            var userResponse = await _httpClient.GetAsync("userDetail/" + int.Parse(UserId));
            if (userResponse.IsSuccessStatusCode)
            {
                user = await userResponse.Content.ReadFromJsonAsync<User>();
            }

            
            if (user != null)
            {
                UserModel = user;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string newPassword = Request.Form["newPassword"];
            string currentPassword = Request.Form["currentPassword"];
            if (currentPassword != null)
            {
                currentPassword = HashPassword(currentPassword);
            }
            string confirmPassword = Request.Form["confirmPassword"];
            string userId = Request.Form["userId"];
            var userResponse = await _httpClient.GetAsync("userDetail/" + int.Parse(userId));
            if (userResponse.IsSuccessStatusCode)
            {
                user = await userResponse.Content.ReadFromJsonAsync<User>();
            }
            var request = new UserUpdateRequest
            {
                CurrentPassword = currentPassword,
                NewPassword = newPassword,
                ConfirmPassword = confirmPassword,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone,

                
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var res = await _httpClient.PutAsync("api/users/"+user.UserId, jsonContent);

            return RedirectToPage("/Profile/Index", new { id = user.UserId });
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
}
