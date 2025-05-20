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
        public async Task<ActionResult<IEnumerable<Moto>>> GetMotos([FromQuery] string status = null, [FromQuery] string setor = null)
        {
            var motos = _context.Motos.AsQueryable();

            if (!string.IsNullOrEmpty(status))
                motos = motos.Where(m => m.Status.ToString() == status);

            if (!string.IsNullOrEmpty(setor))
                motos = motos.Where(m => m.Setor.ToString() == setor);

            return await motos.Include(m => m.Patio).ToListAsync();  // Inclui o pátio
        }

        // GET: api/motos/{placa}
        [HttpGet("{placa}")]
        public async Task<ActionResult<Moto>> GetMoto(string placa)
        {
            var moto = await _context.Motos
                .Include(m => m.Funcionario)
                .Include(m => m.Patio)  // Inclui o pátio associado
                .FirstOrDefaultAsync(m => m.Placa == placa);

            if (moto == null)
                return NotFound("Moto não encontrada.");

            return Ok(moto);
        }

        // POST: api/motos
        [HttpPost]
        public async Task<ActionResult<Moto>> PostMoto(MotoDTO motoDTO)
        {
            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(f => f.UsuarioFuncionario == motoDTO.UsuarioFuncionario);

            if (funcionario == null)
                return BadRequest("Funcionário não encontrado.");

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

            if (moto.Status == StatusMoto.Disponível || moto.Status == StatusMoto.Manutenção)
                patio.VagasOcupadas++;

            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMoto), new { placa = moto.Placa }, moto);
        }

        // PUT: api/motos/{placa}
        [HttpPut("{placa}")]
        public async Task<IActionResult> PutMoto(string placa, MotoDTO motoDTO)
        {
            var motoExistente = await _context.Motos
                .Include(m => m.Patio)
                .FirstOrDefaultAsync(m => m.Placa == placa);

            if (motoExistente == null)
                return NotFound("Moto não encontrada.");

            var patio = await _context.Patios
                .FirstOrDefaultAsync(p => p.NomePatio == motoDTO.NomePatio);

            if (patio == null)
                return NotFound("Pátio não encontrado.");

            // Lógica para atualizar o status
            if (motoDTO.Status != motoExistente.Status)
            {
                // Libera ou ocupa vaga dependendo do status
                if (motoDTO.Status == StatusMoto.Alugada)
                {
                    if (motoExistente.Status == StatusMoto.Disponível)
                        patio.VagasOcupadas--;
                }
                else if (motoDTO.Status == StatusMoto.Disponível || motoDTO.Status == StatusMoto.Manutenção)
                {
                    if (motoExistente.Status == StatusMoto.Alugada)
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
            if (patio != null)
            {
                if (moto.Status == StatusMoto.Disponível || moto.Status == StatusMoto.Manutenção)
                {
                    patio.VagasOcupadas--;  // Libera a vaga se a moto for removida
                }
                await _context.SaveChangesAsync();
            }

            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();

            return Ok("Moto removida com sucesso.");
        }
    }
}
