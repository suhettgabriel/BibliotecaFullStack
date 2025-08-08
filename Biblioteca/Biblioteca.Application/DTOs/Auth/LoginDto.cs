using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Application.DTOs.Auth
{
    public class LoginDto
    {
        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Password { get; set; }
    }
}