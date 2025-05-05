using FaiscaSync.DTO;
using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface IAquisicaoService
    {
        Task<List<Aquisicao>> ObterTodosAsync();
        Task<Aquisicao?> ObterPorIdAsync(int id);
        Task CriarAsync(AquisicaoDTO aquisicaoDto);
        Task<ResultadoOperacao> AtualizarAsync(int id, AquisicaoDTO aquisicaoDto);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}

