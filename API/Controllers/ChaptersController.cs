using API.DTO.Response;
using API.Models;
using API.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/chapters")]
    [ApiController]
    public class ChaptersController : ControllerBase
    {
        private readonly IChapterRepository _chapterRepository;

        public ChaptersController(IChapterRepository chapterRepository)
        {
            _chapterRepository = chapterRepository;
        }

        
        [HttpGet("{chapterId}")]
        public IActionResult GetChapterDetails(int chapterId, [FromQuery] int bookId, [FromQuery] int? userId)
        {
            var chapter = _chapterRepository.GetChapterDetails(chapterId, bookId, userId);
            if (chapter == null)
            {
                return NotFound();
            }
            return Ok(chapter);
        }
        [HttpGet("AllChapter")]
        public IActionResult GetAllChapterByBookId([FromQuery]int bookId)
        {
            return Ok(_chapterRepository.GetAllChapterByBookId(bookId));
        }
    }

    
}
