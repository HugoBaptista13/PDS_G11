using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface

{
    public interface IEstadoManutencaoService
    {
        Task<List<EstadoManutencao>> ObterTodosAsync();
        Task<EstadoManutencao?> ObterPorIdAsync(int id);
        Task CriarAsync(EstadoManutencao estadoManutencao);
        Task<ResultadoOperacao> AtualizarAsync( EstadoManutencao estadoManutencao);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}
