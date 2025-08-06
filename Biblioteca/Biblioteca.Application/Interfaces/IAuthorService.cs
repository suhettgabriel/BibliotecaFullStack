using Biblioteca.Application.DTOs.Author;

namespace Biblioteca.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAsync();
        Task<AuthorDto> GetByIdAsync(int id);
        Task<AuthorDto> AddAsync(CreateAuthorDto createAuthorDto);
        Task UpdateAsync(int id, UpdateAuthorDto updateAuthorDto);
        Task DeleteAsync(int id);
    }
}