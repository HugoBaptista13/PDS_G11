using FaiscaSync.DTO;
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

        public async Task CriarAsync(CargoDTO cargoDto)
        {
            // Converter o DTO para a entidade Cargo
            var cargo = new Cargo
            {
                Nomecargo = cargoDto.Nomecargo
            };
            _context.Cargos.Add(cargo);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync(int id, CargoDTO cargoDto)
        {
            var cargoExistente = await _context.Cargos.FindAsync(id);
            if (cargoExistente == null)
                return ResultadoOperacao.Falha("Falha na atualização do Cargo! Cargo não encontrado.");

            // Atualizar os campos do cargo com os valores do DTO
            cargoExistente.Nomecargo = cargoDto.Nomecargo;

            // Marcar a entidade como modificada
            _context.Entry(cargoExistente).State = EntityState.Modified;

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
