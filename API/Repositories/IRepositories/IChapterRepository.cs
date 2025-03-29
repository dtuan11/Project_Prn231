using API.DTO.Response;
using API.Models;

namespace API.Repositories.IRepositories
{
    public interface IChapterRepository
    {
       
        ChapterDetailResponse GetChapterDetails(int chapterId, int bookId, int? userId);
    }
}