using FaiscaSync.Models;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class EstadoManutencaoServices
    {
        private readonly FsContext _context;

        public EstadoManutencaoServices(FsContext context)
        {
            _context = context;
        }

        // Lista todos os Estados de Manutenção
        public async Task<List<EstadoManutencao>> GetAllAsync()
        {
            return await _context.EstadoManutencaos.ToListAsync();
        }

        // Obtém um Estado de Manutenção por ID
        public async Task<EstadoManutencao?> GetByIdAsync(int id)
        {
            return await _context.EstadoManutencaos.FindAsync(id);
        }

        // Cria um novo Estado de Manutenção
        public async Task<EstadoManutencao> CreateAsync(EstadoManutencao estadoManutencao)
        {
            _context.EstadoManutencaos.Add(estadoManutencao);
            await _context.SaveChangesAsync();
            return estadoManutencao;
        }

        // Atualiza um Estado de Manutenção
        public async Task<bool> UpdateAsync(int id, EstadoManutencao estadoManutencao)
        {
            var existing = await _context.EstadoManutencaos.FindAsync(id);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(estadoManutencao);
            await _context.SaveChangesAsync();
            return true;
        }

        // Remove um Estado de Manutenção
        public async Task<bool> DeleteAsync(int id)
        {
            var estadoManutencao = await _context.EstadoManutencaos.FindAsync(id);
            if (estadoManutencao == null)
                return false;

            _context.EstadoManutencaos.Remove(estadoManutencao);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
