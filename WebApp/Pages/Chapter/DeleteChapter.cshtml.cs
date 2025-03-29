using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;

namespace WebApp.Pages.Chapter
{
    public class DeleteChapterModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public DeleteChapterModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7186");
        }

        public async Task<IActionResult> OnGet(int id, int bookId)
        {
           
            var res = await _httpClient.DeleteAsync("api/chapter/"+id);

            if (res.IsSuccessStatusCode)
            {

                return RedirectToPage("/Chapter/ChapterList", new { id = bookId });
            }
            else
            {
                return NotFound();
            }

        }
    }
}
