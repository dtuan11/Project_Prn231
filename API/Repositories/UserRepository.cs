using API.Models;
using API.DAO;
using API.Repositories.IRepositories;

namespace API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDAO _userDAO;

        public UserRepository(UserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userDAO.GetAllUsersAsync();
        }
        public async Task<User> GetUserById(int id)
        {
            return await _userDAO.GetUserByIdAsync(id);
        }

        public async Task<(User User, List<Reading> Readings)> GetUserByIdAsync(int id)
        {
            var user = await _userDAO.GetUserByIdAsync(id);
            var readings = await _userDAO.GetUserReadingsAsync(id);
            return (user, readings);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userDAO.UpdateUserAsync(user);
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            return await _userDAO.GetUserByCredentialsAsync(username, password);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _userDAO.GetUserByUsernameAsync(username);
        }

        public async Task AddUserAsync(User user)
        {
            await _userDAO.AddUserAsync(user);
        }
        public async Task<(List<string> Titles, List<int> ViewCounts)> GetBookViewStatisticsAsync()
        {
            var books = await _userDAO.GetBookViewStatisticsAsync();
            var titles = books.Select(b => b.Title).ToList();
            var viewCounts = books.Select(b => b.Views).ToList();
            return (titles, viewCounts);
        }
        public async Task<(List<string> Usernames, List<int> ViewCounts)> GetUserBookViewStatisticsAsync()
        {
            var userBookViews = await _userDAO.GetUserBookViewStatisticsAsync();
            var usernames = userBookViews.Select(u => u.Username).ToList();
            var viewCounts = userBookViews.Select(u => u.TotalViews).ToList();
            return (usernames, viewCounts);
        }
    }
}