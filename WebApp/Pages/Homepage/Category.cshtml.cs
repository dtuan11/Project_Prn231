using API.DTO.Response;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text.Json;

namespace WebApp.Pages.Homepage
{
    public class CategoryModel : PageModel
    {

        private readonly PRN221_Project_1Context context;
        private readonly HttpClient _httpClient;


        public CategoryModel(PRN221_Project_1Context context, HttpClient httpClient)
        {
            this.context = context;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7186"); // Replace with your API base URL

        }

        public List<API.Models.Book> Books { get; set; } = new List<API.Models.Book>();
        public List<CategoryResponse> Categories { get; set; } = new List<CategoryResponse>();
        public string? UserId { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Fetch categories
            var categoriesResponse = await _httpClient.GetAsync("api/categories");
            if (categoriesResponse.IsSuccessStatusCode)
            {
                Categories = await categoriesResponse.Content.ReadFromJsonAsync<List<CategoryResponse>>();
            }
            UserId = HttpContext.Session.GetString("userId");
            Books = context.CategoryInBooks.Include(x => x.Book).Where(x => x.CateId == id && x.Book.Approve.Equals("Approved") && !x.Book.Status.Equals("Delete")).Select(x => x.Book).ToList();
            if (context.Categories.Find(id) != null)
            {
                return Page();
            }
            else
            {
                return NotFound();
            }


        }
    }
}
