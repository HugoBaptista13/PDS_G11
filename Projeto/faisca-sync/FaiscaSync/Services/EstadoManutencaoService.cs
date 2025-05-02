using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class EstadoManutencaoService : IEstadoManutencaoService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;


        public EstadoManutencaoService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<EstadoManutencao>> ObterTodosAsync()
        {
            return await _context.EstadoManutencaos.ToListAsync();
        }

        public async Task<EstadoManutencao?> ObterPorIdAsync(int id)
        {
            return await _context.EstadoManutencaos.FindAsync(id);
        }

        public async Task CriarAsync(EstadoManutencao estadoManutencao)
        {
            _context.EstadoManutencaos.Add(estadoManutencao);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync(EstadoManutencao estadoManutencao)
        {
            var exists = await _context.EstadoManutencaos.AnyAsync(a => a.IdEstadoManutencao == estadoManutencao.IdEstadoManutencao);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização do Estado de Manutenção!");

            _context.Entry(estadoManutencao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Estado de Manutenção atualizado com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var estadoManutencao = await _context.EstadoManutencaos.FindAsync(id);
            if (estadoManutencao == null)
                return ResultadoOperacao.Falha("Falha na remoção do estado de manutenção");
            _context.EstadoManutencaos.Remove(estadoManutencao);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("estado de manutenção removido com sucesso");
        }
    }
}
