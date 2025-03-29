using API.DAO.IDAO;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DAO
{
    public class ProfileDao : IProfileDao
    {
        private readonly PRN221_Project_1Context _context;

        public ProfileDao(PRN221_Project_1Context context)
        {
            _context = context;
        }
        public async Task<User> GetAccountByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Reading>> GetReadingsByAccountIdAsync(int accountId)
        {
            return await _context.Readings
                .Include(r => r.Book)
                .Include(r => r.Chapter)
                .Where(r => r.UserId == accountId)
                .ToListAsync();
        }

        public async Task UpdateAccountAsync(User account)
        {
            _context.Users.Update(account);
            await _context.SaveChangesAsync();
        }
    }
}
