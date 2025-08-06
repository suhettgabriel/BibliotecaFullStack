using Biblioteca.Application.DTOs.Author;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            return authors.Select(a => new AuthorDto { Id = a.Id, Name = a.Name });
        }

        public async Task<AuthorDto> GetByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null) return null;
            return new AuthorDto { Id = author.Id, Name = author.Name };
        }

        public async Task<AuthorDto> AddAsync(CreateAuthorDto createAuthorDto)
        {
            var author = new Author { Name = createAuthorDto.Name };
            var createdAuthor = await _authorRepository.AddAsync(author);
            return new AuthorDto { Id = createdAuthor.Id, Name = createdAuthor.Name };
        }

        public async Task UpdateAsync(int id, UpdateAuthorDto updateAuthorDto)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author != null)
            {
                author.Name = updateAuthorDto.Name;
                await _authorRepository.UpdateAsync(author);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _authorRepository.DeleteAsync(id);
        }
    }
}