using API.DAO;
using API.DAO.IDAO;
using API.DTO.Response;
using API.Repositories.IRepositories;

namespace API.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly ICategoryDao _categoryDao;

        public CategoryRepository(ICategoryDao categoryDao)
        {
            _categoryDao = categoryDao;
        }
        public List<CategoryResponse> GetCategories()
        {
            var categories = _categoryDao.GetCategories();
            return categories.Select(c => new CategoryResponse
            {
                CateId = c.CateId,
                Name = c.Name
            }).ToList();
        }
    }
}
