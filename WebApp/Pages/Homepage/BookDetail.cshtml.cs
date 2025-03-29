using API.DTO.Response;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Homepage
{
    public class BookDetailModel : PageModel
    {

        private readonly PRN221_Project_1Context context;
        private readonly HttpClient _httpClient;


        public BookDetailModel(PRN221_Project_1Context context, HttpClient httpClient)
        {
            this.context = context;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7186"); // Replace with your API base URL

        }
        public List<CategoryResponse> Categories { get; set; } = new List<CategoryResponse>();

        public string? UserId { get; set; }
        public int BookId { get; set; }

        public async Task OnGetAsync(int id)
        {
            //Categories = context.Categories.Select(x => new CategoryResponse {
            //    CateId =x.CateId,
            //    Name=x.Name
            //}).ToList();
           

            UserId = HttpContext.Session.GetString("userId");
            BookId = id;
             // Fetch categories
            var categoriesResponse = await _httpClient.GetAsync("api/categories");
            if (categoriesResponse.IsSuccessStatusCode)
            {
                Categories = await categoriesResponse.Content.ReadFromJsonAsync<List<CategoryResponse>>();
            }
        }
    }
}
