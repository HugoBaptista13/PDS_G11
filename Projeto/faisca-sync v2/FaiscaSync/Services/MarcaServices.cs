using FaiscaSync.Models;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class MarcaServices
    {
        private readonly FsContext _context;

        public MarcaServices(FsContext context)
        {
            _context = context;
        }

        // Lista todas as Marcas
        public async Task<List<Marca>> GetAllAsync()
        {
            return await _context.Marcas.ToListAsync();
        }

        // Obtém uma Marca por ID
        public async Task<Marca?> GetByIdAsync(int id)
        {
            return await _context.Marcas.FindAsync(id);
        }

        // Cria uma nova Marca
        public async Task<Marca> CreateAsync(Marca marca)
        {
            _context.Marcas.Add(marca);
            await _context.SaveChangesAsync();
            return marca;
        }

        // Atualiza uma Marca existente
        public async Task<bool> UpdateAsync(int id, Marca marca)
        {
            var existing = await _context.Marcas.FindAsync(id);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(marca);
            await _context.SaveChangesAsync();
            return true;
        }

        // Remove uma Marca
        public async Task<bool> DeleteAsync(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null)
                return false;

            _context.Marcas.Remove(marca);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
