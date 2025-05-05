using FaiscaSync.DTO;
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

        public async Task CriarAsync(ContatoDTO contatoDto)
        {
            var contato = new Contato
            {
                Detalhescontato = contatoDto.Contato,
                IdCliente = contatoDto.IdCliente,
                IdTipoContato = contatoDto.IdTipoContato,
                IdFuncionario = contatoDto.IdFuncionario
            };

            _context.Contatos.Add(contato);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync(int id, ContatoDTO contatoDto)
        {
            var contatoExistente = await _context.Contatos.FindAsync(id);

            if (contatoExistente == null)
                return ResultadoOperacao.Falha("Falha na atualização do Contato! Contato não encontrado.");

            contatoExistente.Detalhescontato = contatoDto.Contato;
            contatoExistente.IdCliente = contatoDto.IdCliente;
            contatoExistente.IdTipoContato = contatoDto.IdTipoContato;
            contatoExistente.IdFuncionario = contatoDto.IdFuncionario;

            _context.Entry(contatoExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Contato atualizado com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var contato = await _context.Contatos.FindAsync(id);
            if (contato == null)
                return ResultadoOperacao.Falha("Falha na remoção do contato");
            _context.Contatos.Remove(contato);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Contato removido com sucesso");
        }
    }
}
