using FaiscaSync.DTO;
using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface IMoradaService
    {
        Task<List<Morada>> ObterTodosAsync();
        Task<Morada?> ObterPorIdAsync(int id);
        Task CriarAsync(MoradaDTO moradaDto);
        Task<ResultadoOperacao> AtualizarAsync(int id, MoradaDTO moradaDto);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}

