using API.DTO.Request;
using API.DTO.Response;
using API.Models;

namespace API.Repositories.IRepositories
{
    public interface IChapterRepository
    {
       
        ChapterDetailResponse GetChapterDetails(int chapterId, int bookId, int? userId);

        List<ChapterDetailResponse> GetAllChapterByBookId(int bookId);
        
        void EditChapter(EditChapterRequest request);
        void AddChapter(AddChapterRequest chapter);
        void DeleteChapter(int chapterId);
    }
}