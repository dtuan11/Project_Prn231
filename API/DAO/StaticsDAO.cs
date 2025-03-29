using API.DAO.IDAO;
using API.DTO.Response;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DAO
{
    public class StaticsDAO : IStaticsDAO
    {
        private readonly PRN221_Project_1Context context;

        public StaticsDAO(PRN221_Project_1Context context)
        {
            this.context = context;
        }

        public List<User> GetUserBookStatics()
        {
            return context.Users.Include(x=>x.Books).Where(user => user.Books.Any(b => b.Views.HasValue && b.Views > 0)).ToList();
        }

        public List<Book> GetViewStatics()
        {
            return context.Books.Where(b => b.Views.HasValue && b.Views != 0).ToList();
        }
    }
}
