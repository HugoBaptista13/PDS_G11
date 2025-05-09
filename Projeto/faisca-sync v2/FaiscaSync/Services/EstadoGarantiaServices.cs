using FaiscaSync.Models;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class EstadoGarantiaServices
    {
        private readonly FsContext _context;

        public EstadoGarantiaServices(FsContext context)
        {
            _context = context;
        }

        // Lista todos os Estados de Garantia
        public async Task<List<EstadoGarantia>> GetAllAsync()
        {
            return await _context.EstadoGarantia.ToListAsync();
        }

        // Obtém um Estado de Garantia por ID
        public async Task<EstadoGarantia?> GetByIdAsync(int id)
        {
            return await _context.EstadoGarantia.FindAsync(id);
        }

        // Cria um novo Estado de Garantia
        public async Task<EstadoGarantia> CreateAsync(EstadoGarantia estadoGarantia)
        {
            _context.EstadoGarantia.Add(estadoGarantia);
            await _context.SaveChangesAsync();
            return estadoGarantia;
        }

        // Atualiza um Estado de Garantia
        public async Task<bool> UpdateAsync(int id, EstadoGarantia estadoGarantia)
        {
            var existing = await _context.EstadoGarantia.FindAsync(id);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(estadoGarantia);
            await _context.SaveChangesAsync();
            return true;
        }

        // Remove um Estado de Garantia
        public async Task<bool> DeleteAsync(int id)
        {
            var estadoGarantia = await _context.EstadoGarantia.FindAsync(id);
            if (estadoGarantia == null)
                return false;

            _context.EstadoGarantia.Remove(estadoGarantia);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
