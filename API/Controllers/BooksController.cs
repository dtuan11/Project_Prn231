using API.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;

        public BooksController(IBookRepository bookRepository, IUserRepository userRepository)
        {
            _bookRepository = bookRepository;
            _userRepository = userRepository;

        }
        [HttpGet("approved")]
        public IActionResult GetApprovedBooks()
        {
            var books = _bookRepository.GetApprovedBooks();
            return Ok(books);
        }

        [HttpGet("new")]
        public IActionResult GetNewBooks()
        {
            var newBooks = _bookRepository.GetNewBooksWithLatestChapters();
            return Ok(newBooks);
        }

        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            var categories = _bookRepository.GetCategories();
            return Ok(categories);
        }
        [HttpGet("category/{id}")]
        public IActionResult GetBooksByCategory(int id)
        {
            if (!_bookRepository.CategoryExists(id))
            {
                return NotFound();
            }

            var books = _bookRepository.GetBooksByCategory(id);
            return Ok(books);
        }
        [HttpGet("search")]
        public IActionResult SearchBooks([FromQuery] string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return BadRequest("Keyword is required");
            }

            var books = _bookRepository.SearchBooks(keyword);
            return Ok(books);
        }
        [HttpGet("{id}")]
        public IActionResult GetBookDetail(int id)
        {
            var book = _bookRepository.GetBookDetailById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
    }
}
