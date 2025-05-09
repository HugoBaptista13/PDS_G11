using FaiscaSync.Models;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class ModeloServices
    {
        private readonly FsContext _context;

        public ModeloServices(FsContext context)
        {
            _context = context;
        }

        // Lista todos os Modelos
        public async Task<List<Modelo>> GetAllAsync()
        {
            return await _context.Modelos.ToListAsync();
        }

        // Obtém um Modelo por ID
        public async Task<Modelo?> GetByIdAsync(int id)
        {
            return await _context.Modelos.FindAsync(id);
        }

        // Cria um novo Modelo
        public async Task<Modelo> CreateAsync(Modelo modelo)
        {
            _context.Modelos.Add(modelo);
            await _context.SaveChangesAsync();
            return modelo;
        }

        // Atualiza um Modelo existente
        public async Task<bool> UpdateAsync(int id, Modelo modelo)
        {
            var existing = await _context.Modelos.FindAsync(id);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(modelo);
            await _context.SaveChangesAsync();
            return true;
        }

        // Remove um Modelo
        public async Task<bool> DeleteAsync(int id)
        {
            var modelo = await _context.Modelos.FindAsync(id);
            if (modelo == null)
                return false;

            _context.Modelos.Remove(modelo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
