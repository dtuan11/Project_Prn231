using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace WebApp.Pages.Login
{
    public class RegisterModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public User UserModel { get; set; } = default!;

        [BindProperty]
        public string ConfirmPassword { get; set; } = default!;

        public string Message { get; set; } = string.Empty;

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();

            var requestBody = new
            {
                UserName = UserModel.UserName,
                Password = UserModel.Password,
                ConfirmPassword = ConfirmPassword
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://localhost:7186/api/Auth/register", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                Message = await response.Content.ReadAsStringAsync();
                return Page();
            }

            var responseData = JsonSerializer.Deserialize<RegisterResponse>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            HttpContext.Session.SetString("userId", responseData.UserId.ToString());
            return RedirectToPage("/Homepage/Index");
        }

        private class RegisterResponse
        {
            public int UserId { get; set; }
        }
    }
}
