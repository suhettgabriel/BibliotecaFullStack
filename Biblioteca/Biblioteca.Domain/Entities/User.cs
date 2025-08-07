using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "Nome de Usuário")]
        public string Username { get; set; }

        public string PasswordHash { get; set; }

        [Display(Name = "Perfil")]
        public string Role { get; set; }
    }
}