using FaiscaSync.Models;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class MotorServices
    {
        private readonly FsContext _context;

        public MotorServices(FsContext context)
        {
            _context = context;
        }

        // Lista todos os Motores
        public async Task<List<Motor>> GetAllAsync()
        {
            return await _context.Motors.ToListAsync();
        }

        // Obtém um Motor por ID
        public async Task<Motor?> GetByIdAsync(int id)
        {
            return await _context.Motors.FindAsync(id);
        }

        // Cria um novo Motor
        public async Task<Motor> CreateAsync(Motor motor)
        {
            _context.Motors.Add(motor);
            await _context.SaveChangesAsync();
            return motor;
        }

        // Atualiza um Motor existente
        public async Task<bool> UpdateAsync(int id, Motor motor)
        {
            var existing = await _context.Motors.FindAsync(id);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(motor);
            await _context.SaveChangesAsync();
            return true;
        }

        // Remove um Motor
        public async Task<bool> DeleteAsync(int id)
        {
            var motor = await _context.Motors.FindAsync(id);
            if (motor == null)
                return false;

            _context.Motors.Remove(motor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
