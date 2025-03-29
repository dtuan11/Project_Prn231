using API.DTO.Response;
using API.Models;

namespace API.DAO.IDAO
{
    public interface IStaticsDAO
    {
        public List<User> GetUserBookStatics();
        public List<Book> GetViewStatics();
    }
}
