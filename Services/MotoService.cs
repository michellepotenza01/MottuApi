using MottuApi.Data;
using MottuApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MottuApi.Services
{
    public class MotoService
    {
        private readonly MottuDbContext _context;

        public MotoService(MottuDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Moto>> GetMotosAsync(string status, string setor)
        {
            var query = _context.Motos.AsQueryable();

            if (!string.IsNullOrEmpty(status) && Enum.TryParse<StatusMoto>(status, true, out var statusEnum))
                query = query.Where(m => m.Status == statusEnum);

            if (!string.IsNullOrEmpty(setor) && Enum.TryParse<SetorMoto>(setor, true, out var setorEnum))
                query = query.Where(m => m.Setor == setorEnum);

            return await Task.FromResult(query);
        }

        public async Task<Moto> GetMotoAsync(string placa)
        {
            return await _context.Motos.FindAsync(placa);
        }

        public async Task<string> AddMotoAsync(Moto moto)
        {
            // Verificar se o UsuarioFuncionario existe no banco 
            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(f => f.UsuarioFuncionario == moto.UsuarioFuncionario);

            if (funcionario == null)
            {
                return "Usuário do funcionário inválido. Certifique-se de que o usuário existe no banco de dados.";
            }

            // Verificar se há vagas disponíveis no pátio
            var patio = await _context.Patios.FindAsync(moto.NomePatio);
            if (patio == null)
                return "Pátio não encontrado.";

            if (patio.VagasOcupadas >= patio.VagasTotais)
                return "Não há vagas disponíveis no pátio.";

            _context.Motos.Add(moto);
            patio.VagasOcupadas++;  // Se a moto for adicionada, uma vaga será ocupada.
            await _context.SaveChangesAsync();

            return "Moto criada com sucesso!";
        }

        public async Task<string> UpdateMotoStatusAsync(string placa, StatusMoto novoStatus)
        {
            var moto = await _context.Motos.FindAsync(placa);
            if (moto == null)
                return "Moto não encontrada.";

            var patio = await _context.Patios.FindAsync(moto.NomePatio);
            if (patio == null)
                return "Pátio não encontrado.";

            // Lógica para gerenciar as vagas do pátio ao alterar o status da moto
            if (moto.Status != novoStatus)  // Verifica se o status mudou
            {
                // Se a moto mudar de "Alugada" para "Disponível" -> liberar uma vaga
                if (moto.Status == StatusMoto.Alugada && novoStatus == StatusMoto.Disponível)
                {
                    patio.VagasOcupadas--;
                }
                // Se a moto mudar de "Disponível" para "Alugada" -> ocupar uma vaga
                else if (moto.Status == StatusMoto.Disponível && novoStatus == StatusMoto.Alugada)
                {
                    patio.VagasOcupadas++;
                }
            }

            moto.Status = novoStatus;
            await _context.SaveChangesAsync();
            return "Status da moto alterado com sucesso!";
        }

        public async Task<string> DeleteMotoAsync(string placa)
        {
            var moto = await _context.Motos.FindAsync(placa);
            if (moto == null)
                return "Moto não encontrada.";

            var patio = await _context.Patios.FindAsync(moto.NomePatio);
            if (patio != null)
            {
                patio.VagasOcupadas--;  // Libera uma vaga no pátio ao excluir a moto
                await _context.SaveChangesAsync();
            }

            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();

            return "Moto excluída com sucesso!";
        }
    }
}
