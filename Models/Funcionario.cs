using MottuApi.Models;
using System.ComponentModel.DataAnnotations;

public class Funcionario
{
    [Key]
    [Required(ErrorMessage = "O usuário do funcionário é obrigatório. Informe o nome de usuário único para o funcionário.")]
    public string UsuarioFuncionario { get; set; }  // Chave primária

    [Required(ErrorMessage = "O nome do funcionário é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "A senha do funcionário é obrigatória.")]
    public string Senha { get; set; }

    [Required(ErrorMessage = "O nome do pátio é obrigatório.")]  // Nome do Pátio associado
    public string NomePatio { get; set; }

    public Patio? Patio { get; set; }  // Relacionamento com o Pátio

    public Funcionario()
    {
        UsuarioFuncionario = string.Empty;
        Nome = string.Empty;
        Senha = string.Empty;
        NomePatio = string.Empty;
    }
}
