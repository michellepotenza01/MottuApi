using System.ComponentModel.DataAnnotations;

namespace MottuApi.Models
{
    public class PatioDTO
    {
        [Required(ErrorMessage = "O nome do pátio é obrigatório.")]
        [StringLength(450)]  // Garantir que o nome do pátio tenha o mesmo comprimento no banco
        public string NomePatio { get; set; }

        [Required(ErrorMessage = "A localização do pátio é obrigatória.")]
        [StringLength(2000)]  // Garantir que a localização tenha o mesmo comprimento no banco
        public string Localizacao { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O número de vagas totais deve ser maior que zero.")]
        [Required(ErrorMessage = "O número total de vagas é obrigatório.")]
        public int VagasTotais { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O número de vagas ocupadas deve ser maior ou igual a zero.")]
        [Required(ErrorMessage = "O número de vagas ocupadas é obrigatório.")]
        public int VagasOcupadas { get; set; }
    }
}
