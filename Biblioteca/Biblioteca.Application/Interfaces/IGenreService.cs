using Biblioteca.Application.DTOs.Genre;

namespace Biblioteca.Application.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetAllAsync();
        Task<GenreDto> GetByIdAsync(int id);
        Task<GenreDto> AddAsync(CreateGenreDto createGenreDto);
        Task UpdateAsync(int id, UpdateGenreDto updateGenreDto);
        Task DeleteAsync(int id);
    }
}