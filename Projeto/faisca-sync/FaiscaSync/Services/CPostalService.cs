using FaiscaSync.DTO;
using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class CPostalService : ICPostalService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;


        public CPostalService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Cpostal>> ObterTodosAsync()
        {
            return await _context.Cpostals.ToListAsync();
        }

        public async Task<Cpostal?> ObterPorIdAsync(int id)
        {
            return await _context.Cpostals.FindAsync(id);
        }

        public async Task CriarAsync(CpostalDTO cpostalDto)
        {
            // Converte o DTO para a entidade Cpostal
            var cpostal = new Cpostal
            {
                Localidade = cpostalDto.Localidade
            };

            // Adiciona a entidade ao contexto e salva as alterações
            _context.Cpostals.Add(cpostal);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync(int id, CpostalDTO cpostalDto)
        {

            var codPostExistente = await _context.Cpostals.FindAsync(id);
            if (codPostExistente == null)
                return ResultadoOperacao.Falha("Falha na atualização do Codigo Postal! Codigo Postal não encontrado.");

            codPostExistente.Localidade = cpostalDto.Localidade;

            _context.Entry(codPostExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Código Postal atualizado com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var cpostal = await _context.Cpostals.FindAsync(id);
            if (cpostal == null)
                return ResultadoOperacao.Falha("Falha na remoção do Código Postal");
            _context.Cpostals.Remove(cpostal);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Código Postal removido com sucesso");
        }
    }
}
