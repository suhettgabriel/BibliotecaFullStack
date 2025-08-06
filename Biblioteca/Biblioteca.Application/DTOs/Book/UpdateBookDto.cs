using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Application.DTOs.Book
{
    public class UpdateBookDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(200, ErrorMessage = "O título deve ter no máximo 200 caracteres.")]
        [Display(Name = "Título")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O ano de publicação é obrigatório.")]
        [Range(1, 9999, ErrorMessage = "Ano inválido.")]
        [Display(Name = "Ano de Publicação")]
        public int Year { get; set; }

        [Required(ErrorMessage = "O autor é obrigatório.")]
        [Display(Name = "Autor")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "O gênero é obrigatório.")]
        [Display(Name = "Gênero")]
        public int GenreId { get; set; }
    }
}