using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface ITipoVeiculoService
    {
        Task<List<TipoVeiculo>> ObterTodosAsync();
        Task<TipoVeiculo?> ObterPorIdAsync(int id);
        Task CriarAsync(TipoVeiculo tipoVeiculo);
        Task<ResultadoOperacao> AtualizarAsync( TipoVeiculo tipoVeiculo);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}

