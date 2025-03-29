using API.DTO.Request;
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
        public IActionResult GetAllChapterByBookId([FromQuery] int bookId)
        {
            return Ok(_chapterRepository.GetAllChapterByBookId(bookId));
        }
        [HttpPut]
        public void EditChapter([FromBody] EditChapterRequest request)
        {
            _chapterRepository.EditChapter(request);
        }

        [HttpPost]
        public void AddChapter([FromBody] AddChapterRequest request)
        {
            _chapterRepository.AddChapter(request);
        }

        [HttpDelete("{id}")]
        public void DeleteChapter(int id)
        {
            _chapterRepository.DeleteChapter(id);
        }
    }


}
