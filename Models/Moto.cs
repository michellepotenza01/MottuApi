using System.ComponentModel.DataAnnotations;

namespace MottuApi.Models
{
    public class Moto
    {
        [Key]
        [Required(ErrorMessage = "A placa é obrigatória.")]
        [StringLength(7)]  // Ajuste para garantir que o campo tenha o mesmo comprimento no banco
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
        [StringLength(2000)]  // Ajuste para garantir que a chave de relacionamento tenha o mesmo comprimento no banco
        public string NomePatio { get; set; }

        [Required(ErrorMessage = "O usuário do funcionário é obrigatório.")]
        [StringLength(2000)]  // Ajuste para garantir que o campo tenha o mesmo comprimento no banco
        public string UsuarioFuncionario { get; set; }

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
