using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface ITipoPagamentoService
    {
        Task<List<TipoPagamento>> ObterTodosAsync();
        Task<TipoPagamento?> ObterPorIdAsync(int id);
        Task CriarAsync(TipoPagamento tipoPagamento);
        Task<ResultadoOperacao> AtualizarAsync( TipoPagamento tipoPagamento);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}

