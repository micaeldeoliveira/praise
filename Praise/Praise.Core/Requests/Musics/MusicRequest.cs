using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Praise.Core.Requests.Musics;

public class MusicRequest
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "O campo {0} deve ter no mínimo {2} e no máximo {1} caracteres.")]
    [DisplayName("Título")]
    public required string Title { get; set; }

    [MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Lembrete")]
    public string? Reminder { get; set; }

    [MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Interprete")]
    public string? Singer { get; set; }

    [DisplayName("Letra")]
    public string? Lirycs { get; set; }

    [DisplayName("Vídeo")]
    public string? Video { get; set; }

    [DisplayName("Tocamos?")]
    public bool Play { get; set; }
}