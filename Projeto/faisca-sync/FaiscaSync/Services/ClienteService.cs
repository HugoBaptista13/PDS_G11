using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Configuration;

namespace FaiscaSync.Services
{
    public class ClienteService : IClienteService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;


        public ClienteService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Cliente>> ObterTodosAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> ObterPorIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task CriarAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync( Cliente cliente)
        {
            var exists = await _context.Clientes.AnyAsync(c => c.IdCliente == cliente.IdCliente);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização do Cliente!");

            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Cliente atualizado com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return ResultadoOperacao.Falha("Falha na remoção do Cliente");

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Cliente removido com sucesso");
        }
    }
}
