using API.DAO.IDAO;
using API.DTO.Response;
using API.Models;
using API.Repositories.IRepositories;

namespace API.Repositories
{
    public class StaticsRepository : IStaticsRepository
    {
        private readonly IStaticsDAO _staticsDao;
        private readonly PRN221_Project_1Context context;

        public StaticsRepository(IStaticsDAO staticsDao, PRN221_Project_1Context context)
        {
            _staticsDao = staticsDao;
            this.context = context;
        }

        public List<UserBookViewResponse> GetUserBookViews()
        {
            var result = _staticsDao.GetUserBookStatics()
                        .Select(user => new UserBookViewResponse
                        {
                            Username = user.UserName,
                            TotalCount = user.Books.Where(b => b.Views.HasValue && b.Views > 0).Sum(b => b.Views.Value)
                        })
                        .ToList();

            return result;
        }

        public List<ViewStaticsResponse> GetViewStatics()
        {
            var result = _staticsDao.GetViewStatics();
            return result.Select(b => new ViewStaticsResponse
            {
                Title = b.Title,
                Views = b.Views.Value
            })
           .ToList();
        }
    }
}
