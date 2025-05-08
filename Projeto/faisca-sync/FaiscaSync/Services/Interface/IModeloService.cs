using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface IModeloService
    {
        Task<List<Modelo>> ObterTodosAsync();
        Task<Modelo?> ObterPorIdAsync(int id);
        Task CriarAsync(Modelo modelo);
        Task<ResultadoOperacao>AtualizarAsync( Modelo modelo);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}

