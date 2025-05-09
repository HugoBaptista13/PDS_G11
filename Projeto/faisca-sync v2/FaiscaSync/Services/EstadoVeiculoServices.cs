using FaiscaSync.Models;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class EstadoVeiculoServices
    {
        private readonly FsContext _context;

        public EstadoVeiculoServices(FsContext context)
        {
            _context = context;
        }

        // Lista todos os Estados de Veículo
        public async Task<List<EstadoVeiculo>> GetAllAsync()
        {
            return await _context.EstadoVeiculos.ToListAsync();
        }

        // Obtém um Estado de Veículo por ID
        public async Task<EstadoVeiculo?> GetByIdAsync(int id)
        {
            return await _context.EstadoVeiculos.FindAsync(id);
        }

        // Cria um novo Estado de Veículo
        public async Task<EstadoVeiculo> CreateAsync(EstadoVeiculo estadoVeiculo)
        {
            _context.EstadoVeiculos.Add(estadoVeiculo);
            await _context.SaveChangesAsync();
            return estadoVeiculo;
        }

        // Atualiza um Estado de Veículo
        public async Task<bool> UpdateAsync(int id, EstadoVeiculo estadoVeiculo)
        {
            var existing = await _context.EstadoVeiculos.FindAsync(id);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(estadoVeiculo);
            await _context.SaveChangesAsync();
            return true;
        }

        // Remove um Estado de Veículo
        public async Task<bool> DeleteAsync(int id)
        {
            var estadoVeiculo = await _context.EstadoVeiculos.FindAsync(id);
            if (estadoVeiculo == null)
                return false;

            _context.EstadoVeiculos.Remove(estadoVeiculo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
