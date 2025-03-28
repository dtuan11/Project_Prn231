using API.DTO.Response;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Chapter
{
    public class ChapterListModel : PageModel
    {

        private readonly HttpClient _httpClient;
        public ChapterListModel( HttpClient httpClient)
        {
            
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7186");
        }
        public string? UserId { get; set; } = default!;
        public List<CategoryResponse> Categories { get; set; } = new List<CategoryResponse>();

        public List<ChapterDetailResponse> Chapters { get; set; } = new List<ChapterDetailResponse>();
        public Dictionary<int,int> CountWords { get; set; } = new Dictionary<int, int>();
        public User user { get; set; }

        public async Task OnGet(int id)
        {
            var categoriesResponse = await _httpClient.GetAsync("api/categories");
            if (categoriesResponse.IsSuccessStatusCode)
            {
                Categories = await categoriesResponse.Content.ReadFromJsonAsync<List<CategoryResponse>>();
            }
            var chaptersResponse = await _httpClient.GetAsync("api/chapters/AllChapter?bookId="+id);
            if (chaptersResponse.IsSuccessStatusCode)
            {
                Chapters = await chaptersResponse.Content.ReadFromJsonAsync<List<ChapterDetailResponse>>();
            }




            
            UserId = HttpContext.Session.GetString("userId");
            //Chapters = context.Chapters.Where(x => x.BookId == id).ToList();
            var userResponse = await _httpClient.GetAsync("userDetail/"+UserId);
            if (userResponse.IsSuccessStatusCode)
            {
                user = await userResponse.Content.ReadFromJsonAsync<User>();
            }

            foreach (var chapter in Chapters)
            {
                if(chapter.Contents1 !=null && chapter.Contents2 != null)
                {
                    var count = chapter.Contents1.Length + chapter.Contents2.Length;
                    CountWords.Add(chapter.ChapterId, count);
                    
                }
                else
                {
                    CountWords.Add(chapter.ChapterId,0);
                }
            }

        }
    }
}
