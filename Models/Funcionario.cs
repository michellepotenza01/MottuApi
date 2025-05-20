using System.ComponentModel.DataAnnotations;

namespace MottuApi.Models
{
    public class Funcionario
    {
        [Key]
        [Required(ErrorMessage = "O usuário do funcionário é obrigatório. Informe o nome de usuário único para o funcionário.")]
        [StringLength(450)]  // Ajuste para garantir que a chave tenha o mesmo comprimento no banco
        public string UsuarioFuncionario { get; set; }  // Chave primária

        [Required(ErrorMessage = "O nome do funcionário é obrigatório.")]
        [StringLength(2000)]  // Ajuste para garantir que o campo tenha o mesmo comprimento no banco
        public string Nome { get; set; }

        [Required(ErrorMessage = "A senha do funcionário é obrigatória.")]
        [StringLength(2000)]  // Ajuste para garantir que o campo tenha o mesmo comprimento no banco
        public string Senha { get; set; }

        // Relacionamento com Patio
        [Required(ErrorMessage = "O nome do pátio é obrigatório.")]
        public string NomePatio { get; set; }  // Chave de relacionamento com Patio (continua sendo string por enquanto)

        // Relacionamento com a classe Patio
        public Patio Patio { get; set; }  // Relacionamento com o pátio associado ao funcionário

        public Funcionario()
        {
            UsuarioFuncionario = string.Empty;
            Nome = string.Empty;
            Senha = string.Empty;
            NomePatio = string.Empty;
        }
    }
}
