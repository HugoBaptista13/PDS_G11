using FaiscaSync.Models;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class CargoServices
    {
        private readonly FsContext _context;

        public CargoServices(FsContext context)
        {
            _context = context;
        }

        public async Task<List<Cargo>> GetAllAsync()
        {
            return await _context.Cargos.ToListAsync();
        }

        public async Task<Cargo?> GetByIdAsync(int id)
        {
            return await _context.Cargos.FindAsync(id);
        }

        public async Task<Cargo> CreateAsync(Cargo cargo)
        {
            _context.Cargos.Add(cargo);
            await _context.SaveChangesAsync();
            return cargo;
        }

        public async Task<bool> UpdateAsync(int id, Cargo cargo)
        {
            var existing = await _context.Cargos.FindAsync(id);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(cargo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cargo = await _context.Cargos.FindAsync(id);
            if (cargo == null)
                return false;

            _context.Cargos.Remove(cargo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
