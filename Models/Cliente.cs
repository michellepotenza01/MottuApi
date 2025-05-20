using System.ComponentModel.DataAnnotations;

namespace MottuApi.Models
{
    public class Cliente
    {
        [Key]
        [Required(ErrorMessage = "O usuário do cliente é obrigatório. Informe o nome de usuário único para o cliente.")]
        public string UsuarioCliente { get; set; }  // Chave primária

        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A senha do cliente é obrigatória.")]
        public string Senha { get; set; }

        public string? MotoPlaca { get; set; }  // Moto do cliente, agora pode ser nula se o cliente não tiver moto

        public Moto? Moto { get; set; }  // Relacionamento com a moto (opcional)

        public Cliente()
        {
            UsuarioCliente = string.Empty;
            Nome = string.Empty;
            Senha = string.Empty;
        }
    }
}
