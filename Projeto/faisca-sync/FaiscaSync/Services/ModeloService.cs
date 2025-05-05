using FaiscaSync.DTO;
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

        public async Task CriarAsync(ModeloDTO modeloDto)
        {
            var modelo = new Modelo
            {
                Nomemodelo = modeloDto.NomeModelo,
                IdMarca = modeloDto.IdMarca
            };
            _context.Modelos.Add(modelo);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync(int id, ModeloDTO modeloDto)
        {
            var modeloExistente = await _context.Modelos.FindAsync(id);

            if (modeloExistente == null)
                return ResultadoOperacao.Falha("Falha na atualização do Modelo! Modelo não encontrado.");

            modeloExistente.Nomemodelo = modeloDto.NomeModelo;
            modeloExistente.IdMarca = modeloDto.IdMarca;

            _context.Entry(modeloExistente).State = EntityState.Modified;
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
            return ResultadoOperacao.Ok("Modelo removido com sucesso"); ;
        }
    }
}
