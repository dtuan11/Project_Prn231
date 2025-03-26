using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Profile
{
    public class ViewStatisticUserBookViewModel : PageModel
    {
        private readonly PRN221_Project_1Context context;

        public ViewStatisticUserBookViewModel(PRN221_Project_1Context context)
        {
            this.context = context;
        }
        public List<string> Username { get; set; } = new List<string>();
        public List<int> ViewCounts { get; set; } = new List<int>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public string? UserId { get; set; } = default!;
        public User user { get; set; }
        public void OnGet()
        {
            Categories = context.Categories.ToList();
            UserId = HttpContext.Session.GetString("userId");
            var userBookViews = context.Users
            .Where(user => user.Books.Any(b => b.Views.HasValue && b.Views > 0)) 
         .Select(user => new
         {
             Username = user.UserName,
             TotalViews = user.Books.Where(b => b.Views.HasValue && b.Views > 0).Sum(b => b.Views.Value)
         })
         .ToList();

            Username = userBookViews.Select(u => u.Username).ToList();
            ViewCounts = userBookViews.Select(u => u.TotalViews).ToList();

            user = context.Users.FirstOrDefault(x => x.UserId == int.Parse(UserId));
        }
    }
}
