using FaiscaSync.Models;
namespace FaiscaSync.Services.Interface

{
    public interface ICoberturaGarantiaService
    {
        Task<List<CoberturaGarantia>> ObterTodosAsync();
        Task<CoberturaGarantia?> ObterPorIdAsync(int id);
        Task CriarAsync(CoberturaGarantia coberturaGarantia);
        Task<ResultadoOperacao> AtualizarAsync(CoberturaGarantia coberturaGarantia);
        Task<ResultadoOperacao> RemoverAsync(int id);
    }
}
