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

        public CategoryModel(PRN221_Project_1Context context)
        {
            this.context = context;
        }

        public List<API.Models.Book> Books { get; set; } = new List<API.Models.Book>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public string? UserId { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            Categories = context.Categories.ToList();
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
