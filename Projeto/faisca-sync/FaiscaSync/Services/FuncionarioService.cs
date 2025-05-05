using FaiscaSync.DTO;
using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;


        public FuncionarioService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Funcionario>> ObterTodosAsync()
        {
            return await _context.Funcionarios.ToListAsync();
        }

        public async Task<Funcionario?> ObterPorIdAsync(int id)
        {
            return await _context.Funcionarios.FindAsync(id);
        }

        public async Task CriarAsync(FuncionarioDTO funcionarioDto)
        {
            var funcionario = new Funcionario
            {
                Nome = funcionarioDto.Nome,
                Datacontratacao = funcionarioDto.DataContrato,
                Datanascimento = funcionarioDto.DataNasc,
                IdCargo = funcionarioDto.IdCargo
            };
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync(int id, FuncionarioDTO funcionarioDto)
        {
            var funcionarioExistente = await _context.Funcionarios.FindAsync(id);

            if (funcionarioExistente == null)
                return ResultadoOperacao.Falha("Falha na atualização do Funcionário! Funcionário não encontrado.");

            funcionarioExistente.Nome = funcionarioDto.Nome;
            funcionarioExistente.Datacontratacao = funcionarioDto.DataContrato;
            funcionarioExistente.Datanascimento = funcionarioDto.DataNasc;
            funcionarioExistente.IdCargo = funcionarioDto.IdCargo;

            _context.Entry(funcionarioExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Funcionário atualizado com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
                return ResultadoOperacao.Falha("Falha na remoção do Funcionário");
            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Funcionário removido com sucesso");
        }
    }
}
