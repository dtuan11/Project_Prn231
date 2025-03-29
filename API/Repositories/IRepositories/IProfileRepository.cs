using API.DTO.Request;
using API.DTO.Response;
using API.Models;

namespace API.Repositories.IRepositories
{
    public interface IProfileRepository
    {
        Task<ProfileResponse> GetProfileAsync(int id);
        Task<List<Category>> GetCategoriesAsync();
        Task<List<ReadingResponse>> GetAccountReadingsAsync(int accountId);
        Task<ProfileResponse> UpdateProfileAsync(ProfileRequest request);
    }
}
