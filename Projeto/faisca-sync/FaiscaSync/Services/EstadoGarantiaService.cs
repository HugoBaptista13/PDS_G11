using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class EstadoGarantiaService : IEstadoGarantiaService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;

        public EstadoGarantiaService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<EstadoGarantia>> ObterTodosAsync()
        {
            return await _context.EstadoGarantia.ToListAsync();
        }

        public async Task<EstadoGarantia?> ObterPorIdAsync(int id)
        {
            return await _context.EstadoGarantia.FindAsync(id);
        }

        public async Task CriarAsync(EstadoGarantia estadoGarantium)
        {
            _context.EstadoGarantia.Add(estadoGarantium);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync(EstadoGarantia estadoGarantium)
        {
            var exists = await _context.EstadoGarantia.AnyAsync(eg => eg.IdEstadoGarantia == estadoGarantium.IdEstadoGarantia);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização da Estado Garantia!");

            _context.Entry(estadoGarantium).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Estado Garantia atualizada com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var estadoGaratium = await _context.EstadoGarantia.FindAsync(id);
            if (estadoGaratium == null)
                return ResultadoOperacao.Falha("Falha na remoção do Estado de Garantia");
            _context.EstadoGarantia.Remove(estadoGaratium);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Estado de Garantia removida com sucesso");
        }
    }
}
