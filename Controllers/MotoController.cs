using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MottuApi.Data;
using MottuApi.Models;

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

        // GET: api/moto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Moto>>> GetMotos([FromQuery] string status = null, [FromQuery] string setor = null)
        {
            var motos = _context.Motos.AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                if (Enum.TryParse<StatusMoto>(status, true, out var statusEnum))
                {
                    motos = motos.Where(m => m.Status == statusEnum);
                }
                else
                {
                    return BadRequest("Status inválido.");
                }
            }

            if (!string.IsNullOrEmpty(setor))
            {
                if (Enum.TryParse<SetorMoto>(setor, true, out var setorEnum))
                {
                    motos = motos.Where(m => m.Setor == setorEnum);
                }
                else
                {
                    return BadRequest("Setor inválido.");
                }
            }

            return await motos.ToListAsync();
        }

        // GET: api/moto/{placa}
        [HttpGet("{placa}")]
        public async Task<ActionResult<Moto>> GetMoto(string placa)
        {
            var moto = await _context.Motos
                .Include(m => m.Funcionario)
                .FirstOrDefaultAsync(m => m.Placa == placa);

            if (moto == null)
                return NotFound("Moto não encontrada.");

            return moto;
        }

        // POST: api/moto
        [HttpPost]
        public async Task<ActionResult<Moto>> PostMoto(MotoDTO motoDTO)
        {
            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(f => f.UsuarioFuncionario == motoDTO.UsuarioFuncionario);

            if (funcionario == null)
                return BadRequest("Usuário do funcionário inválido. Certifique-se de que o usuário existe no banco de dados.");

            var patio = await _context.Patios
                .FirstOrDefaultAsync(p => p.NomePatio == motoDTO.NomePatio);

            if (patio == null)
                return NotFound("Pátio não encontrado.");

            if (patio.VagasOcupadas >= patio.VagasTotais)
                return BadRequest("Não há vagas disponíveis no pátio.");

            if (!Enum.IsDefined(typeof(StatusMoto), motoDTO.Status))
                return BadRequest("Status inválido.");

            if (!Enum.IsDefined(typeof(SetorMoto), motoDTO.Setor))
                return BadRequest("Setor inválido.");

            var moto = new Moto
            {
                Placa = motoDTO.Placa,
                Modelo = motoDTO.Modelo,
                Marca = motoDTO.Marca,
                Status = motoDTO.Status,
                Setor = motoDTO.Setor,
                Patio = patio,
                UsuarioFuncionario = motoDTO.UsuarioFuncionario
            };

            _context.Motos.Add(moto);
            patio.VagasOcupadas++;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMoto), new { placa = moto.Placa }, moto);
        }

        // PUT: api/moto/{placa}
        [HttpPut("{placa}")]
        public async Task<IActionResult> PutMoto(string placa, MotoDTO motoDTO)
        {
            var motoExistente = await _context.Motos
                .Include(m => m.Funcionario)
                .FirstOrDefaultAsync(m => m.Placa == placa);

            if (motoExistente == null)
                return NotFound("Moto não encontrada.");

            var patio = await _context.Patios
                .FirstOrDefaultAsync(p => p.NomePatio == motoExistente.Patio.NomePatio);

            if (motoDTO.Status != motoExistente.Status)
            {
                if (Enum.IsDefined(typeof(StatusMoto), motoDTO.Status))
                {
                    if (motoDTO.Status == StatusMoto.Alugada)
                    {
                        if (patio.VagasOcupadas >= patio.VagasTotais)
                        {
                            return BadRequest("Não há vagas disponíveis no pátio.");
                        }
                        patio.VagasOcupadas++;
                    }
                    else if (motoDTO.Status == StatusMoto.Disponível)
                    {
                        patio.VagasOcupadas--;
                    }
                }
                else
                {
                    return BadRequest("Status inválido.");
                }
            }

            if (Enum.IsDefined(typeof(SetorMoto), motoDTO.Setor))
            {
                motoExistente.Status = motoDTO.Status;
                motoExistente.Setor = motoDTO.Setor;
            }
            else
            {
                return BadRequest("Setor inválido.");
            }

            await _context.SaveChangesAsync();

            return Ok("Status da moto atualizado com sucesso.");
        }

        // DELETE: api/moto/{placa}
        [HttpDelete("{placa}")]
        public async Task<IActionResult> DeleteMoto(string placa)
        {
            var moto = await _context.Motos
                .FirstOrDefaultAsync(m => m.Placa == placa);

            if (moto == null)
                return NotFound("Moto não encontrada.");

            var patio = await _context.Patios
                .FirstOrDefaultAsync(p => p.NomePatio == moto.Patio.NomePatio);

            if (patio != null)
            {
                patio.VagasOcupadas--;
                await _context.SaveChangesAsync();
            }

            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();

            return Ok("Moto removida com sucesso.");
        }
    }
}
