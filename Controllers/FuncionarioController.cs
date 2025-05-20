using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MottuApi.Data;
using MottuApi.Models;
using System.Collections.Generic;
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
            var funcionarios = await _context.Funcionarios.Include(f => f.Patio).ToListAsync();
            return Ok(funcionarios);
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

            return Ok(funcionario);
        }

        // POST: api/funcionarios
        [HttpPost]
        public async Task<ActionResult<Funcionario>> PostFuncionario(FuncionarioDTO funcionarioDTO)
        {
            var patio = await _context.Patios.FirstOrDefaultAsync(p => p.NomePatio == funcionarioDTO.NomePatio);

            if (patio == null)
                return BadRequest("Pátio não encontrado.");

            var funcionario = new Funcionario
            {
                UsuarioFuncionario = funcionarioDTO.UsuarioFuncionario,
                Nome = funcionarioDTO.Nome,
                Senha = funcionarioDTO.Senha,
                Patio = patio
            };

            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFuncionarioByUsuario), new { usuarioFuncionario = funcionario.UsuarioFuncionario }, funcionario);
        }

        // PUT: api/funcionarios/{usuarioFuncionario}
        [HttpPut("{usuarioFuncionario}")]
        public async Task<IActionResult> PutFuncionario(string usuarioFuncionario, FuncionarioDTO funcionarioDTO)
        {
            if (usuarioFuncionario != funcionarioDTO.UsuarioFuncionario)
                return BadRequest("O usuário do funcionário não corresponde ao parâmetro.");

            var funcionarioExistente = await _context.Funcionarios.Include(f => f.Patio)
                .FirstOrDefaultAsync(f => f.UsuarioFuncionario == usuarioFuncionario);

            if (funcionarioExistente == null)
                return NotFound("Funcionário não encontrado.");

            var patio = await _context.Patios.FirstOrDefaultAsync(p => p.NomePatio == funcionarioDTO.NomePatio);
            if (patio == null)
                return BadRequest("Pátio não encontrado.");

            funcionarioExistente.Nome = funcionarioDTO.Nome;
            funcionarioExistente.Senha = funcionarioDTO.Senha;
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
