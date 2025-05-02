using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface IMoradaService
    {
        Task<List<Morada>> ObterTodosAsync();
        Task<Morada?> ObterPorIdAsync(int id);
        Task CriarAsync(Morada morada);
        Task<ResultadoOperacao> AtualizarAsync( Morada morada);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}

