using System.ComponentModel.DataAnnotations;

namespace MottuApi.Models
{
    public class Cliente
    {
        [Key]
        [Required(ErrorMessage = "O usuário do cliente é obrigatório. Informe o nome de usuário único para o cliente.")]
        [StringLength(450)]  // Ajuste para garantir que a chave tenha o mesmo comprimento no banco
        public string UsuarioCliente { get; set; }  // Chave primária

        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        [StringLength(2000)]  // Ajuste para garantir que o campo tenha o mesmo comprimento no banco
        public string Nome { get; set; }

        [Required(ErrorMessage = "A senha do cliente é obrigatória.")]
        [StringLength(2000)]  // Ajuste para garantir que o campo tenha o mesmo comprimento no banco
        public string Senha { get; set; }

        public string? MotoPlaca { get; set; }  // Moto do cliente, agora pode ser nula se o cliente não tiver moto

        public Cliente()
        {
            UsuarioCliente = string.Empty;
            Nome = string.Empty;
            Senha = string.Empty;
        }
    }
}
