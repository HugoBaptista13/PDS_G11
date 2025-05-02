using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface

{
    public interface IHistoricoEstadoVeiculoService
    {
        Task<List<HistoricoEstadoVeiculo>> ObterTodosAsync();
        Task<HistoricoEstadoVeiculo?> ObterPorIdAsync(int id);
        Task CriarAsync(HistoricoEstadoVeiculo historicoEstadoVeiculo);
        Task<ResultadoOperacao> AtualizarAsync(HistoricoEstadoVeiculo historicoEstadoVeiculo);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}
