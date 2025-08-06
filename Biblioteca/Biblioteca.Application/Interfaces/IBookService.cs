using Biblioteca.Application.DTOs.Book;

namespace Biblioteca.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<BookDto> GetByIdAsync(int id);
        Task<BookDto> AddAsync(CreateBookDto createBookDto);
        Task UpdateAsync(int id, UpdateBookDto updateBookDto);
        Task DeleteAsync(int id);
    }
}