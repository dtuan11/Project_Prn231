using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Chapter
{
    public class DeleteChapterModel : PageModel
    {
        private readonly PRN221_Project_1Context context;

        public DeleteChapterModel(PRN221_Project_1Context context)
        {
            this.context = context;
        }

        public IActionResult OnGet(int id)
        {
            var exist = context.Chapters.Find(id);
            if (exist != null)
            {
                context.Chapters.Remove(exist);
                context.SaveChanges();
                return RedirectToPage("/Chapter/ChapterList", new { id = exist.BookId });
            }
            else
            {
                return NotFound();
            }
            
        }
    }
}
