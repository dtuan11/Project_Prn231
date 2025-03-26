using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Book
{
    public class DeleteBookModel : PageModel
    {
        private readonly PRN221_Project_1Context context;

        public DeleteBookModel(PRN221_Project_1Context context)
        {
            this.context = context;
        }

        public IActionResult OnGet(int id)
        {
            var exist = context.Books.Find(id);
            if (exist != null)
            {
                exist.Status = "Delete";
                context.Books.Update(exist);
                context.SaveChanges();
            }
            return RedirectToPage("/Book/ManageBook");
        }
    }
}
