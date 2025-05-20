using System.ComponentModel.DataAnnotations;

namespace MottuApi.Models
{
    public class ClienteDTO
    {
        [Required(ErrorMessage = "O usuário do cliente é obrigatório. Informe o nome de usuário único para o cliente.")]
        public string UsuarioCliente { get; set; }

        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A senha do cliente é obrigatória.")]
        public string Senha { get; set; }

        // Se o cliente tem uma moto, a placa da moto seria informada, mas pode ser nula
        public string? MotoPlaca { get; set; }  // A moto do cliente pode ser opcional, então é anulável

        // Construtor
        public ClienteDTO()
        {
            UsuarioCliente = string.Empty;
            Nome = string.Empty;
            Senha = string.Empty;
        }
    }
}
