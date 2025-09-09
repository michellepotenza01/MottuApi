using System.ComponentModel.DataAnnotations;

namespace MottuApi.Models
{
    public class ClienteDto
    {
        [Required(ErrorMessage = "O usuário do cliente é obrigatório. Informe o nome de usuário único para o cliente.")]
        [StringLength(450)]  
        public string UsuarioCliente { get; set; }

        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        [StringLength(2000)]  
        public string Nome { get; set; }

        [Required(ErrorMessage = "A senha do cliente é obrigatória.")]
        [StringLength(2000)] 
        public string Senha { get; set; }

        public string? MotoPlaca { get; set; }  

        public ClienteDto()
        {
            UsuarioCliente = string.Empty;
            Nome = string.Empty;
            Senha = string.Empty;
        }
    }
}
