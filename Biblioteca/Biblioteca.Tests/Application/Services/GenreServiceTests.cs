using Xunit;
using Moq;
using FluentAssertions;
using Biblioteca.Application.Services;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Biblioteca.Application.DTOs.Genre;

namespace Biblioteca.Tests.Application.Services
{
    public class GenreServiceTests
    {
        private readonly Mock<IGenreRepository> _mockGenreRepository;
        private readonly GenreService _genreService;

        public GenreServiceTests()
        {
            _mockGenreRepository = new Mock<IGenreRepository>();
            _genreService = new GenreService(_mockGenreRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfGenreDto_WhenGenresExist()
        {
            var genres = new List<Genre>
            {
                new Genre { Id = 1, Name = "Ficção" },
                new Genre { Id = 2, Name = "Aventura" }
            };
            _mockGenreRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(genres);

            var result = await _genreService.GetAllAsync();

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeAssignableTo<IEnumerable<GenreDto>>();
            result.First().Name.Should().Be("Ficção");
            _mockGenreRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoGenresExist()
        {
            var genres = new List<Genre>();
            _mockGenreRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(genres);

            var result = await _genreService.GetAllAsync();

            result.Should().NotBeNull();
            result.Should().BeEmpty();
            _mockGenreRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnGenreDto_WhenGenreExists()
        {
            var genre = new Genre { Id = 1, Name = "Ficção" };
            _mockGenreRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(genre);

            var result = await _genreService.GetByIdAsync(1);

            result.Should().NotBeNull();
            result.Should().BeOfType<GenreDto>();
            result.Id.Should().Be(1);
            result.Name.Should().Be("Ficção");
            _mockGenreRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenGenreDoesNotExist()
        {
            _mockGenreRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Genre)null);

            var result = await _genreService.GetByIdAsync(99);

            result.Should().BeNull();
            _mockGenreRepository.Verify(repo => repo.GetByIdAsync(99), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ShouldCallRepositoryAndReturnGenreDto()
        {
            var createDto = new CreateGenreDto { Name = "Terror" };
            var genre = new Genre { Id = 1, Name = "Terror" };

            _mockGenreRepository.Setup(repo => repo.AddAsync(It.IsAny<Genre>())).ReturnsAsync(genre);

            var result = await _genreService.AddAsync(createDto);

            result.Should().NotBeNull();
            result.Should().BeOfType<GenreDto>();
            result.Name.Should().Be(createDto.Name);
            result.Id.Should().Be(genre.Id);
            _mockGenreRepository.Verify(repo => repo.AddAsync(It.Is<Genre>(g => g.Name == createDto.Name)), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldCallRepository_WhenGenreExists()
        {
            var updateDto = new UpdateGenreDto { Name = "Fantasia" };
            var existingGenre = new Genre { Id = 1, Name = "Ficção" };

            _mockGenreRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingGenre);
            _mockGenreRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Genre>())).Returns(Task.CompletedTask);

            await _genreService.UpdateAsync(1, updateDto);

            _mockGenreRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
            _mockGenreRepository.Verify(repo => repo.UpdateAsync(It.Is<Genre>(g => g.Id == 1 && g.Name == "Fantasia")), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldNotCallUpdate_WhenGenreDoesNotExist()
        {
            var updateDto = new UpdateGenreDto { Name = "Fantasia" };
            _mockGenreRepository.Setup(repo => repo.GetByIdAsync(99)).ReturnsAsync((Genre)null);

            await _genreService.UpdateAsync(99, updateDto);

            _mockGenreRepository.Verify(repo => repo.GetByIdAsync(99), Times.Once);
            _mockGenreRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Genre>()), Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepository()
        {
            _mockGenreRepository.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

            await _genreService.DeleteAsync(1);

            _mockGenreRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }
    }
}