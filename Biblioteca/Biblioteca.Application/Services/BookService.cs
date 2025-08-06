using Biblioteca.Application.DTOs.Book;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Year = b.Year,
                AuthorId = b.AuthorId,
                AuthorName = b.Author.Name,
                GenreId = b.GenreId,
                GenreName = b.Genre.Name
            });
        }

        public async Task<BookDto> GetByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return null;

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Year = book.Year,
                AuthorId = book.AuthorId,
                AuthorName = book.Author.Name,
                GenreId = book.GenreId,
                GenreName = book.Genre.Name
            };
        }

        public async Task<BookDto> AddAsync(CreateBookDto createBookDto)
        {
            var book = new Book
            {
                Title = createBookDto.Title,
                Year = createBookDto.Year,
                AuthorId = createBookDto.AuthorId,
                GenreId = createBookDto.GenreId
            };

            var createdBook = await _bookRepository.AddAsync(book);

            var bookWithRelations = await _bookRepository.GetByIdAsync(createdBook.Id);

            return new BookDto
            {
                Id = bookWithRelations.Id,
                Title = bookWithRelations.Title,
                Year = bookWithRelations.Year,
                AuthorId = bookWithRelations.AuthorId,
                AuthorName = bookWithRelations.Author.Name,
                GenreId = bookWithRelations.GenreId,
                GenreName = bookWithRelations.Genre.Name
            };
        }

        public async Task UpdateAsync(int id, UpdateBookDto updateBookDto)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book != null)
            {
                book.Title = updateBookDto.Title;
                book.Year = updateBookDto.Year;
                book.AuthorId = updateBookDto.AuthorId;
                book.GenreId = updateBookDto.GenreId;
                await _bookRepository.UpdateAsync(book);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _bookRepository.DeleteAsync(id);
        }
    }
}