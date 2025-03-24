using API.Models;

namespace API.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<(User User, List<Reading> Readings)> GetUserByIdAsync(int id);
        Task<User> GetUserById(int id);
        Task UpdateUserAsync(User user);
        Task<User> LoginAsync(string username, string password);
        Task<User> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
        Task<(List<string> Titles, List<int> ViewCounts)> GetBookViewStatisticsAsync();
        Task<(List<string> Usernames, List<int> ViewCounts)> GetUserBookViewStatisticsAsync();
    }
}