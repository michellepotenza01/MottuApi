using System.ComponentModel.DataAnnotations;

namespace MottuApi.Models
{
    public class FuncionarioDTO
    {
        [Required(ErrorMessage = "O usuário é obrigatório.")]
        [StringLength(450)]  // Garantir que a chave tenha o mesmo comprimento no banco
        public string UsuarioFuncionario { get; set; }

        [Required(ErrorMessage = "O nome do funcionário é obrigatório.")]
        [StringLength(2000)]  // Garantir que o nome tenha o mesmo comprimento no banco
        public string Nome { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(2000)]  // Garantir que a senha tenha o mesmo comprimento no banco
        public string Senha { get; set; }

        [Required(ErrorMessage = "O nome do pátio de trabalho é obrigatório.")]
        [StringLength(2000)]  // Garantir que o nome do pátio tenha o mesmo comprimento no banco
        public string NomePatio { get; set; }
    }
}
