using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Rate
{
    public class RatingModel : PageModel
    {
        private readonly PRN221_Project_1Context context;

        public RatingModel(PRN221_Project_1Context context)
        {
            this.context = context;
        }

        public List<Category> Categories { get; set; } = new List<Category>();
        public string? UserId { get; set; } = default!;
        public IActionResult OnGet(int point, int bookId, int userId)
        {
            Categories = context.Categories.ToList();
            UserId = HttpContext.Session.GetString("userId");
            var existRate = context.Rates.FirstOrDefault(r => r.UserId == userId && r.BookId == bookId);
            if (existRate != null)
            {
                existRate.Point = point;
                context.Rates.Update(existRate);
                context.SaveChanges();
            }
            else
            {
                var rate = new API.Models.Rate
                {
                    BookId = bookId,
                    UserId = userId,
                    Point = point,
                };
                context.Rates.Add(rate);
                context.SaveChanges();
            }
            return RedirectToPage("/Homepage/BookDetail",new {id = bookId});
        }
    }
}
