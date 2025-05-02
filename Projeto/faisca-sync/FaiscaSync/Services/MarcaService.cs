using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class MarcaService : IMarcaService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;

        public MarcaService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Marca>> ObterTodosAsync()
        {
            return await _context.Marcas.ToListAsync();
        }

        public async Task<Marca?> ObterPorIdAsync(int id)
        {
            return await _context.Marcas.FindAsync(id);
        }

        public async Task CriarAsync(Marca marca)
        {
            _context.Marcas.Add(marca);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync( Marca marca)
        {
            var exists = await _context.Marcas.AnyAsync(a => a.IdMarca == marca.IdMarca);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização da Marca!");

            _context.Entry(marca).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Marca atualizada com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null)
                return ResultadoOperacao.Falha("Falha na remoção da Marca");
            _context.Marcas.Remove(marca);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Marca removida com sucesso");
        }
    }
}
