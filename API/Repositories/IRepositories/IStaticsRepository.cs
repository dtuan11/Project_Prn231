using API.DTO.Response;

namespace API.Repositories.IRepositories
{
    public interface IStaticsRepository
    {
        public List<UserBookViewResponse> GetUserBookViews();
        public List<ViewStaticsResponse> GetViewStatics();
    }
}
