using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class TipoPagamentoService : ITipoPagamentoService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;

        public TipoPagamentoService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<TipoPagamento>> ObterTodosAsync()
        {
            return await _context.TipoPagamentos.ToListAsync();
        }

        public async Task<TipoPagamento?> ObterPorIdAsync(int id)
        {
            return await _context.TipoPagamentos.FindAsync(id);
        }

        public async Task CriarAsync(TipoPagamento tipoPagamento)
        {
            _context.TipoPagamentos.Add(tipoPagamento);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync(TipoPagamento tipoPagamento)
        {
            var exists = await _context.TipoPagamentos.AnyAsync(a => a.IdTipoPagamento == tipoPagamento.IdTipoPagamento);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização do Tipo de Pagamento!");

            _context.Entry(tipoPagamento).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Tipo de Pagamento atualizado com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var tipoPagamento = await _context.TipoPagamentos.FindAsync(id);
            if (tipoPagamento == null)
                return ResultadoOperacao.Falha("Falha na remoção do Tipo de Pagamento");
            _context.TipoPagamentos.Remove(tipoPagamento);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Tipo de Pagamento removido com sucesso");
        }
    }
}
