using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class ModeloService : IModeloService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;

        public ModeloService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Modelo>> ObterTodosAsync()
        {
            return await _context.Modelos.ToListAsync();
        }

        public async Task<Modelo?> ObterPorIdAsync(int id)
        {
            return await _context.Modelos.FindAsync(id);
        }

        public async Task CriarAsync(Modelo modelo)
        {
            _context.Modelos.Add(modelo);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync(Modelo modelo)
        {
            var exists = await _context.Modelos.AnyAsync(a => a.IdModelo == modelo.IdModelo);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização do Modelo!");

            _context.Entry(modelo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Modelo atualizado com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var modelo = await _context.Modelos.FindAsync(id);
            if ( modelo== null)
                return ResultadoOperacao.Falha("Falha na remoção da modelo");
            _context.Modelos.Remove(modelo);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Modelo removida com sucesso"); ;
        }
    }
}
