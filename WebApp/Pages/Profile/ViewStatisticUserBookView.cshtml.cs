using API.DTO.Response;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Profile
{
    public class ViewStatisticUserBookViewModel : PageModel
    {
        
        private readonly HttpClient _httpClient;

        public ViewStatisticUserBookViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7186");
        }

        public List<string> Username { get; set; } = new List<string>();
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
            var userBookViewsResponse = await _httpClient.GetAsync("api/Statics/userbookview");
            if (userBookViewsResponse.IsSuccessStatusCode)
            {
                var userBookViews = await userBookViewsResponse.Content.ReadFromJsonAsync<List<UserBookViewResponse>>();
                Username = userBookViews.Select(u => u.Username).ToList();
                ViewCounts = userBookViews.Select(u => u.TotalCount).ToList();
            }
            var userResponse = await _httpClient.GetAsync("userDetail/" + int.Parse(UserId));
            if (userResponse.IsSuccessStatusCode)
            {
                user = await userResponse.Content.ReadFromJsonAsync<User>();
            }
            //   var userBookViews = context.Users
            //   .Where(user => user.Books.Any(b => b.Views.HasValue && b.Views > 0)) 
            //.Select(user => new
            //{
            //    Username = user.UserName,
            //    TotalViews = user.Books.Where(b => b.Views.HasValue && b.Views > 0).Sum(b => b.Views.Value)
            //})
            //.ToList();

            //   Username = userBookViews.Select(u => u.Username).ToList();
            //   ViewCounts = userBookViews.Select(u => u.TotalViews).ToList();

            //   user = context.Users.FirstOrDefault(x => x.UserId == int.Parse(UserId));
        }
    }
}
