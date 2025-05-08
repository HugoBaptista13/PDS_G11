using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface IMarcaService
    {
        Task<List<Marca>> ObterTodosAsync();
        Task<Marca?> ObterPorIdAsync(int id);
        Task CriarAsync(Marca marca);
        Task<ResultadoOperacao> AtualizarAsync(Marca marca);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}

