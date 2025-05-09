using FaiscaSync.Models;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class TipoVeiculoServices
    {
        private readonly FsContext _context;

        public TipoVeiculoServices(FsContext context)
        {
            _context = context;
        }

        // Lista todos os Tipos de Veículo
        public async Task<List<TipoVeiculo>> GetAllAsync()
        {
            return await _context.TipoVeiculos.ToListAsync();
        }

        // Obtém um Tipo de Veículo por ID
        public async Task<TipoVeiculo?> GetByIdAsync(int id)
        {
            return await _context.TipoVeiculos.FindAsync(id);
        }

        // Cria um novo Tipo de Veículo
        public async Task<TipoVeiculo> CreateAsync(TipoVeiculo tipoVeiculo)
        {
            _context.TipoVeiculos.Add(tipoVeiculo);
            await _context.SaveChangesAsync();
            return tipoVeiculo;
        }

        // Atualiza um Tipo de Veículo existente
        public async Task<bool> UpdateAsync(int id, TipoVeiculo tipoVeiculo)
        {
            var existing = await _context.TipoVeiculos.FindAsync(id);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(tipoVeiculo);
            await _context.SaveChangesAsync();
            return true;
        }

        // Remove um Tipo de Veículo
        public async Task<bool> DeleteAsync(int id)
        {
            var tipoVeiculo = await _context.TipoVeiculos.FindAsync(id);
            if (tipoVeiculo == null)
                return false;

            _context.TipoVeiculos.Remove(tipoVeiculo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
