using FaiscaSync.Models;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class VendaServices
    {
        private readonly FsContext _context;
        private readonly VeiculoServices _veiculoService;

        // Injeção de dependência do VeiculoServices
        public VendaServices(FsContext context, VeiculoServices veiculoService)
        {
            _context = context;
            _veiculoService = veiculoService;
        }

        // Lista todas as Vendas
        public async Task<List<Venda>> GetAllAsync()
        {
            return await _context.Vendas.ToListAsync();
        }

        // Obtém uma Venda por ID
        public async Task<Venda?> GetByIdAsync(int id)
        {
            return await _context.Vendas.FindAsync(id);
        }

        // Cria uma nova Venda
        public async Task<Venda> CreateAsync(Venda venda)
        {
            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();

            await _veiculoService.AtualizarEstadoParaVendidoAsync(venda.IdVeiculo);

            return venda;
        }

        // Atualiza uma Venda existente
        public async Task<bool> UpdateAsync(int id, Venda venda)
        {
            var existing = await _context.Vendas.FindAsync(id);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(venda);
            await _context.SaveChangesAsync();
            return true;
        }

        // Remove uma Venda
        public async Task<bool> DeleteAsync(int id)
        {
            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null)
                return false;

            _context.Vendas.Remove(venda);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
