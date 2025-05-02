using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class CPostalService : ICPostalService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;


        public CPostalService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Cpostal>> ObterTodosAsync()
        {
            return await _context.Cpostals.ToListAsync();
        }

        public async Task<Cpostal?> ObterPorIdAsync(int id)
        {
            return await _context.Cpostals.FindAsync(id);
        }

        public async Task CriarAsync(Cpostal cpostal)
        {
            _context.Cpostals.Add(cpostal);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync(Cpostal cpostal)
        {
            var exists = await _context.Cpostals.AnyAsync(cp => cp.IdCpostal == cpostal.IdCpostal);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização do Codigo Postal!");

            _context.Entry(cpostal).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Código Postal atualizado com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var cpostal = await _context.Cpostals.FindAsync(id);
            if (cpostal == null)
                return ResultadoOperacao.Falha("Falha na remoção do Código Postal");
            _context.Cpostals.Remove(cpostal);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Código Postal removido com sucesso");
        }
    }
}
