using System.ComponentModel.DataAnnotations;

namespace MottuApi.Models
{
    public class Funcionario
    {
        [Key]  
        [Required(ErrorMessage = "O usuário é obrigatório.")]
        public string UsuarioFuncionario { get; set; }  // UsuarioFuncionario é a chave primária

        [Required(ErrorMessage = "O nome do funcionário é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O pátio de trabalho é obrigatório.")]
        public int PatioId { get; set; }
        public Patio Patio { get; set; }
    }
}
