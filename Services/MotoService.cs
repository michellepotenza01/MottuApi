using MottuApi.Data;
using MottuApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MottuApi.Services
{
    public class MotoService
    {
        private const string Disponivel = "Disponível";
        private const string Alugada = "Alugada";
        private const string Manutencao = "Manutenção";

        private readonly MottuDbContext _context;

        public MotoService(MottuDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Moto>> GetMotosAsync(string status = null, string setor = null)
        {
            var motos = _context.Motos.AsQueryable();

            if (!string.IsNullOrEmpty(status))
                motos = motos.Where(m => m.Status == status);

            if (!string.IsNullOrEmpty(setor))
                motos = motos.Where(m => m.Setor == setor);

            return motos;
        }

        public async Task<Moto> GetMotoAsync(string placa)
        {
            return await _context.Motos
                .FirstOrDefaultAsync(m => m.Placa == placa);
        }

        public async Task<string> AddMotoAsync(Moto moto)
        {
            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(f => f.UsuarioFuncionario == moto.UsuarioFuncionario);

            if (funcionario == null)
                return "Funcionário não encontrado.";

            var patio = await _context.Patios
                .FirstOrDefaultAsync(p => p.NomePatio == moto.NomePatio);

            if (patio == null)
                return "Pátio não encontrado.";

            if (patio.VagasOcupadas >= patio.VagasTotais)
                return "Não há vagas disponíveis no pátio.";

            _context.Motos.Add(moto);

            if (moto.Status == Disponivel || moto.Status == Manutencao)
                patio.VagasOcupadas++;

            await _context.SaveChangesAsync();
            return "Moto criada com sucesso!";
        }

        public async Task<string> UpdateMotoAsync(string placa, Moto moto)
        {
            var motoExistente = await _context.Motos
                .Include(m => m.Patio)
                .FirstOrDefaultAsync(m => m.Placa == placa);

            if (motoExistente == null)
                return "Moto não encontrada.";

            var patio = motoExistente.Patio;

            if (moto.Status != motoExistente.Status)
            {
                // Merge: Merging both if statements into one
                if (moto.Status == Alugada && motoExistente.Status == Disponivel)
                {
                    patio.VagasOcupadas--;
                }
                else if ((moto.Status == Disponivel || moto.Status == Manutencao) && motoExistente.Status == Alugada)
                {
                    patio.VagasOcupadas++;
                }
            }

            motoExistente.Modelo = moto.Modelo;
            motoExistente.Status = moto.Status;
            motoExistente.Setor = moto.Setor;

            await _context.SaveChangesAsync();
            return "Moto atualizada com sucesso!";
        }

        public async Task<string> DeleteMotoAsync(string placa)
        {
            var moto = await _context.Motos
                .Include(m => m.Patio)
                .FirstOrDefaultAsync(m => m.Placa == placa);

            if (moto == null)
                return "Moto não encontrada.";

            var patio = moto.Patio;

            if (patio != null && (moto.Status == Disponivel || moto.Status == Manutencao))
                patio.VagasOcupadas--;

            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();

            return "Moto removida com sucesso!";
        }
    }
}
