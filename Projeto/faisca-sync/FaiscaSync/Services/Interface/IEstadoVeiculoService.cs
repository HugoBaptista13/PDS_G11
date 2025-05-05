using FaiscaSync.DTO;
using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface

{
    public interface IEstadoVeiculoService
    {
        Task<List<EstadoVeiculo>> ObterTodosAsync();
        Task<EstadoVeiculo?> ObterPorIdAsync(int id);
        Task CriarAsync(EstadoVeiculoDTO estadoVeiculo);
        Task<ResultadoOperacao> AtualizarAsync(int id, EstadoVeiculoDTO estadoVeiculoDto);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}
