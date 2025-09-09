using System.ComponentModel.DataAnnotations;

namespace MottuApi.Models
{
    public class FuncionarioDto
    {
        [Required(ErrorMessage = "O usuário é obrigatório.")]
        [StringLength(450)]  
        public required string UsuarioFuncionario { get; set; }

        [Required(ErrorMessage = "O nome do funcionário é obrigatório.")]
        [StringLength(2000)]  
        public required string Nome { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(2000)]  
        public required string Senha { get; set; }

        [Required(ErrorMessage = "O nome do pátio de trabalho é obrigatório.")]
        [StringLength(2000)]  
        public required string NomePatio { get; set; }
    }
}
