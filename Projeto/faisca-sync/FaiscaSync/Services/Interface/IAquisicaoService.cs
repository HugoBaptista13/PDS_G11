using FaiscaSync.DTO;
using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface
{
    public interface IAquisicaoService
    {
        Task<List<Aquisicao>> ObterTodosAsync();
        Task<Aquisicao?> ObterPorIdAsync(int id);
        Task CriarAsync(Aquisicao aquisicao);
        Task<ResultadoOperacao> AtualizarAsync(Aquisicao aquisicao);
        Task<ResultadoOperacao> RemoverAsync(int id);


        /// <summary>
        /// Processo de negócio Aquisicao
        /// </summary>
        /// <param name="funcionarioId"></param>
        /// <returns></returns>
        Task<ResultadoOperacao> SubmeterPedidoAsync(int funcionarioId);
        Task<ResultadoOperacao> RejeitarFinanceiroAsync(int aquisicaoId);
        Task<ResultadoOperacao> AprovarAdminAsync(int aquisicaoId, NovoVeiculoDTO dto);
    }
}

