using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Profile
{
    public class ViewStatisticModel : PageModel
    {
        private readonly PRN221_Project_1Context context;

        public ViewStatisticModel(PRN221_Project_1Context context)
        {
            this.context = context;
        }
        public List<string> Titles { get; set; } = new List<string>();
        public List<int> ViewCounts { get; set; } = new List<int>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public string? UserId { get; set; } = default!;
        public User user { get; set; }
        public void OnGet()
        {
            Categories = context.Categories.ToList();
            UserId = HttpContext.Session.GetString("userId");
            var books =  context.Books
           .Where(b => b.Views.HasValue && b.Views != 0)
           .Select(b => new { b.Title, b.Views })
           .ToList();

            Titles = books.Select(b => b.Title).ToList();
            ViewCounts = books.Select(b => b.Views.Value).ToList();
            user = context.Users.FirstOrDefault(x => x.UserId == int.Parse(UserId));
        }
    }
}
