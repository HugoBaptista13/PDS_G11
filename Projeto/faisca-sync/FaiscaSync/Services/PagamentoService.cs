using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class PagamentoService : IPagamentoService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;

        public PagamentoService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Pagamento>> ObterTodosAsync()
        {
            return await _context.Pagamentos.ToListAsync();
        }

        public async Task<Pagamento?> ObterPorIdAsync(int id)
        {
            return await _context.Pagamentos.FindAsync(id);
        }

        public async Task CriarAsync(Pagamento pagamento)
        {
            _context.Pagamentos.Add(pagamento);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync( Pagamento pagamento)
        {
            var exists = await _context.Pagamentos.AnyAsync(a => a.IdPagamento == pagamento.IdPagamento);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização do Pagamento!");

            _context.Entry(pagamento).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Pagamento atualizada com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var pagamento = await _context.Pagamentos.FindAsync(id);
            if (pagamento == null)
                return ResultadoOperacao.Falha("Falha na remoção do Pagamento");
            _context.Pagamentos.Remove(pagamento);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Pagamento removido com sucesso");
        }
    }
}
