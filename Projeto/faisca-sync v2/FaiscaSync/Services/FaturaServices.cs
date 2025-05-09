using FaiscaSync.DTO;
using FaiscaSync.Models;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class FaturaServices
    {
        private readonly FsContext _context;

        public FaturaServices(FsContext context)
        {
            _context = context;
        }

        // Lista todas as Faturas
        public async Task<List<Fatura>> GetAllAsync()
        {
            return await _context.Faturas.ToListAsync();
        }

        // Obtém uma Fatura por ID
        public async Task<Fatura?> GetByIdAsync(int id)
        {
            return await _context.Faturas.FindAsync(id);
        }

        // Cria uma nova Fatura
        public async Task<Fatura> CreateAsync(FaturaDTO faturaDto)
        {
            var fatura = new Fatura
            {
                Dataemissao = faturaDto.DataEmissao,
                Valorfatura = faturaDto.ValorFatura,
                TipoPagamento = faturaDto.TipoPagamento,
                IdCliente = faturaDto.IdCliente,
            };

            if (faturaDto.IsVenda)
            {
                if (faturaDto.IdVendas == 0)
                    throw new InvalidOperationException("IdVendas deve ser fornecido para faturas de venda.");

                // Verifica se a venda existe
                var vendaExiste = await _context.Vendas.AnyAsync(v => v.IdVendas == faturaDto.IdVendas);
                if (!vendaExiste)
                    throw new InvalidOperationException($"Venda com ID {faturaDto.IdVendas} não encontrada.");

                fatura.IdVendas = faturaDto.IdVendas;
            }
            else
            {
                if (faturaDto.IdManutencao == 0)
                    throw new InvalidOperationException("IdManutencao deve ser fornecido para faturas de manutenção.");

                // Verifica se a manutenção existe
                var manutencaoExiste = await _context.Manutencaos.AnyAsync(m => m.IdManutencao == faturaDto.IdManutencao);
                if (!manutencaoExiste)
                    throw new InvalidOperationException($"Manutenção com ID {faturaDto.IdManutencao} não encontrada.");

                fatura.IdManutencao = faturaDto.IdManutencao;
            }

            _context.Faturas.Add(fatura);
            await _context.SaveChangesAsync();

            return fatura;
        }

        // Atualiza uma Fatura existente
        public async Task<bool> UpdateAsync(int id, Fatura fatura)
        {
            var existing = await _context.Faturas.FindAsync(id);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(fatura);
            await _context.SaveChangesAsync();
            return true;
        }

        // Remove uma Fatura
        public async Task<bool> DeleteAsync(int id)
        {
            var fatura = await _context.Faturas.FindAsync(id);
            if (fatura == null)
                return false;

            _context.Faturas.Remove(fatura);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
