using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class GarantiaService : IGarantiaService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;

        public GarantiaService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Garantia>> ObterTodosAsync()
        {
            return await _context.Garantia.ToListAsync();
        }

        public async Task<Garantia?> ObterPorIdAsync(int id)
        {
            return await _context.Garantia.FindAsync(id);
        }

        public async Task CriarAsync(Garantia garantium)
        {
            _context.Garantia.Add(garantium);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync(Garantia garantia)
        {
            var exists = await _context.Garantia.AnyAsync(a => a.IdGarantia == garantia.IdGarantia);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização da Garantia!");

            _context.Entry(garantia).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Garantia atualizada com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var garantia = await _context.Garantia.FindAsync(id);
            if (garantia == null)
                return ResultadoOperacao.Falha("Falha na remoção da garantia");
            _context.Garantia.Remove(garantia);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Garantia removida com sucesso");
        }
    }
}
