using API.DTO.Response;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Rate
{
    public class RatingModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public RatingModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7186");
        }

        public List<CategoryResponse> Categories { get; set; } = new List<CategoryResponse>();
        public string? UserId { get; set; } = default!;
        public async Task<IActionResult> OnGet(int point, int bookId, int userId)
        {
            var categoriesResponse = await _httpClient.GetAsync("api/categories");
            if (categoriesResponse.IsSuccessStatusCode)
            {
                Categories = await categoriesResponse.Content.ReadFromJsonAsync<List<CategoryResponse>>();
            }
            UserId = HttpContext.Session.GetString("userId");

            var rateResponse = await _httpClient.GetAsync("api/books/rate?point="+point+"&bookId="+bookId+"&userId="+userId);
            
            return RedirectToPage("/Homepage/BookDetail", new { id = bookId });


        }
    }
}
