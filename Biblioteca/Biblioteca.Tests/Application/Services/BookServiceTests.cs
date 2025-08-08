using Xunit;
using Moq;
using FluentAssertions;
using Biblioteca.Application.Services;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Biblioteca.Application.DTOs.Book;

namespace Biblioteca.Tests.Application.Services
{
    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> _mockBookRepository;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _mockBookRepository = new Mock<IBookRepository>();
            _bookService = new BookService(_mockBookRepository.Object);
        }

        private List<Book> GetSampleBooks()
        {
            var author1 = new Author { Id = 1, Name = "J.R.R. Tolkien" };
            var genre1 = new Genre { Id = 1, Name = "Fantasia" };
            var author2 = new Author { Id = 2, Name = "George Orwell" };
            var genre2 = new Genre { Id = 2, Name = "Distopia" };

            return new List<Book>
            {
                new Book { Id = 1, Title = "O Senhor dos Anéis", Year = 1954, AuthorId = 1, GenreId = 1, Author = author1, Genre = genre1 },
                new Book { Id = 2, Title = "1984", Year = 1949, AuthorId = 2, GenreId = 2, Author = author2, Genre = genre2 }
            };
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfBookDto_WhenBooksExist()
        {
            var books = GetSampleBooks();
            _mockBookRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(books);

            var result = await _bookService.GetAllAsync();

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeAssignableTo<IEnumerable<BookDto>>();
            var firstBook = result.First();
            firstBook.Title.Should().Be("O Senhor dos Anéis");
            firstBook.AuthorName.Should().Be("J.R.R. Tolkien");
            firstBook.GenreName.Should().Be("Fantasia");
            _mockBookRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnBookDto_WhenBookExists()
        {
            var book = GetSampleBooks().First();
            _mockBookRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(book);

            var result = await _bookService.GetByIdAsync(1);

            result.Should().NotBeNull();
            result.Should().BeOfType<BookDto>();
            result.Id.Should().Be(1);
            result.Title.Should().Be("O Senhor dos Anéis");
            _mockBookRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ShouldCallRepositoryAndReturnMappedBookDto()
        {
            var createDto = new CreateBookDto { Title = "O Hobbit", Year = 1937, AuthorId = 1, GenreId = 1 };
            var createdBook = new Book { Id = 3, Title = "O Hobbit", Year = 1937, AuthorId = 1, GenreId = 1 };

            var fullBookWithRelations = new Book
            {
                Id = 3,
                Title = "O Hobbit",
                Year = 1937,
                AuthorId = 1,
                GenreId = 1,
                Author = new Author { Id = 1, Name = "J.R.R. Tolkien" },
                Genre = new Genre { Id = 1, Name = "Fantasia" }
            };

            _mockBookRepository.Setup(repo => repo.AddAsync(It.IsAny<Book>())).ReturnsAsync(createdBook);
            _mockBookRepository.Setup(repo => repo.GetByIdAsync(createdBook.Id)).ReturnsAsync(fullBookWithRelations);

            var result = await _bookService.AddAsync(createDto);

            result.Should().NotBeNull();
            result.Id.Should().Be(3);
            result.Title.Should().Be(createDto.Title);
            result.AuthorName.Should().Be("J.R.R. Tolkien");
            result.GenreName.Should().Be("Fantasia");
            _mockBookRepository.Verify(repo => repo.AddAsync(It.Is<Book>(b => b.Title == createDto.Title)), Times.Once);
            _mockBookRepository.Verify(repo => repo.GetByIdAsync(createdBook.Id), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldCallRepository_WhenBookExists()
        {
            var updateDto = new UpdateBookDto { Title = "A Revolução dos Bichos", Year = 1945, AuthorId = 2, GenreId = 2 };
            var existingBook = GetSampleBooks().Last();
            _mockBookRepository.Setup(repo => repo.GetByIdAsync(2)).ReturnsAsync(existingBook);

            await _bookService.UpdateAsync(2, updateDto);

            _mockBookRepository.Verify(repo => repo.UpdateAsync(It.Is<Book>(b => b.Id == 2 && b.Title == updateDto.Title)), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepository()
        {
            await _bookService.DeleteAsync(1);

            _mockBookRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }
    }
}