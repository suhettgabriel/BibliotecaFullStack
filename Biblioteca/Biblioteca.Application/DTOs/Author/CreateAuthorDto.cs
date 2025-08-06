using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Application.DTOs.Author
{
    public class CreateAuthorDto
    {
        [Required(ErrorMessage = "O nome do autor é obrigatório.")]
        [StringLength(150, ErrorMessage = "O nome do autor deve ter no máximo 150 caracteres.")]
        [Display(Name = "Nome")]
        public string Name { get; set; }
    }
}