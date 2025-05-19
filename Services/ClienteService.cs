using MottuApi.Data;
using MottuApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MottuApi.Services
{
    public class ClienteService
    {
        private readonly MottuDbContext _context;

        public ClienteService(MottuDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Cliente>> GetClientesAsync()
        {
            return _context.Clientes.AsQueryable();
        }

        public async Task<Cliente> GetClienteAsync(string usuarioCliente)
        {
            return await _context.Clientes.FindAsync(usuarioCliente);
        }

        public async Task<string> AddClienteAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return "Cliente criado com sucesso!";
        }

        public async Task<string> UpdateClienteAsync(string usuarioCliente, Cliente cliente)
        {
            var clienteExistente = await _context.Clientes.FindAsync(usuarioCliente);
            if (clienteExistente == null)
                return "Cliente não encontrado.";

            clienteExistente.Nome = cliente.Nome;
            clienteExistente.Senha = cliente.Senha;

            await _context.SaveChangesAsync();
            return "Cliente atualizado com sucesso!";
        }

        public async Task<string> DeleteClienteAsync(string usuarioCliente)
        {
            var cliente = await _context.Clientes.FindAsync(usuarioCliente);
            if (cliente == null)
                return "Cliente não encontrado.";

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return "Cliente excluído com sucesso!";
        }
    }
}
