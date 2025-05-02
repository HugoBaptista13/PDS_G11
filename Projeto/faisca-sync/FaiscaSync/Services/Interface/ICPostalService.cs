using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface

{
    public interface ICPostalService
    {
        Task<List<Cpostal>> ObterTodosAsync();
        Task<Cpostal?> ObterPorIdAsync(int id);
        Task CriarAsync(Cpostal cpostal);
        Task<ResultadoOperacao> AtualizarAsync(Cpostal cpostal);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}
