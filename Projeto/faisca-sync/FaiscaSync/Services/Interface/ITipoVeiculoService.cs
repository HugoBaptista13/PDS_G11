using FaiscaSync.DTO;
using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface ITipoVeiculoService
    {
        Task<List<TipoVeiculo>> ObterTodosAsync();
        Task<TipoVeiculo?> ObterPorIdAsync(int id);
        Task CriarAsync(TipoVeiculoDTO tipoVeiculoDto);
        Task<ResultadoOperacao> AtualizarAsync(int id, TipoVeiculoDTO tipoVeiculoDto);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}

