using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface IAquisicaoService
    {
        Task<List<Aquisicao>> ObterTodosAsync();
        Task<Aquisicao?> ObterPorIdAsync(int id);
        Task CriarAsync(Aquisicao aquisicao);
        Task<ResultadoOperacao> AtualizarAsync(Aquisicao aquisicao);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}

