using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MottuApi.Data;
using MottuApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MottuApi.Controllers
{
    [Route("api/motos")]
    [ApiController]
    public class MotoController : ControllerBase
    {
    private const string Disponivel = "Disponível";
    private const string Alugada = "Alugada";
    private const string Manutencao = "Manutenção";
        private readonly MottuDbContext _context;

        public MotoController(MottuDbContext context)
        {
            _context = context;
        }

        // GET: api/motos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Moto>>> GetMotos()
        {
            var motos = await _context.Motos
                .Include(m => m.Patio)  
                .Include(m => m.Funcionario) 
                .ToListAsync();

            if (motos == null || motos.Count == 0)
                return NotFound("Nenhuma moto encontrada.");

            var result = motos.Select(m => new
            {
                m.Placa,
                m.Modelo,
                m.Status,
                m.Setor,
                m.NomePatio,
                m.UsuarioFuncionario
            }).ToList();

            return Ok(result);
        }

        // GET: api/motos/{placa}
        [HttpGet("{placa}")]
        public async Task<ActionResult<Moto>> GetMoto(string placa)
        {
            var moto = await _context.Motos
                .Include(m => m.Funcionario)
                .Include(m => m.Patio)
                .FirstOrDefaultAsync(m => m.Placa == placa);

            if (moto == null)
                return NotFound("Moto não encontrada.");

            var result = new
            {
                moto.Placa,
                moto.Modelo,
                moto.Status,
                moto.Setor,
                moto.NomePatio,
                moto.UsuarioFuncionario
            };

            return Ok(result);
        }

        // POST: api/motos
        [HttpPost]
        public async Task<ActionResult<Moto>> PostMoto([FromBody] MotoDto motoDto)
        {
            // Validações de modelo, status e setor
            if (!new[] { "MottuSport", "MottuE", "MottuPop" }.Contains(motoDto.Modelo))
                return BadRequest("Modelo inválido. Os modelos válidos são: MottuSport, MottuE, MottuPop.");

            if (!new[] { Disponivel, Alugada, Manutencao }.Contains(motoDto.Status))
                return BadRequest($"Status inválido. Os valores válidos são: '{Disponivel}', '{Alugada}', ou '{Manutencao}'.");


            if (!new[] { "Bom", "Intermediário", "Ruim" }.Contains(motoDto.Setor))
                return BadRequest("Setor inválido. Os valores válidos são: 'Bom', 'Intermediário', ou 'Ruim'.");

            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(f => f.UsuarioFuncionario == motoDto.UsuarioFuncionario);

            if (funcionario == null)
                return BadRequest("Funcionário não encontrado.");

            var patio = await _context.Patios
                .FirstOrDefaultAsync(p => p.NomePatio == motoDto.NomePatio);

            if (patio == null)
                return BadRequest("Pátio não encontrado.");

            if (patio.VagasOcupadas >= patio.VagasTotais)
                return BadRequest("Não há vagas disponíveis no pátio.");

            var moto = new Moto
            {
                Placa = motoDto.Placa,
                Modelo = motoDto.Modelo,
                Status = motoDto.Status,
                Setor = motoDto.Setor,
                NomePatio = motoDto.NomePatio,
                UsuarioFuncionario = motoDto.UsuarioFuncionario,
                Funcionario = funcionario,
                Patio = patio
            };

            if (moto.Status == Disponivel || moto.Status == Manutencao)
                patio.VagasOcupadas++;


            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMoto), new { placa = moto.Placa }, moto);
        }

        // PUT: api/motos/{placa}
        [HttpPut("{placa}")]
        public async Task<IActionResult> PutMoto(string placa, [FromBody] MotoDto motoDto)
        {
            // Validações de modelo, status e setor
            if (!new[] { "MottuSport", "MottuE", "MottuPop" }.Contains(motoDto.Modelo))
                return BadRequest("Modelo inválido. Os modelos válidos são: MottuSport, MottuE, MottuPop.");

            if (!new[] { Disponivel, Alugada, Manutencao }.Contains(motoDto.Status))
                return BadRequest($"Status inválido. Os valores válidos são: '{Disponivel}', '{Alugada}', ou '{Manutencao}'.");

            if (!new[] { "Bom", "Intermediário", "Ruim" }.Contains(motoDto.Setor))
                return BadRequest("Setor inválido. Os valores válidos são: 'Bom', 'Intermediário', ou 'Ruim'.");

            var motoExistente = await _context.Motos
                .Include(m => m.Patio)
                .FirstOrDefaultAsync(m => m.Placa == placa);

            if (motoExistente == null)
                return NotFound("Moto não encontrada.");

            var patio = motoExistente.Patio;

            if (motoDto.Status != motoExistente.Status)
            { if ((motoDto.Status == Alugada && motoExistente.Status == Disponivel) ||
            ((motoDto.Status == Disponivel || motoDto.Status == Manutencao) && motoExistente.Status == Alugada))
                {
                    patio.VagasOcupadas += (motoDto.Status == Alugada) ? -1 : 1;
                }
            }
    

            motoExistente.Modelo = motoDto.Modelo;
            motoExistente.Status = motoDto.Status;
            motoExistente.Setor = motoDto.Setor;

            await _context.SaveChangesAsync();

            return Ok("Moto atualizada com sucesso.");
        }

        // DELETE: api/motos/{placa}
        [HttpDelete("{placa}")]
        public async Task<IActionResult> DeleteMoto(string placa)
        {
            var moto = await _context.Motos
                .Include(m => m.Patio)
                .FirstOrDefaultAsync(m => m.Placa == placa);

            if (moto == null)
                return NotFound("Moto não encontrada.");

            var patio = moto.Patio;
            if (patio != null && (moto.Status == Disponivel || moto.Status == Manutencao))
            {
                patio.VagasOcupadas--;
            }

            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();

            return Ok("Moto removida com sucesso.");
        }
    }
}
