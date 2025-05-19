using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MottuApi.Data;
using MottuApi.Models;

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

        // GET: api/funcionario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios()
        {
            return await _context.Funcionarios.ToListAsync();
        }

        // GET: api/funcionario/{usuarioFuncionario}
        [HttpGet("{usuarioFuncionario}")]
        public async Task<ActionResult<Funcionario>> GetFuncionario(string usuarioFuncionario)
        {
            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(f => f.UsuarioFuncionario == usuarioFuncionario);

            if (funcionario == null)
                return NotFound("Funcionário não encontrado.");

            return funcionario;
        }

        // POST: api/funcionario
        [HttpPost]
        public async Task<ActionResult<Funcionario>> PostFuncionario(FuncionarioDTO funcionarioDTO)
        {
            if (ModelState.IsValid)
            {
                var patio = await _context.Patios
                    .FirstOrDefaultAsync(p => p.NomePatio == funcionarioDTO.NomePatio);

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

                return CreatedAtAction(nameof(GetFuncionario), new { usuarioFuncionario = funcionario.UsuarioFuncionario }, funcionario);
            }

            return BadRequest("Dados inválidos.");
        }

        // PUT: api/funcionario/{usuarioFuncionario}
        [HttpPut("{usuarioFuncionario}")]
        public async Task<IActionResult> PutFuncionario(string usuarioFuncionario, FuncionarioDTO funcionarioDTO)
        {
            if (usuarioFuncionario != funcionarioDTO.UsuarioFuncionario)
                return BadRequest("O usuário do funcionário não corresponde ao parâmetro.");

            var funcionarioExistente = await _context.Funcionarios
                .FirstOrDefaultAsync(f => f.UsuarioFuncionario == usuarioFuncionario);

            if (funcionarioExistente == null)
                return NotFound("Funcionário não encontrado.");

            funcionarioExistente.Nome = funcionarioDTO.Nome;
            funcionarioExistente.Senha = funcionarioDTO.Senha;

            await _context.SaveChangesAsync();

            return Ok("Funcionário atualizado com sucesso.");
        }

        // DELETE: api/funcionario/{usuarioFuncionario}
        [HttpDelete("{usuarioFuncionario}")]
        public async Task<IActionResult> DeleteFuncionario(string usuarioFuncionario)
        {
            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(f => f.UsuarioFuncionario == usuarioFuncionario);

            if (funcionario == null)
                return NotFound("Funcionário não encontrado.");

            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();

            return Ok("Funcionário removido com sucesso.");
        }
    }
}
