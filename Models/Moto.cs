using System.ComponentModel.DataAnnotations;

namespace MottuApi.Models
{
    public class Moto
    {
        [Key]
        [Required(ErrorMessage = "A placa é obrigatória. Informe a placa da moto, que deve ter 7 caracteres.")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "A placa deve ter 7 caracteres.")]
        public string Placa { get; set; }  // Chave primária

        [Required(ErrorMessage = "O modelo da moto é obrigatório.")]
        [EnumDataType(typeof(ModeloMoto), ErrorMessage = "Modelo inválido. Os modelos válidos são: Mottu Sport, Mottu E ou Mottu Pop.")]
        public ModeloMoto Modelo { get; set; }

        [Required(ErrorMessage = "O status da moto é obrigatório.")]
        [EnumDataType(typeof(StatusMoto), ErrorMessage = "Status inválido. Os valores válidos são: 'Disponível', 'Alugada', ou 'Manutenção'.")]
        public StatusMoto Status { get; set; }

        [Required(ErrorMessage = "O setor da moto é obrigatório.")]
        [EnumDataType(typeof(SetorMoto), ErrorMessage = "Setor inválido. Os valores válidos são: 'Bom', 'Intermediário', ou 'Ruim'.")]
        public SetorMoto Setor { get; set; }

        [Required(ErrorMessage = "O nome do pátio é obrigatório.")]
        public string NomePatio { get; set; }  // Chave de relacionamento com Patio

        [Required(ErrorMessage = "O usuário do funcionário é obrigatório.")]
        public string UsuarioFuncionario { get; set; }  // Relacionamento com o funcionário

        public Funcionario Funcionario { get; set; }  // Relacionamento com o funcionário
        public Patio Patio { get; set; }  // Relacionamento com o pátio

        public Moto()
        {
            Placa = string.Empty;
            NomePatio = string.Empty;
            UsuarioFuncionario = string.Empty;
        }
    }

    public enum ModeloMoto
    {
        MottuSport,
        MottuE,
        MottuPop
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
