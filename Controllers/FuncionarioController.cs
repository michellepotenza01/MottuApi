using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MottuApi.Data;
using MottuApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MottuApi.Controllers
{
    [Route("api/funcionarios")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly MottuDbContext _context;

        public FuncionarioController(MottuDbContext context)
        {
            _context = context;
        }

        // GET: api/funcionarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios()
        {
            var funcionarios = await _context.Funcionarios
                .Include(f => f.Patio)  // Inclui o pátio
                .ToListAsync();

            if (funcionarios == null || funcionarios.Count == 0)
                return NotFound("Nenhum funcionário encontrado.");

            // Simplificando a resposta para evitar dados desnecessários e tornar mais organizada
            var result = funcionarios.Select(f => new
            {
                f.UsuarioFuncionario,
                f.Nome,
                f.NomePatio,
                Patio = new
                {
                    f.Patio.NomePatio,
                    f.Patio.Localizacao
                }
            }).ToList();

            return Ok(result);
        }

        // GET: api/funcionarios/{usuarioFuncionario}
        [HttpGet("{usuarioFuncionario}")]
        public async Task<ActionResult<Funcionario>> GetFuncionarioByUsuario(string usuarioFuncionario)
        {
            var funcionario = await _context.Funcionarios
                .Include(f => f.Patio)
                .FirstOrDefaultAsync(f => f.UsuarioFuncionario == usuarioFuncionario);

            if (funcionario == null)
                return NotFound("Funcionário não encontrado.");

            var result = new
            {
                funcionario.UsuarioFuncionario,
                funcionario.Nome,
                funcionario.NomePatio,
                Patio = new
                {
                    funcionario.Patio.NomePatio,
                    funcionario.Patio.Localizacao
                }
            };

            return Ok(result);
        }

        // POST: api/funcionarios
        [HttpPost]
        public async Task<ActionResult<Funcionario>> PostFuncionario(FuncionarioDto funcionarioDto)
        {
            // Verificar se o pátio existe no banco de dados
            var patio = await _context.Patios.FirstOrDefaultAsync(p => p.NomePatio == funcionarioDto.NomePatio);
            if (patio == null)
                return BadRequest("Pátio não encontrado.");

            // Criar o funcionário
            var funcionario = new Funcionario
            {
                UsuarioFuncionario = funcionarioDto.UsuarioFuncionario,
                Nome = funcionarioDto.Nome,
                Senha = funcionarioDto.Senha,
                Patio = patio
            };

            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFuncionarioByUsuario), new { usuarioFuncionario = funcionario.UsuarioFuncionario }, funcionario);
        }

        // PUT: api/funcionarios/{usuarioFuncionario}
        [HttpPut("{usuarioFuncionario}")]
        public async Task<IActionResult> PutFuncionario(string usuarioFuncionario, FuncionarioDto funcionarioDto)
        {
            if (usuarioFuncionario != funcionarioDto.UsuarioFuncionario)
                return BadRequest("O usuário do funcionário não corresponde ao parâmetro.");

            var funcionarioExistente = await _context.Funcionarios.Include(f => f.Patio)
                .FirstOrDefaultAsync(f => f.UsuarioFuncionario == usuarioFuncionario);

            if (funcionarioExistente == null)
                return NotFound("Funcionário não encontrado.");

            var patio = await _context.Patios.FirstOrDefaultAsync(p => p.NomePatio == funcionarioDto.NomePatio);
            if (patio == null)
                return BadRequest("Pátio não encontrado.");

            funcionarioExistente.Nome = funcionarioDto.Nome;
            funcionarioExistente.Senha = funcionarioDto.Senha;
            funcionarioExistente.Patio = patio;

            await _context.SaveChangesAsync();

            return Ok("Funcionário atualizado com sucesso.");
        }

        // DELETE: api/funcionarios/{usuarioFuncionario}
        [HttpDelete("{usuarioFuncionario}")]
        public async Task<IActionResult> DeleteFuncionario(string usuarioFuncionario)
        {
            var funcionario = await _context.Funcionarios.Include(f => f.Patio)
                .FirstOrDefaultAsync(f => f.UsuarioFuncionario == usuarioFuncionario);

            if (funcionario == null)
                return NotFound("Funcionário não encontrado.");

            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();

            return Ok("Funcionário removido com sucesso.");
        }
    }
}
