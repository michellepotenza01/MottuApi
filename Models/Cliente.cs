using System.ComponentModel.DataAnnotations;

namespace MottuApi.Models
{
    public class Cliente
    {
        [Key]  
        [Required(ErrorMessage = "O usuário do cliente é obrigatório.")]
        public string UsuarioCliente { get; set; }  // UsuarioCliente - chave primária

        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A senha do cliente é obrigatória.")]
        public string Senha { get; set; }

        public Moto Moto { get; set; }
    }
}
