using System;
using System.ComponentModel.DataAnnotations;

namespace Praise.Commands
{
    public class AddUserCommand
    {
        [Required]
        [MinLength(3, ErrorMessage = "O campo 'Nome' deve ter no mínimo 3 caracteres.")]
        [MaxLength(100, ErrorMessage = "O campo 'Nome' deve ter no máximo 100 caracteres.")]
        public string Name { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "O campo 'Usuário' deve ter no mínimo 2 caracteres.")]
        [MaxLength(20, ErrorMessage = "O campo 'Usuário' deve ter no máximo 20 caracteres.")]
        public string Username { get; set; }
        //[Required]
        //[EmailAddress (ErrorMessage = "Email inválido.")]
        public string Email { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "O campo 'Senha' deve ter no mínimo 5 caracteres.")]
        [MaxLength(20, ErrorMessage = "O campo 'Senha' deve ter no máximo 20 caracteres.")]
        public string Password { get; set; }
        [Required]        
        [MaxLength(14, ErrorMessage = "O campo 'Usuário' deve ter no máximo 14 caracteres.")]
        public string Phone { get; set; }
        public DateTime? Birthday { get; set; }
        public string Photo { get; set; }
    }
}
