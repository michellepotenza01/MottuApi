using MottuApi.Models;
using System.ComponentModel.DataAnnotations;

namespace MottuApi.Models
{
    public class Moto
    {
        [Key]
        [Required(ErrorMessage = "A placa é obrigatória.")]
        [StringLength(7)]
        public string Placa { get; set; }  // Chave primária

        [Required(ErrorMessage = "O modelo da moto é obrigatório.")]
        [StringLength(100)]
        public string Modelo { get; set; }  // Modelo como string

        [Required(ErrorMessage = "O status da moto é obrigatório.")]
        [StringLength(100)]
        public string Status { get; set; }  // Status como string

        [Required(ErrorMessage = "O setor da moto é obrigatório.")]
        [StringLength(100)]
        public string Setor { get; set; }  // Setor como string

        [Required(ErrorMessage = "O nome do pátio é obrigatório.")]
        [StringLength(2000)]
        public string NomePatio { get; set; }

        [Required(ErrorMessage = "O usuário do funcionário é obrigatório.")]
        [StringLength(2000)]
        public string UsuarioFuncionario { get; set; }

        public Funcionario Funcionario { get; set; }
        public Patio Patio { get; set; }

        public Moto()
        {
            Placa = string.Empty;
            NomePatio = string.Empty;
            UsuarioFuncionario = string.Empty;

        }
    }
}
