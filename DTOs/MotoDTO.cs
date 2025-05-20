using System.ComponentModel.DataAnnotations;

namespace MottuApi.Models
{
    public class MotoDTO
    {
        [Required(ErrorMessage = "A placa é obrigatória.")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "A placa deve ter 7 caracteres.")]
        public string Placa { get; set; }

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
        public string NomePatio { get; set; }

        [Required(ErrorMessage = "O usuário do funcionário é obrigatório.")]
        public string UsuarioFuncionario { get; set; }
    }
}
