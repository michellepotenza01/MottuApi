using System.ComponentModel.DataAnnotations;

namespace MottuApi.Models
{
    public class PatioDTO
    {
        [Required(ErrorMessage = "O nome do pátio é obrigatório.")]
        public string NomePatio { get; set; }  // Chave primária, substituindo ID

        [Required(ErrorMessage = "A localização do pátio é obrigatória.")]
        public string Localizacao { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O número de vagas totais deve ser maior que zero.")]
        [Required(ErrorMessage = "O número total de vagas é obrigatório.")]
        public int VagasTotais { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O número de vagas ocupadas deve ser maior ou igual a zero.")]
        [Required(ErrorMessage = "O número de vagas ocupadas é obrigatório.")]
        public int VagasOcupadas { get; set; }
    }
}
