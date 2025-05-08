using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class TipoContatoService : ITipoContatoService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;

        public TipoContatoService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<TipoContato>> ObterTodosAsync()
        {
            return await _context.TipoContatos.ToListAsync();
        }

        public async Task<TipoContato?> ObterPorIdAsync(int id)
        {
            return await _context.TipoContatos.FindAsync(id);
        }

        public async Task CriarAsync(TipoContato tipoContato)
        {
            _context.TipoContatos.Add(tipoContato);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync(TipoContato tipoContato)
        {
            var exists = await _context.TipoContatos.AnyAsync(a => a.IdTipoContato == tipoContato.IdTipoContato);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização do Tipo de Contactos!");

            _context.Entry(tipoContato).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Tipo de Contato atualizado com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var tipoContato = await _context.TipoContatos.FindAsync(id);
            if (tipoContato == null)
                return ResultadoOperacao.Falha("Falha na remoção do Tipo Contactos");
            _context.TipoContatos.Remove(tipoContato);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Tipo de Contactos removida com sucesso");
        }
    }
}
