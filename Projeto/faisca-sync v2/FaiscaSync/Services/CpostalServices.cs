using FaiscaSync.Models;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class CpostalServices
    {
        private readonly FsContext _context;

        public CpostalServices(FsContext context)
        {
            _context = context;
        }

        // Lista todos os Códigos Postais
        public async Task<List<Cpostal>> GetAllAsync()
        {
            return await _context.Cpostals.ToListAsync();
        }

        // Obtém um Código Postal por ID
        public async Task<Cpostal?> GetByIdAsync(int id)
        {
            return await _context.Cpostals.FindAsync(id);
        }

        // Cria um novo Código Postal
        public async Task<Cpostal> CreateAsync(Cpostal cpostal)
        {
            _context.Cpostals.Add(cpostal);
            await _context.SaveChangesAsync();
            return cpostal;
        }

        // Atualiza um Código Postal
        public async Task<bool> UpdateAsync(int id, Cpostal cpostal)
        {
            var existing = await _context.Cpostals.FindAsync(id);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(cpostal);
            await _context.SaveChangesAsync();
            return true;
        }

        // Remove um Código Postal
        public async Task<bool> DeleteAsync(int id)
        {
            var cpostal = await _context.Cpostals.FindAsync(id);
            if (cpostal == null)
                return false;

            _context.Cpostals.Remove(cpostal);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
