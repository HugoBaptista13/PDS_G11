using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface ICargoService
    {
        Task<List<Cargo>> ObterTodosAsync();
        Task<Cargo?> ObterPorIdAsync(int id);
        Task CriarAsync(Cargo cargo);
        Task<ResultadoOperacao> AtualizarAsync(Cargo cargo);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}

