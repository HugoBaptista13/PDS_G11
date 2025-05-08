using FaiscaSync.Models;
using FaiscaSync.DTO;

namespace FaiscaSync.Services.Interface
{
    public interface IVeiculoService
    {
        Task<List<Veiculo>> ObterTodosAsync();
        Task<Veiculo?> ObterPorIdAsync(int id);
        Task CriarAsync(Veiculo veiculo);
        Task<ResultadoOperacao> AtualizarAsync( Veiculo veiculo);
        Task<ResultadoOperacao> RemoverAsync(int id);
        Task<IEnumerable<Veiculo>> PesquisaAvancadaAsync(PesquisaVeiculoDTO filtro);
        Task<IEnumerable<Veiculo>> ObterVeiculosEmDestaqueAsync();

        Task<List<Veiculo>> ObterVeiculosDisponiveisAsync();


    }
}
