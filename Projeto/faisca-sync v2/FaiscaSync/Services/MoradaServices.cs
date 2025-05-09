using FaiscaSync.Models;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class MoradaServices
    {
        private readonly FsContext _context;

        public MoradaServices(FsContext context)
        {
            _context = context;
        }

        // Lista todas as Moradas
        public async Task<List<Morada>> GetAllAsync()
        {
            return await _context.Morada.ToListAsync();
        }

        // Obtém uma Morada por ID
        public async Task<Morada?> GetByIdAsync(int id)
        {
            return await _context.Morada.FindAsync(id);
        }

        // Cria uma nova Morada
        public async Task<Morada> CreateAsync(Morada morada)
        {
            _context.Morada.Add(morada);
            await _context.SaveChangesAsync();
            return morada;
        }

        // Atualiza uma Morada existente
        public async Task<bool> UpdateAsync(int id, Morada morada)
        {
            var existing = await _context.Morada.FindAsync(id);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(morada);
            await _context.SaveChangesAsync();
            return true;
        }

        // Remove uma Morada
        public async Task<bool> DeleteAsync(int id)
        {
            var morada = await _context.Morada.FindAsync(id);
            if (morada == null)
                return false;

            _context.Morada.Remove(morada);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
