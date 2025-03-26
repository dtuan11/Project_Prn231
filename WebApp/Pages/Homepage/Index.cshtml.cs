using API.DTO.Response;
using API.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace WebApp.Pages.Homepage
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel( HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7186"); // Replace with your API base URL

        }

        public List<BookResponse> Books { get; set; } = new List<BookResponse>();
        public List<BookResponse> NewBooks { get; set; } = new List<BookResponse>();
        public List<CategoryResponse> Categories { get; set; } = new List<CategoryResponse>();
        public string? UserId { get; set; }

        public async Task OnGetAsync()
        {
            UserId = HttpContext.Session.GetString("userId");


            // Fetch approved books
            var booksResponse = await _httpClient.GetAsync("api/books/approved");
            if (booksResponse.IsSuccessStatusCode)
            {
                Books = await booksResponse.Content.ReadFromJsonAsync<List<BookResponse>>();
            }

            // Fetch new books with latest chapters
            var newBooksResponse = await _httpClient.GetAsync("api/books/new");
            if (newBooksResponse.IsSuccessStatusCode)
            {
                NewBooks = await newBooksResponse.Content.ReadFromJsonAsync<List<BookResponse>>();
            }

            // Fetch categories
            var categoriesResponse = await _httpClient.GetAsync("api/books/categories");
            if (categoriesResponse.IsSuccessStatusCode)
            {
                Categories = await categoriesResponse.Content.ReadFromJsonAsync<List<CategoryResponse>>();
            }
        }
    }
}
