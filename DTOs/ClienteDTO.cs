using System.ComponentModel.DataAnnotations;

namespace MottuApi.Models
{
    public class ClienteDTO
    {
        [Required(ErrorMessage = "O usuário do cliente é obrigatório. Informe o nome de usuário único para o cliente.")]
        public string UsuarioCliente { get; set; }  // UsuarioCliente é a chave primária

        [Required(ErrorMessage = "O nome do cliente é obrigatório. Informe o nome completo do cliente.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A senha do cliente é obrigatória. A senha deve ser forte e segura.")]
        public string Senha { get; set; }

        [StringLength(7, MinimumLength = 7, ErrorMessage = "A placa deve ter 7 caracteres. A placa é composta por 7 caracteres alfanuméricos.")]
        public string MotoPlaca { get; set; }  
    }
}
