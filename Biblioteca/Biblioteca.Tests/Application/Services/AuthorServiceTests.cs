using Xunit;
using Moq;
using FluentAssertions;
using Biblioteca.Application.Services;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Biblioteca.Application.DTOs.Author;

namespace Biblioteca.Tests.Application.Services
{
    public class AuthorServiceTests
    {
        private readonly Mock<IAuthorRepository> _mockAuthorRepository;
        private readonly AuthorService _authorService;

        public AuthorServiceTests()
        {
            _mockAuthorRepository = new Mock<IAuthorRepository>();
            _authorService = new AuthorService(_mockAuthorRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfAuthorDto_WhenAuthorsExist()
        {
            var authors = new List<Author>
            {
                new Author { Id = 1, Name = "J.R.R. Tolkien" },
                new Author { Id = 2, Name = "George Orwell" }
            };
            _mockAuthorRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(authors);

            var result = await _authorService.GetAllAsync();

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeAssignableTo<IEnumerable<AuthorDto>>();
            result.First().Name.Should().Be("J.R.R. Tolkien");
            _mockAuthorRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnAuthorDto_WhenAuthorExists()
        {
            var author = new Author { Id = 1, Name = "J.R.R. Tolkien" };
            _mockAuthorRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(author);

            var result = await _authorService.GetByIdAsync(1);

            result.Should().NotBeNull();
            result.Should().BeOfType<AuthorDto>();
            result.Id.Should().Be(1);
            result.Name.Should().Be("J.R.R. Tolkien");
            _mockAuthorRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenAuthorDoesNotExist()
        {
            _mockAuthorRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Author)null);

            var result = await _authorService.GetByIdAsync(99);

            result.Should().BeNull();
        }

        [Fact]
        public async Task AddAsync_ShouldCallRepositoryAndReturnAuthorDto()
        {
            var createDto = new CreateAuthorDto { Name = "Machado de Assis" };
            var author = new Author { Id = 3, Name = "Machado de Assis" };
            _mockAuthorRepository.Setup(repo => repo.AddAsync(It.IsAny<Author>())).ReturnsAsync(author);

            var result = await _authorService.AddAsync(createDto);

            result.Should().NotBeNull();
            result.Name.Should().Be(createDto.Name);
            result.Id.Should().Be(author.Id);
            _mockAuthorRepository.Verify(repo => repo.AddAsync(It.Is<Author>(a => a.Name == createDto.Name)), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldCallRepository_WhenAuthorExists()
        {
            var updateDto = new UpdateAuthorDto { Name = "George R. R. Martin" };
            var existingAuthor = new Author { Id = 1, Name = "George Martin" };
            _mockAuthorRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingAuthor);

            await _authorService.UpdateAsync(1, updateDto);

            _mockAuthorRepository.Verify(repo => repo.UpdateAsync(It.Is<Author>(a => a.Id == 1 && a.Name == updateDto.Name)), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepository()
        {
            await _authorService.DeleteAsync(1);

            _mockAuthorRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }
    }
}