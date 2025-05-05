using FaiscaSync.DTO;
using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface IMotorService
    {
        Task<List<Motor>> ObterTodosAsync();
        Task<Motor?> ObterPorIdAsync(int id);
        Task CriarAsync(MotorDTO motorDto);
        Task<ResultadoOperacao> AtualizarAsync(int id, MotorDTO motorDto);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}

