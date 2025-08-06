using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Display(Name = "Título")]
        public string Title { get; set; }

        [Display(Name = "Ano de Publicação")]
        public int Year { get; set; }

        public int AuthorId { get; set; }
        public int GenreId { get; set; }

        public Author Author { get; set; }
        public Genre Genre { get; set; }
    }
}