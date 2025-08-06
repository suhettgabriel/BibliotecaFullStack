using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Application.DTOs.Genre
{
    public class CreateGenreDto
    {
        [Required(ErrorMessage = "O nome do gênero é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do gênero deve ter no máximo 100 caracteres.")]
        [Display(Name = "Nome")]
        public string Name { get; set; }
    }
}