using FaiscaSync.DTO;
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

        public async Task CriarAsync(MarcaDTO marcaDto)
        {
            var marca = new Marca
            {
                Descricaomarca = marcaDto.Marca
            };
            _context.Marcas.Add(marca);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync(int id, MarcaDTO marcaDto)
        {
            var marcaExistente = await _context.Marcas.FindAsync(id);

            if (marcaExistente == null)
                return ResultadoOperacao.Falha("Falha na atualização da Marca! Marca não encontrada.");

            marcaExistente.Descricaomarca = marcaDto.Marca;

            _context.Entry(marcaExistente).State = EntityState.Modified;
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
