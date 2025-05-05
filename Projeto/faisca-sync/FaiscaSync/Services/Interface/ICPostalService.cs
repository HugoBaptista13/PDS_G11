using FaiscaSync.DTO;
using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface

{
    public interface ICPostalService
    {
        Task<List<Cpostal>> ObterTodosAsync();
        Task<Cpostal?> ObterPorIdAsync(int id);
        Task CriarAsync(CpostalDTO cpostalDto);
        Task<ResultadoOperacao> AtualizarAsync(int id, CpostalDTO cpostalDto);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}
