using System.ComponentModel.DataAnnotations;

namespace MottuApi.Models
{
    public class MotoDTO
    {
        [Required(ErrorMessage = "A placa é obrigatória. Informe a placa da moto, que deve ter 7 caracteres.")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "A placa deve ter 7 caracteres. A placa é composta por 7 caracteres alfanuméricos.")]
        public string Placa { get; set; }  // Chave primária

        [Required(ErrorMessage = "O modelo é obrigatório. Informe o modelo da moto.")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "A marca é obrigatória. Informe a marca da moto.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "O status da moto é obrigatório. Informe o status atual da moto (Disponível, Alugada, Manutenção).")]
        [EnumDataType(typeof(StatusMoto), ErrorMessage = "Status inválido. Os valores válidos são: 'Disponível', 'Alugada', ou 'Manutenção'.")]
        public StatusMoto Status { get; set; }

        [Required(ErrorMessage = "O setor da moto é obrigatório. Informe o setor da moto (Bom, Intermediário, Ruim).")]
        [EnumDataType(typeof(SetorMoto), ErrorMessage = "Setor inválido. Os valores válidos são: 'Bom', 'Intermediário', ou 'Ruim'.")]
        public SetorMoto Setor { get; set; }

        [Required(ErrorMessage = "O nome do pátio é obrigatório. Informe o nome do pátio onde a moto está estacionada.")]
        public string NomePatio { get; set; }  

        [Required(ErrorMessage = "O usuário do funcionário é obrigatório. Informe o nome de usuário do funcionário responsável pela moto.")]
        public string UsuarioFuncionario { get; set; }  
    }
}
