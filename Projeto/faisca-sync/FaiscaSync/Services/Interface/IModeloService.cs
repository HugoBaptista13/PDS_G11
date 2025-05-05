using FaiscaSync.DTO;
using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface IModeloService
    {
        Task<List<Modelo>> ObterTodosAsync();
        Task<Modelo?> ObterPorIdAsync(int id);
        Task CriarAsync(ModeloDTO modeloDto);
        Task<ResultadoOperacao>AtualizarAsync(int id, ModeloDTO modeloDto);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}

