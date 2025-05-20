using System.ComponentModel.DataAnnotations;

namespace MottuApi.Models
{
    public class ClienteDTO
    {
        [Required(ErrorMessage = "O usuário do cliente é obrigatório. Informe o nome de usuário único para o cliente.")]
        [StringLength(450)]  // Garantir que a chave de relacionamento tenha o mesmo comprimento no banco
        public string UsuarioCliente { get; set; }

        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        [StringLength(2000)]  // Garantir que o nome tenha o mesmo comprimento no banco
        public string Nome { get; set; }

        [Required(ErrorMessage = "A senha do cliente é obrigatória.")]
        [StringLength(2000)]  // Garantir que a senha tenha o mesmo comprimento no banco
        public string Senha { get; set; }

        public string? MotoPlaca { get; set; }  // Moto do cliente, agora pode ser nula

        public ClienteDTO()
        {
            UsuarioCliente = string.Empty;
            Nome = string.Empty;
            Senha = string.Empty;
        }
    }
}
