using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MottuApi.Data;
using MottuApi.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MottuApi.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly MottuDbContext _context;

        public ClienteController(MottuDbContext context)
        {
            _context = context;
        }

        // GET: api/cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        // GET: api/cliente/{usuarioCliente}
        [HttpGet("{usuarioCliente}")]
        public async Task<ActionResult<Cliente>> GetCliente(string usuarioCliente)
        {
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.UsuarioCliente == usuarioCliente);

            if (cliente == null)
                return NotFound("Cliente não encontrado.");

            return cliente;
        }

        // POST: api/cliente
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(ClienteDTO clienteDTO)
        {
            if (ModelState.IsValid)
            {
                var cliente = new Cliente
                {
                    UsuarioCliente = clienteDTO.UsuarioCliente,
                    Nome = clienteDTO.Nome,
                    Senha = clienteDTO.Senha,
                };

                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCliente), new { usuarioCliente = cliente.UsuarioCliente }, cliente);
            }

            return BadRequest("Dados inválidos. Verifique as informações fornecidas.");
        }

        // PUT: api/cliente/{usuarioCliente}
        [HttpPut("{usuarioCliente}")]
        public async Task<IActionResult> PutCliente(string usuarioCliente, ClienteDTO clienteDTO)
        {
            if (usuarioCliente != clienteDTO.UsuarioCliente)
                return BadRequest("O usuário do cliente não corresponde ao parâmetro.");

            var clienteExistente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.UsuarioCliente == usuarioCliente);

            if (clienteExistente == null)
                return NotFound("Cliente não encontrado.");

            clienteExistente.Nome = clienteDTO.Nome;
            clienteExistente.Senha = clienteDTO.Senha;

            await _context.SaveChangesAsync();

            return Ok("Cliente atualizado com sucesso.");
        }

        // DELETE: api/cliente/{usuarioCliente}
        [HttpDelete("{usuarioCliente}")]
        public async Task<IActionResult> DeleteCliente(string usuarioCliente)
        {
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.UsuarioCliente == usuarioCliente);

            if (cliente == null)
                return NotFound("Cliente não encontrado.");

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return Ok("Cliente removido com sucesso.");
        }
    }
}
