using FaiscaSync.Models;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class GarantiaServices
    {
        private readonly FsContext _context;

        public GarantiaServices(FsContext context)
        {
            _context = context;
        }

        // Lista todas as Garantias
        public async Task<List<Garantia>> GetAllAsync()
        {
            return await _context.Garantia.ToListAsync();
        }

        // Obtém uma Garantia por ID
        public async Task<Garantia?> GetByIdAsync(int id)
        {
            return await _context.Garantia.FindAsync(id);
        }

        // Cria uma nova Garantia
        public async Task<Garantia> CreateAsync(Garantia garantia)
        {
            _context.Garantia.Add(garantia);
            await _context.SaveChangesAsync();
            return garantia;
        }

        // Atualiza uma Garantia existente
        public async Task<bool> UpdateAsync(int id, Garantia garantia)
        {
            var existing = await _context.Garantia.FindAsync(id);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(garantia);
            await _context.SaveChangesAsync();
            return true;
        }

        // Remove uma Garantia
        public async Task<bool> DeleteAsync(int id)
        {
            var garantia = await _context.Garantia.FindAsync(id);
            if (garantia == null)
                return false;

            _context.Garantia.Remove(garantia);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
