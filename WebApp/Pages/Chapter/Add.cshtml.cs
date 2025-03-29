using API.DTO.Request;
using API.DTO.Response;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;

namespace WebApp.Pages.Chapter
{
    public class AddModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public AddModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7186"); // Replace with your API base URL
        }

        [BindProperty]
        public API.Models.Chapter Chapter { get; set; } = default!;
        public string Message { get; set; } = string.Empty;
        [BindProperty]
        public int BookId { get; set; }
        public List<CategoryResponse> Categories { get; set; } = new List<CategoryResponse>();
        public string? UserId { get; set; } = default!;
        public async Task OnGet(int bookId)
        {
            BookId = bookId;
            var categoriesResponse = await _httpClient.GetAsync("api/categories");
            if (categoriesResponse.IsSuccessStatusCode)
            {
                Categories = await categoriesResponse.Content.ReadFromJsonAsync<List<CategoryResponse>>();
            }
            UserId = HttpContext.Session.GetString("userId");
        }
        public async Task<IActionResult> OnPost()
        {
            var chapterResponse = await _httpClient.GetAsync("api/chapters/AllChapter?bookId=" + BookId);
            if (chapterResponse.IsSuccessStatusCode)
            {
                var chapterList = await chapterResponse.Content.ReadFromJsonAsync<List<ChapterDetailResponse>>();
                var existChapterNumber = chapterList.FirstOrDefault(x => x.BookId == BookId && x.NumberChapter == Chapter.NumberChapter);
                if (existChapterNumber != null)
                {
                    Message = "Đã có chương " + existChapterNumber.NumberChapter + " trong truyện";
                    return Page();
                }
            }

            API.Models.Book? book = new API.Models.Book();
            var categoriesResponse = await _httpClient.GetAsync("api/books/"+BookId);
            if (categoriesResponse.IsSuccessStatusCode)
            {
                book = await categoriesResponse.Content.ReadFromJsonAsync<API.Models.Book>();
            }
            string content = Request.Form["content"];
            string content1 = content.Substring(0, content.Length);
            string content2 = content.Substring(content.Length);
            if (book != null)
            {
                AddChapterRequest request = new AddChapterRequest
                {
                    bookId = BookId,
                    ChapterName = Chapter.ChapterName,
                    Contents1 = content1,
                    Contents2 = content2,
                    NumberChapter = Chapter.NumberChapter,
                };
                var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                await _httpClient.PostAsync("api/chapters", jsonContent);
                if (!categoriesResponse.IsSuccessStatusCode)
                {
                    Message = "Lỗi khi tạo chương";
                    return Page();
                }
            }
            return RedirectToPage("/Book/ManageChapter");
        }
    }
}
