using API.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaticsController : ControllerBase
    {
        private readonly IStaticsRepository staticsRepository;

        public StaticsController(IStaticsRepository staticsRepository)
        {
            this.staticsRepository = staticsRepository;
        }
        [HttpGet("userbookview")]
        public IActionResult GetUserBookView()
        {
            return Ok(staticsRepository.GetUserBookViews());
        }
        [HttpGet("bookviews")]
        public IActionResult GetBookView()
        {
            return Ok(staticsRepository.GetViewStatics());
        }

    }
}
