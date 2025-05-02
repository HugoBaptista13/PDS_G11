using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface

{
    public interface IGarantiaService
    {
        Task<List<Garantia>> ObterTodosAsync();
        Task<Garantia?> ObterPorIdAsync(int id);
        Task CriarAsync(Garantia garantium);
        Task<ResultadoOperacao> AtualizarAsync( Garantia garantium);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}
