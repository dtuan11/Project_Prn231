using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Homepage
{
    public class ChapterModel : PageModel
    {
        private readonly PRN221_Project_1Context context;

        public ChapterModel(PRN221_Project_1Context context)
        {
            this.context = context;
        }
        public List<Category> Categories { get; set; } = new List<Category>();
        public string? UserId { get; set; }
        public int ChapterId { get; set; }
        public int BookId { get; set; }
        public void OnGet(int id, int bookId)
        {
            Categories = context.Categories.ToList();
            UserId = HttpContext.Session.GetString("userId");

            ChapterId = id;
            BookId = bookId;

        }
    }
}
