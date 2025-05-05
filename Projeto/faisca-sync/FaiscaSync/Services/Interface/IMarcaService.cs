using FaiscaSync.DTO;
using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface IMarcaService
    {
        Task<List<Marca>> ObterTodosAsync();
        Task<Marca?> ObterPorIdAsync(int id);
        Task CriarAsync(MarcaDTO marcaDto);
        Task<ResultadoOperacao> AtualizarAsync(int id, MarcaDTO marcaDto);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}

