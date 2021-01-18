using Praise.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Praise.Commands
{
    public class UpdateEventCommand
    {
        [Required(ErrorMessage = "O nome do evento é obrigatório")]
        [MinLength(3, ErrorMessage = "O campo 'Nome' deve ter no mínimo 3 caracteres.")]
        [MaxLength(50, ErrorMessage = "O campo 'Nome' deve ter no máximo 50 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
        public string Date { get; set; }
                
        [MaxLength(100, ErrorMessage = "O campo 'Local' deve ter no máximo 100 caracteres.")]
        public string Local { get; set; }
        
        [MaxLength(1000, ErrorMessage = "O campo 'Observação' deve ter no máximo 1000 caracteres.")]
        public string Note { get; set; }
        [Required(ErrorMessage = "O status do evento é obrigatório.")]
        public EStatusEvent Status { get; set; }

        public string StartHour { get; set; }
        public string EndHour { get; set; }

        
    }
}
