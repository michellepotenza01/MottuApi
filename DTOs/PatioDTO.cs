using System.ComponentModel.DataAnnotations;

namespace MottuApi.Models
{
    public class PatioDTO
    {
        [Required(ErrorMessage = "O nome do pátio é obrigatório. Informe o nome único do pátio.")]
        public string NomePatio { get; set; }  // Nome do pátio é a chave primária

        [Required(ErrorMessage = "A localização do pátio é obrigatória. Informe o endereço ou a localização do pátio.")]
        public string Localizacao { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O número de vagas totais deve ser maior que zero. Informe o número total de vagas disponíveis no pátio.")]
        [Required(ErrorMessage = "O número total de vagas é obrigatório. Informe a quantidade total de vagas no pátio.")]
        public int VagasTotais { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O número de vagas ocupadas deve ser maior ou igual a zero. Informe o número de vagas já ocupadas no pátio.")]
        [Required(ErrorMessage = "O número de vagas ocupadas é obrigatório. Informe o número de vagas que já estão ocupadas.")]
        public int VagasOcupadas { get; set; }
    }
}
