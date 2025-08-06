using Biblioteca.Application.DTOs.Author;
using Biblioteca.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAll()
        {
            var authors = await _authorService.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetById(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDto>> Create(CreateAuthorDto createAuthorDto)
        {
            var createdAuthor = await _authorService.AddAsync(createAuthorDto);
            return CreatedAtAction(nameof(GetById), new { id = createdAuthor.Id }, createdAuthor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateAuthorDto updateAuthorDto)
        {
            await _authorService.UpdateAsync(id, updateAuthorDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.DeleteAsync(id);
            return NoContent();
        }
    }
}