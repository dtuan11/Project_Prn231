using API.DTO.Response;
using API.DTO.Response;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Shared
{
    public class HeaderModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public HeaderModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7186"); // Replace with your API base URL

        }
        public List<CategoryResponse> Categories { get; set; } = new List<CategoryResponse>();
        public string? UserId { get; set; }

        public async Task OnGetAsync()
        {
            //UserId = HttpContext.Session.GetString("userId");

            //// Fetch categories
            //var categoriesResponse = await _httpClient.GetAsync("api/books/categories");
            //if (categoriesResponse.IsSuccessStatusCode)
            //{
            //    Categories = await categoriesResponse.Content.ReadFromJsonAsync<List<CategoryResponse>>();
            //}
        }
    }
}
