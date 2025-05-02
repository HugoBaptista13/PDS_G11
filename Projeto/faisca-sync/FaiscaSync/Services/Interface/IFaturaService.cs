using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface

{
    public interface IFaturaService
    {
        Task<List<Fatura>> ObterTodosAsync();
        Task<Fatura?> ObterPorIdAsync(int id);
        Task CriarAsync(Fatura fatura);
        Task<ResultadoOperacao> AtualizarAsync(Fatura fatura);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}
