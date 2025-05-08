using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Configuration;

namespace FaiscaSync.Services
{
    public class CargoService : ICargoService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;


        public CargoService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        public async Task<List<Cargo>> ObterTodosAsync()
        {
            return await _context.Cargos.ToListAsync();
        }

        public async Task<Cargo?> ObterPorIdAsync(int id)
        {
            return await _context.Cargos.FindAsync(id);
        }

        public async Task CriarAsync(Cargo cargo)
        {
            _context.Cargos.Add(cargo);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync(Cargo cargo)
        {
            var exists = await _context.Cargos.AnyAsync(c => c.IdCargo == cargo.IdCargo);
            if (!exists)
                return ResultadoOperacao.Falha("Falha na atualização do Cargo!");

            _context.Entry(cargo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Cargo atualizado com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var cargo = await _context.Cargos.FindAsync(id);
            if (cargo == null)
                return ResultadoOperacao.Falha("Falha na remoção do Cargo");
            _context.Cargos.Remove(cargo);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Cargo removido com sucesso");
        }
    }
}
