using API.DTO.Request;
using API.Models;
using API.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpPost]
        public IActionResult CreateBook([FromBody] BookRequest bookRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = _bookRepository.CreateBook(bookRequest);
            if (book == null)
            {
                return StatusCode(500, "An error occurred while creating the book.");
            }

            return Ok("Tao book thanh cong: \n"+book);
        }
        [HttpPut("{bookId}")]
        public IActionResult UpdateBook(int bookId, [FromBody] BookRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

        

            var result = _bookRepository.UpdateBook(bookId, request);

            if (!result)
            {
                return NotFound("Book not found or you don't have permission to update this book.");
            }

            return Ok("Book updated successfully.");
        }
        [HttpDelete("{bookId}")]
        public IActionResult DeleteBook(int bookId)
        {
            //var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            //if (userId == 0)
            //{
            //    return Unauthorized("User is not authenticated.");
            //}

            var result = _bookRepository.DeleteBook(bookId);

            if (!result)
            {
                return NotFound("Book not found or you don't have permission to delete this book.");
            }

            return Ok("Book deleted successfully.");
        }


    }
}
