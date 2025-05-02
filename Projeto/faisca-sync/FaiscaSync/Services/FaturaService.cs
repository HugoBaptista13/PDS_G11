using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class FaturaService : IFaturaService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;


        public FaturaService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Fatura>> ObterTodosAsync()
        {
            return await _context.Faturas.ToListAsync();
        }

        public async Task<Fatura?> ObterPorIdAsync(int id)
        {
            return await _context.Faturas.FindAsync(id);
        }

        public async Task CriarAsync(Fatura fatura)
        {
            _context.Faturas.Add(fatura);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync( Fatura fatura)
        {
            var exists = await _context.Faturas.AnyAsync(a => a.IdFatura == fatura.IdFatura);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização da Fatura!");

            _context.Entry(fatura).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Fatura atualizada com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var fatura = await _context.Faturas.FindAsync(id);
            if (fatura == null)
                return ResultadoOperacao.Falha("Falha na remoção da fatura");
            _context.Faturas.Remove(fatura);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Fatura removida com sucesso");
        }
    }
}
