using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface

{
    public interface IEstadoVeiucloService
    {
        Task<List<EstadoVeiculo>> ObterTodosAsync();
        Task<EstadoVeiculo?> ObterPorIdAsync(int id);
        Task CriarAsync(EstadoVeiculo estadoVeiculo);
        Task<ResultadoOperacao> AtualizarAsync(EstadoVeiculo estadoVeiculo);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}
