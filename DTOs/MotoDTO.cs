using System.ComponentModel.DataAnnotations;

public class MotoDto
{
    [Required(ErrorMessage = "A placa é obrigatória.")]
    [StringLength(7, ErrorMessage = "A placa deve ter 7 caracteres.")]
    public required string Placa { get; set; }

    [Required(ErrorMessage = "O modelo da moto é obrigatório.")]
    [StringLength(100)]
    public required string Modelo { get; set; }

    [Required(ErrorMessage = "O status da moto é obrigatório.")]
    [StringLength(100)]
    [RegularExpression("^(Disponível|Alugada|Manutenção)$", ErrorMessage = "Status inválido. Os valores válidos são: 'Disponível', 'Alugada', ou 'Manutenção'.")]
    public required string Status { get; set; }

    [Required(ErrorMessage = "O setor da moto é obrigatório.")]
    [StringLength(100)]
    [RegularExpression("^(Bom|Intermediário|Ruim)$", ErrorMessage = "Setor inválido. Os valores válidos são: 'Bom', 'Intermediário', ou 'Ruim'.")]
    public required string Setor { get; set; }

    [Required(ErrorMessage = "O nome do pátio é obrigatório.")]
    [StringLength(2000)]
    public required string NomePatio { get; set; }

    [Required(ErrorMessage = "O usuário do funcionário é obrigatório.")]
    [StringLength(2000)]
    public required string UsuarioFuncionario { get; set; }
}
