using API.DTO.Response;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Homepage
{
    public class BookDetailModel : PageModel
    {

        private readonly PRN221_Project_1Context context;

        public BookDetailModel(PRN221_Project_1Context context)
        {
            this.context = context;
        }
        public List<CategoryResponse> Categories { get; set; } = new List<CategoryResponse>();

        public string? UserId { get; set; }
        public int BookId { get; set; }

        public async Task OnGet(int id)
        {
            Categories = context.Categories.Select(x => new CategoryResponse {
                CateId =x.CateId,
                Name=x.Name
            }).ToList();


            UserId = HttpContext.Session.GetString("userId");
            BookId = id;
            
        }
    }
}
