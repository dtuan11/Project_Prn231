using API.DAO.IDAO;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DAO
{
    public class CategoryDAO: ICategoryDao
    {
        private readonly PRN221_Project_1Context _context;

        public CategoryDAO(PRN221_Project_1Context context)
        {
            _context = context;
        }
        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
    }
}
