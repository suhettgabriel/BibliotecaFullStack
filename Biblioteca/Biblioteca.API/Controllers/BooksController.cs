using Biblioteca.Application.DTOs.Book;
using Biblioteca.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> Create(CreateBookDto createBookDto)
        {
            var createdBook = await _bookService.AddAsync(createBookDto);
            return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateBookDto updateBookDto)
        {
            await _bookService.UpdateAsync(id, updateBookDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteAsync(id);
            return NoContent();
        }
    }
}