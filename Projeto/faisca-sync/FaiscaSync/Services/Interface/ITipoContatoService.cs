using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface ITipoContatoService
    {
        Task<List<TipoContato>> ObterTodosAsync();
        Task<TipoContato?> ObterPorIdAsync(int id);
        Task CriarAsync(TipoContato tipoContato);
        Task<ResultadoOperacao> AtualizarAsync( TipoContato tipoContato);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}

