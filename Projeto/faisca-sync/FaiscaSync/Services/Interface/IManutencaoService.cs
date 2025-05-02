using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface IManutencaoService
    {
        Task<List<Manutencao>> ObterTodosAsync();
        Task<Manutencao?> ObterPorIdAsync(int id);
        Task CriarAsync(Manutencao manutencao);
        Task<ResultadoOperacao> AtualizarAsync( Manutencao manutencao);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}

