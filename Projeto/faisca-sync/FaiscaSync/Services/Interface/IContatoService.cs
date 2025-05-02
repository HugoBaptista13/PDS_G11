using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface

{
    public interface IContatoService
    {
        Task<List<Contato>> ObterTodosAsync();
        Task<Contato?> ObterPorIdAsync(int id);
        Task CriarAsync(Contato contato);
        Task<ResultadoOperacao> AtualizarAsync(Contato contato);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}
