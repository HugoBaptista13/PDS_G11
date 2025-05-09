using FaiscaSync.Models;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class ClienteService
    {
        private readonly FsContext _context;

        public ClienteService(FsContext context)
        {
            _context = context;
        }

        // Lista todos os clientes
        public async Task<List<Cliente>> GetClientesAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        // Obtém cliente por ID
        public async Task<Cliente?> GetClienteByIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        // Cria novo cliente
        public async Task<Cliente> CreateClienteAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        // Atualiza cliente
        public async Task<bool> UpdateClienteAsync(int id, Cliente cliente)
        {
            var existing = await _context.Clientes.FindAsync(id);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(cliente);
            await _context.SaveChangesAsync();
            return true;
        }

        // Remove cliente
        public async Task<bool> DeleteClienteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return false;

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
