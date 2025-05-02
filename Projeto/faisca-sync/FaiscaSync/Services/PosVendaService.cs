using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class PosVendaService : IPosVendaService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;

        public PosVendaService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<PosVenda>> ObterTodosAsync()
        {
            return await _context.PosVenda.ToListAsync();
        }

        public async Task<PosVenda?> ObterPorIdAsync(int id)
        {
            return await _context.PosVenda.FindAsync(id);
        }

        public async Task CriarAsync(PosVenda posVenda)
        {
            _context.PosVenda.Add(posVenda);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync( PosVenda posVenda)
        {
            var exists = await _context.PosVenda.AnyAsync(a => a.IdPosVenda == posVenda.IdPosVenda);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização da Pos Venda!");

            _context.Entry(posVenda).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Pos Venda atualizada com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var posVenda = await _context.PosVenda.FindAsync(id);
            if (posVenda == null)
                return ResultadoOperacao.Falha("Falha na remoção da Pos Venda");
            _context.PosVenda.Remove(posVenda);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Pos Venda removida com sucesso");
        }
    }
}
