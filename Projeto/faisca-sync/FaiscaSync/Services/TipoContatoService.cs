using FaiscaSync.DTO;
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

        public async Task CriarAsync(TipoContatoDTO tipoContatoDto)
        {
            var tipoContato = new TipoContato
            {
                Descricaotipocontato = tipoContatoDto.TipoContato,
            };

            _context.TipoContatos.Add(tipoContato);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync(int id, TipoContatoDTO tipoContatoDto)
        {
            var tipoContatoExistente = await _context.TipoContatos.FindAsync(id);

            if (tipoContatoExistente == null)
                return ResultadoOperacao.Falha("Falha na atualização do Tipo de Contacto! Tipo de Contacto não encontrado.");

            tipoContatoExistente.Descricaotipocontato = tipoContatoDto.TipoContato;

            _context.Entry(tipoContatoExistente).State = EntityState.Modified;
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
            return ResultadoOperacao.Ok("Tipo de Contactos removido com sucesso");
        }
    }
}
