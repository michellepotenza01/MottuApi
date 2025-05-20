using System.ComponentModel.DataAnnotations;

namespace MottuApi.Models
{
    public class FuncionarioDTO
    {
        [Required(ErrorMessage = "O usuário é obrigatório.")]
        public string UsuarioFuncionario { get; set; }

        [Required(ErrorMessage = "O nome do funcionário é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O nome do pátio de trabalho é obrigatório.")]
        public string NomePatio { get; set; }  // O pátio será associado automaticamente no cadastro
    }
}
