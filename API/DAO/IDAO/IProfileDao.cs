using API.Models;

namespace API.DAO.IDAO
{
    public interface IProfileDao
    {
        Task<User> GetAccountByIdAsync(int id);
        Task<List<Category>> GetCategoriesAsync();
        Task<List<Reading>> GetReadingsByAccountIdAsync(int accountId);
        Task UpdateAccountAsync(User account);
    }
}
