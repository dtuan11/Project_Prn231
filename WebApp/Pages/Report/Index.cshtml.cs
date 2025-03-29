using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Report
{
    public class IndexModel : PageModel
    {
        private readonly PRN221_Project_1Context context;

        public IndexModel(PRN221_Project_1Context context)
        {
            this.context = context;
        }
        public List<Category> Categories { get; set; } = new List<Category>();
        public string? UserId { get; set; } = default!;
        public List<User> users { get; set; } = new List<User>();
        public User user { get; set; }
        public IActionResult OnGet()
        {
            Categories = context.Categories.ToList();
            UserId = HttpContext.Session.GetString("userId");
            if (!string.IsNullOrEmpty(UserId))
            {
                user = context.Users.FirstOrDefault(x => x.UserId == int.Parse(UserId));
                if (user.RoleId == 0)
                {
                    users = context.Users.Include(x => x.Role)
                        .Include(x => x.Books)
                        .Include(x => x.Rates)
                        .Include(x => x.Responses).ToList();
                    return Page();
                }
                if (user.RoleId == 1)
                {
                    return NotFound();
                }
            }
            return RedirectToPage("/Homepage/Index");
        }
    }
}
