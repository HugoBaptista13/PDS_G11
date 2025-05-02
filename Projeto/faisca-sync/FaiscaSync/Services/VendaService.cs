using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace FaiscaSync.Services
{
    public class VendaService : IVendaService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;


        public VendaService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }
        public async Task<List<Venda>> ObterTodosAsync()
        {
            return await _context.Vendas.ToListAsync();
        }

        public async Task<Venda?> ObterPorIdAsync(int id)
        {
            return await _context.Vendas.FindAsync(id);
        }

        public async Task CriarAsync (Venda venda)
        {
            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();
        }
        public async Task<ResultadoOperacao> AtualizarAsync(Venda venda)
        {

            var exists = await _context.Vendas.AnyAsync(a => a.IdVendas == venda.IdVendas);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização da Venda!");

            _context.Entry(venda).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Venda atualizada com Sucesso!");
        }
        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null)
                return ResultadoOperacao.Falha("Falha na remoção da venda");
            _context.Vendas.Remove(venda);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Venda removida com sucesso");
        }


    }
}
