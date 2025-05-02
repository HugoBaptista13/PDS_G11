using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface

{
    public interface IEstadoGarantiaService
    {
        Task<List<EstadoGarantia>> ObterTodosAsync();
        Task<EstadoGarantia?> ObterPorIdAsync(int id);
        Task CriarAsync(EstadoGarantia estadoGarantia);
        Task<ResultadoOperacao> AtualizarAsync(EstadoGarantia estadoGarantia);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}
