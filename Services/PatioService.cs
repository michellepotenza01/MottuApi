using MottuApi.Data;
using MottuApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MottuApi.Services
{
    public class PatioService
    {
        private readonly MottuDbContext _context;

        public PatioService(MottuDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Patio>> GetPatiosAsync()
        {
            return _context.Patios.AsQueryable();
        }

        public async Task<Patio> GetPatioAsync(string nomePatio)
        {
            return await _context.Patios
                .FirstOrDefaultAsync(p => p.NomePatio == nomePatio);
        }

        public async Task<string> AddPatioAsync(Patio patio)
        {
            if (patio.VagasOcupadas > patio.VagasTotais)
                return "O número de vagas ocupadas não pode ser maior que o número total de vagas.";

            _context.Patios.Add(patio);
            await _context.SaveChangesAsync();

            return "Pátio criado com sucesso!";
        }

        public async Task<string> UpdatePatioAsync(string nomePatio, Patio patio)
        {
            var patioExistente = await _context.Patios
                .FirstOrDefaultAsync(p => p.NomePatio == nomePatio);

            if (patioExistente == null)
                return "Pátio não encontrado.";

            patioExistente.Localizacao = patio.Localizacao;
            patioExistente.VagasTotais = patio.VagasTotais;

            if (patio.VagasOcupadas > patioExistente.VagasTotais)
                return "O número de vagas ocupadas não pode ser maior que o número total de vagas.";

            patioExistente.VagasOcupadas = patio.VagasOcupadas;

            await _context.SaveChangesAsync();
            return "Pátio atualizado com sucesso!";
        }

        public async Task<string> DeletePatioAsync(string nomePatio)
        {
            var patio = await _context.Patios
                .FirstOrDefaultAsync(p => p.NomePatio == nomePatio);

            if (patio == null)
                return "Pátio não encontrado.";

            _context.Patios.Remove(patio);
            await _context.SaveChangesAsync();

            return "Pátio removido com sucesso!";
        }
    }
}
