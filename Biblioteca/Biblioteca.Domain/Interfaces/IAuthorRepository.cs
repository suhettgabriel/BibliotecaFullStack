using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(int id);
        Task<Author> AddAsync(Author author);
        Task UpdateAsync(Author author);
        Task DeleteAsync(int id);
    }
}