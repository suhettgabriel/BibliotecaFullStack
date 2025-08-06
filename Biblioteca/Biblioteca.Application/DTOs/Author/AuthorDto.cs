using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Application.DTOs.Author
{
    public class AuthorDto
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }
    }
}