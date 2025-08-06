using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Application.DTOs.Genre
{
    public class GenreDto
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }
    }
}