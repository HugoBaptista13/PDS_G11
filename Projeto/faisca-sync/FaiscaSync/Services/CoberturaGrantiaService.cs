using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace FaiscaSync.Services
{
    public class CoberturaGarantiaService : ICoberturaGarantiaService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;


        public CoberturaGarantiaService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        public async Task<List<CoberturaGarantia>> ObterTodosAsync()
        {
            return await _context.CoberturaGarantia.ToListAsync();
        }

        public async Task<CoberturaGarantia?> ObterPorIdAsync(int id)
        {
            return await _context.CoberturaGarantia.FindAsync(id);
        }

        public async Task CriarAsync(CoberturaGarantia coberturaGarantia)
        {
            _context.CoberturaGarantia.Add(coberturaGarantia);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync(CoberturaGarantia coberturaGarantium)
        {
            var exists = await _context.CoberturaGarantia.AnyAsync(cg => cg.IdCoberturaGarantia == coberturaGarantium.IdCoberturaGarantia);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização da Cobertura de Grantia!");

            _context.Entry(coberturaGarantium).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Cobertura de Garantia atualizada com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var coberturaGarantia = await _context.CoberturaGarantia.FindAsync(id);
            if (coberturaGarantia == null)
                return ResultadoOperacao.Falha("Falha na remoção da Cobertura de Garantia");
            _context.CoberturaGarantia.Remove(coberturaGarantia);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Cobertura de Grantia    removida com sucesso");
        }
    }
}
