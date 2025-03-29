using API.DTO.Response;

namespace API.Repositories.IRepositories
{
    public interface ICategoryRepository
    {
        List<CategoryResponse> GetCategories();

    }
}
