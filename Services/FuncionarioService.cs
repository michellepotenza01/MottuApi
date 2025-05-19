using MottuApi.Data;
using MottuApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MottuApi.Services
{
    public class FuncionarioService
    {
        private readonly MottuDbContext _context;

        public FuncionarioService(MottuDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Funcionario>> GetFuncionariosAsync()
        {
            return _context.Funcionarios.AsQueryable();
        }

        public async Task<Funcionario> GetFuncionarioAsync(string usuarioFuncionario)
        {
            return await _context.Funcionarios.FindAsync(usuarioFuncionario);
        }

        public async Task<string> AddFuncionarioAsync(Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();
            return "Funcionário criado com sucesso!";
        }

        public async Task<string> UpdateFuncionarioAsync(string usuarioFuncionario, Funcionario funcionario)
        {
            var funcionarioExistente = await _context.Funcionarios.FindAsync(usuarioFuncionario);
            if (funcionarioExistente == null)
                return "Funcionário não encontrado.";

            funcionarioExistente.Nome = funcionario.Nome;
            funcionarioExistente.Senha = funcionario.Senha;

            await _context.SaveChangesAsync();
            return "Funcionário atualizado com sucesso!";
        }

        public async Task<string> DeleteFuncionarioAsync(string usuarioFuncionario)
        {
            var funcionario = await _context.Funcionarios.FindAsync(usuarioFuncionario);
            if (funcionario == null)
                return "Funcionário não encontrado.";

            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
            return "Funcionário excluído com sucesso!";
        }
    }
}
