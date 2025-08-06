using Biblioteca.Application.DTOs.Genre;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IEnumerable<GenreDto>> GetAllAsync()
        {
            var genres = await _genreRepository.GetAllAsync();
            return genres.Select(g => new GenreDto { Id = g.Id, Name = g.Name });
        }

        public async Task<GenreDto> GetByIdAsync(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null) return null;
            return new GenreDto { Id = genre.Id, Name = genre.Name };
        }

        public async Task<GenreDto> AddAsync(CreateGenreDto createGenreDto)
        {
            var genre = new Genre { Name = createGenreDto.Name };
            var createdGenre = await _genreRepository.AddAsync(genre);
            return new GenreDto { Id = createdGenre.Id, Name = createdGenre.Name };
        }

        public async Task UpdateAsync(int id, UpdateGenreDto updateGenreDto)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre != null)
            {
                genre.Name = updateGenreDto.Name;
                await _genreRepository.UpdateAsync(genre);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _genreRepository.DeleteAsync(id);
        }
    }
}