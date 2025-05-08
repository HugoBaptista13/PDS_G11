using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace FaiscaSync.Services
{
    public class ContatoService : IContatoService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;


        public ContatoService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        public async Task<List<Contato>> ObterTodosAsync()
        {
            return await _context.Contatos.ToListAsync();
        }

        public async Task<Contato?> ObterPorIdAsync(int id)
        {
            return await _context.Contatos.FindAsync(id);
        }

        public async Task CriarAsync(Contato contato)
        {
            _context.Contatos.Add(contato);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync(Contato contatos)
        {
            var exists = await _context.Contatos.AnyAsync(c => c.Idcontato == contatos.Idcontato);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização da Aquisição!");

            _context.Entry(contatos).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Aquisição atualizada com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var contato = await _context.Contatos.FindAsync(id);
            if (contato == null)
                return ResultadoOperacao.Falha("Falha na remoção da aquisição");
            _context.Contatos.Remove(contato);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Aquisição removida com sucesso");
        }
    }
}
