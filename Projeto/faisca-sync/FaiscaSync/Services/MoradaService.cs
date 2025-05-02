using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class MoradaService : IMoradaService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;

        public MoradaService(FsContext context, IConfiguration configuration )
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Morada>> ObterTodosAsync()
        {
            return await _context.Morada.ToListAsync();
        }

        public async Task<Morada?> ObterPorIdAsync(int id)
        {
            return await _context.Morada.FindAsync(id);
        }

        public async Task CriarAsync(Morada morada)
        {
            _context.Morada.Add(morada);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync( Morada morada)
        {
            var exists = await _context.Morada.AnyAsync(a => a.IdMorada == morada.IdMorada);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização da Morada!");

            _context.Entry(morada).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Morada atualizada com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var morada = await _context.Morada.FindAsync(id);
            if (morada == null)
                return ResultadoOperacao.Falha("Falha na remoção da morada");
            _context.Morada.Remove(morada);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Morada removida com sucesso");
        }
    }
}
