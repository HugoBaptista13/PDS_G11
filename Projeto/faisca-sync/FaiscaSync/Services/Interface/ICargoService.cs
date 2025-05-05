using FaiscaSync.DTO;
using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface ICargoService
    {
        Task<List<Cargo>> ObterTodosAsync();
        Task<Cargo?> ObterPorIdAsync(int id);
        Task CriarAsync(CargoDTO cargoDto);
        Task<ResultadoOperacao> AtualizarAsync(int id, CargoDTO cargoDto);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}

