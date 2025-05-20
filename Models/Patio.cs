using System.ComponentModel.DataAnnotations;

namespace MottuApi.Models
{
    public class Patio
    {
        [Key]
        [Required(ErrorMessage = "O nome do pátio é obrigatório.")]
        [StringLength(450)]  // Ajuste para garantir que a chave de relacionamento tenha o mesmo comprimento no banco
        public string NomePatio { get; set; }

        [Required(ErrorMessage = "A localização do pátio é obrigatória.")]
        [StringLength(2000)]  // Ajuste para garantir que o campo tenha o mesmo comprimento no banco
        public string Localizacao { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O número de vagas totais deve ser maior que zero.")]
        [Required(ErrorMessage = "O número total de vagas é obrigatório.")]
        public int VagasTotais { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O número de vagas ocupadas deve ser maior ou igual a zero.")]
        [Required(ErrorMessage = "O número de vagas ocupadas é obrigatório.")]
        public int VagasOcupadas { get; set; }

        public List<Moto> Motos { get; set; }  // Relacionamento com as motos

        public Patio()
        {
            NomePatio = string.Empty;
            Localizacao = string.Empty;
        }
    }
}
