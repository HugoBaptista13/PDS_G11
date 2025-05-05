using FaiscaSync.DTO;
using FaiscaSync.Models;

namespace FaiscaSync.Services.Interface
{
    public interface IVeiculoService
    {
        Task<List<Veiculo>> ObterTodosAsync();
        Task<Veiculo?> ObterPorIdAsync(int id);
        Task CriarAsync(VeiculoDTO veiculoDto);
        Task<ResultadoOperacao> AtualizarAsync(int id, VeiculoDTO veiculoDto);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}
