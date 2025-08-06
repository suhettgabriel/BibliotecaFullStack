using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Domain.Entities
{
    public class Genre
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }
    }
}