using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MottuApi.Data;
using MottuApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MottuApi.Controllers
{
    [Route("api/patios")]
    [ApiController]
    public class PatioController : ControllerBase
    {
        private readonly MottuDbContext _context;

        public PatioController(MottuDbContext context)
        {
            _context = context;
        }

        // GET: api/patios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patio>>> GetPatios([FromQuery] string nomePatio = null)
        {
            var patios = _context.Patios.AsQueryable();

            if (!string.IsNullOrEmpty(nomePatio))
                patios = patios.Where(p => p.NomePatio == nomePatio);

            return await patios.ToListAsync();
        }

        // GET: api/patios/{nomePatio}
        [HttpGet("{nomePatio}")]
        public async Task<ActionResult<Patio>> GetPatio(string nomePatio)
        {
            var patio = await _context.Patios.FirstOrDefaultAsync(p => p.NomePatio == nomePatio);

            if (patio == null)
                return NotFound("Pátio não encontrado.");

            return Ok(patio);
        }

        // POST: api/patios
        [HttpPost]
        public async Task<ActionResult<Patio>> PostPatio(PatioDTO patioDTO)
        {
            if (patioDTO.VagasOcupadas > patioDTO.VagasTotais)
                return BadRequest("O número de vagas ocupadas não pode ser maior que o número total de vagas.");

            var patio = new Patio
            {
                NomePatio = patioDTO.NomePatio,
                Localizacao = patioDTO.Localizacao,
                VagasTotais = patioDTO.VagasTotais,
                VagasOcupadas = patioDTO.VagasOcupadas
            };

            _context.Patios.Add(patio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPatio), new { nomePatio = patio.NomePatio }, patio);
        }

        // PUT: api/patios/{nomePatio}
        [HttpPut("{nomePatio}")]
        public async Task<IActionResult> PutPatio(string nomePatio, PatioDTO patioDTO)
        {
            if (nomePatio != patioDTO.NomePatio)
                return BadRequest("O nome do pátio não corresponde ao parâmetro.");

            var patioExistente = await _context.Patios.FirstOrDefaultAsync(p => p.NomePatio == nomePatio);

            if (patioExistente == null)
                return NotFound("Pátio não encontrado.");

            patioExistente.Localizacao = patioDTO.Localizacao;
            patioExistente.VagasTotais = patioDTO.VagasTotais;

            if (patioDTO.VagasOcupadas > patioExistente.VagasTotais)
                return BadRequest("O número de vagas ocupadas não pode ser maior que o número total de vagas.");

            patioExistente.VagasOcupadas = patioDTO.VagasOcupadas;

            await _context.SaveChangesAsync();

            return Ok("Pátio atualizado com sucesso.");
        }

        // DELETE: api/patios/{nomePatio}
        [HttpDelete("{nomePatio}")]
        public async Task<IActionResult> DeletePatio(string nomePatio)
        {
            var patio = await _context.Patios.FirstOrDefaultAsync(p => p.NomePatio == nomePatio);

            if (patio == null)
                return NotFound("Pátio não encontrado.");

            _context.Patios.Remove(patio);
            await _context.SaveChangesAsync();

            return Ok("Pátio removido com sucesso.");
        }
    }
}
