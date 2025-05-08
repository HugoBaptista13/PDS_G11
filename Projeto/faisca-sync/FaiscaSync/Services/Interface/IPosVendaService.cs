using FaiscaSync.DTO;
using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface IPosVendaService
    {
        Task<List<PosVenda>> ObterTodosAsync();
        Task<PosVenda?> ObterPorIdAsync(int id);
        Task CriarAsync(PosVenda posVenda);
        Task<ResultadoOperacao> AtualizarAsync( PosVenda posVenda);
        Task<ResultadoOperacao> RemoverAsync(int id);

        Task<ResultadoOperacao> CriarSolicitacaoAsync(CriarPosVendaDTO dto);
 
    }
}

