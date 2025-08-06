using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Application.DTOs.Book
{
    public class BookDto
    {
        public int Id { get; set; }

        [Display(Name = "Título")]
        public string Title { get; set; }

        [Display(Name = "Ano de Publicação")]
        public int Year { get; set; }

        public int AuthorId { get; set; }

        [Display(Name = "Autor")]
        public string AuthorName { get; set; }

        public int GenreId { get; set; }

        [Display(Name = "Gênero")]
        public string GenreName { get; set; }
    }
}