using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using API.Models;

namespace Web.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly PRN221_Project_1Context _context;
        private readonly HttpClient _httpClient;

        public IndexModel(PRN221_Project_1Context context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7186/"); // Replace with your API base URL
        }

        public List<Category> Categories { get; set; } = new List<Category>();
        public string UserId { get; set; } = default!;
        [BindProperty]
        public User UserLogin { get; set; } = default!;
        public string Message { get; set; } = string.Empty;

        public void OnGet()
        {
            Categories = _context.Categories.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var loginRequest = new
            {
                userName = UserLogin.UserName,
                password = UserLogin.Password
            };

            var content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                HttpContext.Session.SetString("userId", result.UserId); // Assuming API returns userId
                return RedirectToPage("/Homepage/Index");
            }
            else
            {
                var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                Message = error.Message;
                return Page();
            }
        }
    }

    public class LoginResponse
    {
        public string Message { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty; // Add this if API returns it
    }

    public class ErrorResponse
    {
        public string Message { get; set; } = string.Empty;
    }
}
