using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.DAO
{
    public class UserDAO
    {
        private readonly PRN221_Project_1Context _context;

        public UserDAO(PRN221_Project_1Context context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(x => x.Role)
                .Include(x => x.Books)
                .Include(x => x.Rates)
                .Include(x => x.Responses)
                .ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<Reading>> GetUserReadingsAsync(int userId)
        {
            return await _context.Readings
                .Include(x => x.Book)
                .Include(x => x.Chapter)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByCredentialsAsync(string username, string password)
        {
            var hashedPassword = HashPassword(password);
            return await _context.Users
                .FirstOrDefaultAsync(x => x.UserName == username && x.Password == hashedPassword);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<BookViewStatistic>> GetBookViewStatisticsAsync()
        {
            return await _context.Books
        .Where(b => b.Views.HasValue && b.Views != 0)
        .Select(b => new BookViewStatistic
        {
            Title = b.Title,
            Views = b.Views.Value
        })
        .ToListAsync();
        }

        public async Task<List<UserBookViewStatistic>> GetUserBookViewStatisticsAsync()
        {
            return await _context.Users
         .Where(user => user.Books.Any(b => b.Views.HasValue && b.Views > 0))
         .Select(user => new UserBookViewStatistic
         {
             Username = user.UserName,
             TotalViews = user.Books.Where(b => b.Views.HasValue && b.Views > 0).Sum(b => b.Views.Value)
         })
         .ToListAsync();
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public class BookViewStatistic
        {
            public string Title { get; set; }
            public int Views { get; set; }
        }
        public class UserBookViewStatistic
        {
            public string Username { get; set; }
            public int TotalViews { get; set; }
        }
    }
}