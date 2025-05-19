using System.ComponentModel.DataAnnotations;

namespace MottuApi.Models
{
    public class Moto
    {
        [Key]
        [Required(ErrorMessage = "A placa é obrigatória.")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "A placa deve ter 7 caracteres.")]
        public string Placa { get; set; }  // Chave primária

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "A marca é obrigatória.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "O status da moto é obrigatório.")]
        [EnumDataType(typeof(StatusMoto), ErrorMessage = "Status inválido.")]
        public StatusMoto Status { get; set; }

        [Required(ErrorMessage = "O setor da moto é obrigatório.")]
        [EnumDataType(typeof(SetorMoto), ErrorMessage = "Setor inválido.")]
        public SetorMoto Setor { get; set; }

        [Required(ErrorMessage = "O pátio é obrigatório.")]
        public int NomePatio { get; set; }
        public Patio Patio { get; set; }

        [Required(ErrorMessage = "O usuário do funcionário é obrigatório.")]
        public string UsuarioFuncionario { get; set; }  

        public Funcionario Funcionario { get; set; }
    }

    public enum StatusMoto
    {
        Disponível,
        Alugada,
        Manutenção
    }

    public enum SetorMoto
    {
        Bom,
        Intermediário,
        Ruim
    }
}
