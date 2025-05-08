using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface IMotorService
    {
        Task<List<Motor>> ObterTodosAsync();
        Task<Motor?> ObterPorIdAsync(int id);
        Task CriarAsync(Motor motor);
        Task<ResultadoOperacao> AtualizarAsync(Motor motor);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}

