using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MottuApi.Models
{
    public class Patio
    {
        [Key]
        [Required(ErrorMessage = "O nome do pátio é obrigatório.")]
        public string NomePatio { get; set; }  // Chave primária

        [Required(ErrorMessage = "A localização do pátio é obrigatória.")]
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
