using System.ComponentModel.DataAnnotations;

namespace Praise.Commands
{
    public class UpdateMusicCommand
    {
        [MinLength(3, ErrorMessage = "O campo 'Título' deve ter no mínimo 3 caracteres.")]
        [MaxLength(60, ErrorMessage = "O campo 'Título' deve ter no máximo 60 caracteres.")]
        public string Title { get; set; }
        [MinLength(3, ErrorMessage = "O campo 'Lembrete' deve ter no mínimo 3 caracteres.")]
        [MaxLength(60, ErrorMessage = "O campo 'Lembrete' deve ter no máximo 60 caracteres.")]
        public string Reminder { get; set; }
        [MaxLength(60, ErrorMessage = "O campo 'Interprete' deve ter no máximo 60 caracteres.")]
        public string Singer { get; set; }
        public string Lirycs { get; set; }
        public string Notation { get; set; }
        public string Video { get; set; }
        public bool Play { get; set; }
    }
}
