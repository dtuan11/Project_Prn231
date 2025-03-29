using API.DTO.Response;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Profile
{
    public class ViewStatisticModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public ViewStatisticModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7186");
        }

        public List<string> Titles { get; set; } = new List<string>();
        public List<int> ViewCounts { get; set; } = new List<int>();
        public List<CategoryResponse> Categories { get; set; } = new List<CategoryResponse>();
        public string? UserId { get; set; } = default!;
        public User user { get; set; } = default!;
        public async Task OnGet()
        {
            var categoriesResponse = await _httpClient.GetAsync("api/categories");
            if (categoriesResponse.IsSuccessStatusCode)
            {
                Categories = await categoriesResponse.Content.ReadFromJsonAsync<List<CategoryResponse>>();
            }
            UserId = HttpContext.Session.GetString("userId");
            var bookViewsResponse = await _httpClient.GetAsync("api/Statics/bookviews");
            if (bookViewsResponse.IsSuccessStatusCode)
            {
                var books = await bookViewsResponse.Content.ReadFromJsonAsync<List<ViewStaticsResponse>>();
                Titles = books.Select(b => b.Title).ToList();
                ViewCounts = books.Select(b => b.Views).ToList();
                
            }
            var userResponse = await _httpClient.GetAsync("userDetail/" + int.Parse(UserId));
            if (userResponse.IsSuccessStatusCode)
            {
                user = await userResponse.Content.ReadFromJsonAsync<User>();
            }



        }
    }
}
