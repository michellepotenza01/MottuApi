using MottuApi.Data;
using MottuApi.Models;
using Microsoft.EntityFrameworkCore;
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
            return await _context.Funcionarios
                .FirstOrDefaultAsync(f => f.UsuarioFuncionario == usuarioFuncionario);
        }

        public async Task<string> AddFuncionarioAsync(Funcionario funcionario)
        {
            // Verificar se o pátio existe
            var patio = await _context.Patios
                .FirstOrDefaultAsync(p => p.NomePatio == funcionario.NomePatio);

            if (patio == null)
                return "Pátio não encontrado.";

            funcionario.Patio = patio;  // Associando o pátio ao funcionário
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();
            return "Funcionário criado com sucesso!";
        }

        public async Task<string> UpdateFuncionarioAsync(string usuarioFuncionario, Funcionario funcionario)
        {
            var funcionarioExistente = await _context.Funcionarios
                .FirstOrDefaultAsync(f => f.UsuarioFuncionario == usuarioFuncionario);

            if (funcionarioExistente == null)
                return "Funcionário não encontrado.";

            funcionarioExistente.Nome = funcionario.Nome;
            funcionarioExistente.Senha = funcionario.Senha;

            // Atualizar o pátio
            var patio = await _context.Patios
                .FirstOrDefaultAsync(p => p.NomePatio == funcionario.NomePatio);

            if (patio == null)
                return "Pátio não encontrado.";

            funcionarioExistente.Patio = patio;

            await _context.SaveChangesAsync();
            return "Funcionário atualizado com sucesso!";
        }

        public async Task<string> DeleteFuncionarioAsync(string usuarioFuncionario)
        {
            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(f => f.UsuarioFuncionario == usuarioFuncionario);

            if (funcionario == null)
                return "Funcionário não encontrado.";

            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
            return "Funcionário excluído com sucesso!";
        }
    }
}
