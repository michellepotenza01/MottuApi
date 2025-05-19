using System.ComponentModel.DataAnnotations;

namespace MottuApi.Models
{
    public class FuncionarioDTO
    {
        [Required(ErrorMessage = "O usuário é obrigatório. Informe o nome de usuário único para o funcionário.")]
        public string UsuarioFuncionario { get; set; }  // UsuarioFuncionario chave primária

        [Required(ErrorMessage = "O nome do funcionário é obrigatório. Informe o nome completo do funcionário.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória. Informe a senha segura do funcionário.")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O nome do pátio de trabalho é obrigatório. Informe o nome do pátio onde o funcionário trabalha.")]
        public string NomePatio { get; set; }  
}
