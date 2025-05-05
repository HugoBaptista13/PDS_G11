using FaiscaSync.DTO;
using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface IClienteService
    {
        Task<List<Cliente>> ObterTodosAsync();
        Task<Cliente?> ObterPorIdAsync(int id);
        Task CriarAsync(ClienteDTO clienteDto);
        Task<ResultadoOperacao> AtualizarAsync(int id, ClienteDTO clienteDto);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}
