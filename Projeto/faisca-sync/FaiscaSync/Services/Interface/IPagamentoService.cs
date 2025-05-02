using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface IPagamentoService
    {
        Task<List<Pagamento>> ObterTodosAsync();
        Task<Pagamento?> ObterPorIdAsync(int id);
        Task CriarAsync(Pagamento pagamento);
        Task<ResultadoOperacao> AtualizarAsync( Pagamento pagamento);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}

