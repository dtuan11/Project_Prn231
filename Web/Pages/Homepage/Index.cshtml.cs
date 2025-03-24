using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Web.Pages.Homepage
{
    public class IndexModel : PageModel
    {
        private readonly PRN221_Project_1Context context;

        public IndexModel(PRN221_Project_1Context context)
        {
            this.context = context;
        }
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Book> NewBooks { get; set; } = new List<Book>();
        public Dictionary<int, Chapter> LatestChap { get; set; } = new Dictionary<int, Chapter>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public string? UserId { get; set; } = default!;
        public void OnGet()
        {
            Categories = context.Categories.ToList();
            UserId = HttpContext.Session.GetString("userId");
            Books = context.Books.Where(x => !x.Status.Equals("Delete") && x.Approve.Equals("Approved")).ToList();
            NewBooks = context.Books.Include(x => x.Chapters).Where(x => !x.Status.Equals("Delete") && x.Approve.Equals("Approved") && x.Chapters.Any()).OrderByDescending(x => x.PublishDate).ToList();
            foreach (var b in NewBooks)
            {
                var c = b.Chapters.OrderByDescending(x => x.NumberChapter).First();
                LatestChap.Add(b.BookId, c);
            }
        }
        public void OnPost() { }
    }
}
