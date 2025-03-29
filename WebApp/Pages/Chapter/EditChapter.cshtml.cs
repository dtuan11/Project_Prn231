using API.DTO.Request;
using API.DTO.Response;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace WebApp.Pages.Chapter
{
    public class EditChapterModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public EditChapterModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7186");
        }

        [BindProperty]
        public ChapterDetailResponse Chapter { get; set; } = default!;
        public string Message { get; set; } = string.Empty;
        public async Task OnGet(int chapterId, int bookId)
        {
            var userId = HttpContext.Session.GetString("userId");
            var exist = await _httpClient.GetAsync("api/chapters/"+chapterId+"?bookId="+bookId+"&userId="+userId);
            if (exist.IsSuccessStatusCode)
            {
                Chapter = await exist.Content.ReadFromJsonAsync<ChapterDetailResponse>();
            }
        }
        public async Task<IActionResult> OnPost()
        {
            try
            {
                string content = Request.Form["content"];
                string content1 = content.Substring(0, content.Length);
                string content2 = content.Substring(content.Length);
                Chapter.Contents1 = content1;
                Chapter.Contents2 = content2;
                EditChapterRequest request = new EditChapterRequest
                {
                    Contents1 = content1,
                    ChapterId = Chapter.ChapterId,
                    ChapterName = Chapter.ChapterName,
                    Contents2 = content2,
                    NumberChapter = Chapter.NumberChapter,
                    
                };
                var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
               var res = await _httpClient.PutAsync("api/chapters", jsonContent);
                if (res.IsSuccessStatusCode)
                {
                    Message = "Đã cập nhật chapter";
                    return Page();
                }
                else
                {
                    Message = "Có lỗi xảy ra khi cập nhật chapter";
                    return Page();
                }
            }
            catch (Exception)
            {
                Message = "Lỗi";
                return Page();
            }
        }
    }
}
