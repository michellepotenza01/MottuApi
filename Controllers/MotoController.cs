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
                .Include(m => m.Patio)  // Inclui o pátio
                .Include(m => m.Funcionario) // Inclui o funcionário
                .ToListAsync();

            if (motos == null || motos.Count == 0)
                return NotFound("Nenhuma moto encontrada.");

            // Simplificar o retorno para incluir apenas as informações necessárias
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

            // Simplificar o retorno para incluir apenas as informações necessárias
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
        public async Task<ActionResult<Moto>> PostMoto([FromBody] MotoDTO motoDTO)
        {
            // Validações de modelo, status e setor
            if (!new[] { "MottuSport", "MottuE", "MottuPop" }.Contains(motoDTO.Modelo))
                return BadRequest("Modelo inválido. Os modelos válidos são: MottuSport, MottuE, MottuPop.");

            if (!new[] { "Disponível", "Alugada", "Manutenção" }.Contains(motoDTO.Status))
                return BadRequest("Status inválido. Os valores válidos são: 'Disponível', 'Alugada', ou 'Manutenção'.");

            if (!new[] { "Bom", "Intermediário", "Ruim" }.Contains(motoDTO.Setor))
                return BadRequest("Setor inválido. Os valores válidos são: 'Bom', 'Intermediário', ou 'Ruim'.");

            // Verificar se o funcionário existe
            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(f => f.UsuarioFuncionario == motoDTO.UsuarioFuncionario);

            if (funcionario == null)
                return BadRequest("Funcionário não encontrado.");

            // Verificar se o pátio existe
            var patio = await _context.Patios
                .FirstOrDefaultAsync(p => p.NomePatio == motoDTO.NomePatio);

            if (patio == null)
                return BadRequest("Pátio não encontrado.");

            if (patio.VagasOcupadas >= patio.VagasTotais)
                return BadRequest("Não há vagas disponíveis no pátio.");

            var moto = new Moto
            {
                Placa = motoDTO.Placa,
                Modelo = motoDTO.Modelo,
                Status = motoDTO.Status,
                Setor = motoDTO.Setor,
                NomePatio = motoDTO.NomePatio,
                UsuarioFuncionario = motoDTO.UsuarioFuncionario,
                Funcionario = funcionario,
                Patio = patio
            };

            if (moto.Status == "Disponível" || moto.Status == "Manutenção")
                patio.VagasOcupadas++;

            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMoto), new { placa = moto.Placa }, moto);
        }

        // PUT: api/motos/{placa}
        [HttpPut("{placa}")]
        public async Task<IActionResult> PutMoto(string placa, [FromBody] MotoDTO motoDTO)
        {
            // Validações de modelo, status e setor
            if (!new[] { "MottuSport", "MottuE", "MottuPop" }.Contains(motoDTO.Modelo))
                return BadRequest("Modelo inválido. Os modelos válidos são: MottuSport, MottuE, MottuPop.");

            if (!new[] { "Disponível", "Alugada", "Manutenção" }.Contains(motoDTO.Status))
                return BadRequest("Status inválido. Os valores válidos são: 'Disponível', 'Alugada', ou 'Manutenção'.");

            if (!new[] { "Bom", "Intermediário", "Ruim" }.Contains(motoDTO.Setor))
                return BadRequest("Setor inválido. Os valores válidos são: 'Bom', 'Intermediário', ou 'Ruim'.");

            var motoExistente = await _context.Motos
                .Include(m => m.Patio)
                .FirstOrDefaultAsync(m => m.Placa == placa);

            if (motoExistente == null)
                return NotFound("Moto não encontrada.");

            var patio = motoExistente.Patio;

            if (motoDTO.Status != motoExistente.Status)
            {
                if (motoDTO.Status == "Alugada")
                {
                    if (motoExistente.Status == "Disponível")
                        patio.VagasOcupadas--;
                }
                else if (motoDTO.Status == "Disponível" || motoDTO.Status == "Manutenção")
                {
                    if (motoExistente.Status == "Alugada")
                        patio.VagasOcupadas++;
                }
            }

            motoExistente.Modelo = motoDTO.Modelo;
            motoExistente.Status = motoDTO.Status;
            motoExistente.Setor = motoDTO.Setor;

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
            if (patio != null && (moto.Status == "Disponível" || moto.Status == "Manutenção"))
            {
                patio.VagasOcupadas--;
            }

            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();

            return Ok("Moto removida com sucesso.");
        }
    }
}
