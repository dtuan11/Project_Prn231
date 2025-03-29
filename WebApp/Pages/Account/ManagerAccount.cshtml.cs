using API.DTO;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebApp.Pages.Account
{
    public class ManagerAccountModel : PageModel
    {
        private readonly PRN221_Project_1Context context;
        private readonly HttpClient _httpClient;

        public ManagerAccountModel(HttpClient httpClient, PRN221_Project_1Context context)
        {
            _httpClient = httpClient;
            this.context = context;
        }
        public List<Category> Categories { get; set; } = new List<Category>();
        // public string? UserId { get; set; } = default!;
        public string? UserId { get; set; }

        //public List<User> users { get; set; } = new List<User>();

        public List<UserDto> users { get; set; } = new List<UserDto>();
        public User user { get; set; }
        public async Task<IActionResult> OnGet()
        {
            Categories = context.Categories.ToList();
            UserId = HttpContext.Session.GetString("userId");

            if (!string.IsNullOrEmpty(UserId))
            {
                user = context.Users.FirstOrDefault(x => x.UserId == int.Parse(UserId));
                if (user.RoleId == 0)
                {
                    try
                    {
                        var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7186/api/Users");
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var response = await _httpClient.SendAsync(request);

                        if (response.IsSuccessStatusCode)
                        {
                            var jsonResponse = await response.Content.ReadAsStringAsync();
                            users = JsonSerializer.Deserialize<List<UserDto>>(jsonResponse, new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Lỗi khi lấy danh sách người dùng.");
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, $"Lỗi: {ex.Message}");
                    }
                    return Page();
                }
                if (user.RoleId == 1)
                {
                    return NotFound();
                }
            }
            return RedirectToPage("/Homepage/Index");
        }

    }
}
