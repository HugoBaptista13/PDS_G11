using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Configuration;

namespace FaiscaSync.Services
{
    public class AquisicaoService : IAquisicaoService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;


        public AquisicaoService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Aquisicao>> ObterTodosAsync()
        {
            return await _context.Aquisicaos.ToListAsync();
        }

        public async Task<Aquisicao?> ObterPorIdAsync(int id)
        {
            return await _context.Aquisicaos.FindAsync(id);
        }

        public async Task CriarAsync(Aquisicao aquisicao)
        {
            _context.Aquisicaos.Add(aquisicao);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync(Aquisicao aquisicao)
        {

            var exists = await _context.Aquisicaos.AnyAsync(a => a.IdAquisicao == aquisicao.IdAquisicao);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização da Aquisição!");

            _context.Entry(aquisicao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Aquisição atualizada com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var aquisicao = await _context.Aquisicaos.FindAsync(id);
            if (aquisicao == null)
                return ResultadoOperacao.Falha("Falha na remoção da aquisição");
            _context.Aquisicaos.Remove(aquisicao);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Aquisição removida com sucesso");
        }
    }
}
