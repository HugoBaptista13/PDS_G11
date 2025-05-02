using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class HistoricoEstadoVeiculoService : IHistoricoEstadoVeiculoService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;

        public HistoricoEstadoVeiculoService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<HistoricoEstadoVeiculo>> ObterTodosAsync()
        {
            return await _context.HistoricoEstadoVeiculos.ToListAsync();
        }

        public async Task<HistoricoEstadoVeiculo?> ObterPorIdAsync(int id)
        {
            return await _context.HistoricoEstadoVeiculos.FindAsync(id);
        }

        public async Task CriarAsync(HistoricoEstadoVeiculo historicoEstadoVeiculo)
        {
            _context.HistoricoEstadoVeiculos.Add(historicoEstadoVeiculo);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync(HistoricoEstadoVeiculo historicoEstadoVeiculo)
        {
            var exists = await _context.HistoricoEstadoVeiculos.AnyAsync(a => a.IdHistEstadoVeiculo == historicoEstadoVeiculo.IdHistEstadoVeiculo);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização do Histórico do Estado do Veiculo!");

            _context.Entry(historicoEstadoVeiculo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Histórico do Estado do Veiculo atualizado com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var historicoEstadoVeiculo = await _context.HistoricoEstadoVeiculos.FindAsync(id);
            if (historicoEstadoVeiculo == null)
                return ResultadoOperacao.Falha("Falha na remoção do Hist Estado Veiculo");
            _context.HistoricoEstadoVeiculos.Remove(historicoEstadoVeiculo);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Historico Estado do Veiculo removida com sucesso");
        }
    }
}
