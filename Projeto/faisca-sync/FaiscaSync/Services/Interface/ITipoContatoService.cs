using FaiscaSync.DTO;
using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface ITipoContatoService
    {
        Task<List<TipoContato>> ObterTodosAsync();
        Task<TipoContato?> ObterPorIdAsync(int id);
        Task CriarAsync(TipoContatoDTO tipoContatoDto);
        Task<ResultadoOperacao> AtualizarAsync(int id,TipoContatoDTO tipoContatoDto);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}

