using FaiscaSync.DTO;
using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class VeiculoService : IVeiculoService
    {
        private readonly FsContext _context;
        private readonly IConfiguration _configuration;

        public VeiculoService(FsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Veiculo>> ObterTodosAsync()
        {
            return await _context.Veiculos.ToListAsync();
        }

        public async Task<Veiculo?> ObterPorIdAsync(int id)
        {
            return await _context.Veiculos.FindAsync(id);
        }

        public async Task CriarAsync(VeiculoDTO veiculoDto)
        {
            var veiculo = new Veiculo
            {
                Matricula = veiculoDto.Matricula,
                Chassi = veiculoDto.Chassi,
                Anofabrico = veiculoDto.AnoFabrico,
                Cor = veiculoDto.Cor,
                Quilometros = veiculoDto.Quilometragem,
                Preco = veiculoDto.Preco,
                IdMotor = veiculoDto.IdMotor,
                IdEstadoVeiculo = veiculoDto.IdEstadoVeiculo,
                IdTipoVeiculo = veiculoDto.IdTipoVeiculo,
                IdModelo = veiculoDto.IdModelo,
                IdAquisicao = veiculoDto.IdAquisicao

            };

            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoOperacao> AtualizarAsync(int id, VeiculoDTO veiculoDto)
        {
            var veiculoExistente = await _context.Veiculos.FindAsync(id);

            if (veiculoExistente == null)
                return ResultadoOperacao.Falha("Falha na atualização do Veiculo! Veiculo não encontrado.");

            veiculoExistente.Matricula = veiculoDto.Matricula;
            veiculoExistente.Chassi = veiculoDto.Chassi;
            veiculoExistente.Anofabrico = veiculoDto.AnoFabrico;
            veiculoExistente.Cor = veiculoDto.Cor;
            veiculoExistente.Quilometros = veiculoDto.Quilometragem;
            veiculoExistente.Preco = veiculoDto.Preco;
            veiculoExistente.IdMotor = veiculoDto.IdMotor;
            veiculoExistente.IdEstadoVeiculo = veiculoDto.IdEstadoVeiculo;
            veiculoExistente.IdTipoVeiculo = veiculoDto.IdTipoVeiculo;
            veiculoExistente.IdModelo = veiculoDto.IdModelo;
            veiculoExistente.IdAquisicao = veiculoDto.IdAquisicao;

            _context.Entry(veiculoExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Veiculo atualizado com Sucesso!");
        }

        public async Task<ResultadoOperacao> RemoverAsync(int id)
        {
            var Veiculo = await _context.Veiculos.FindAsync(id);
            if (Veiculo == null)
                return ResultadoOperacao.Falha("Falha na remoção do Veiculo");
            _context.Veiculos.Remove(Veiculo);
            await _context.SaveChangesAsync();
            return ResultadoOperacao.Ok("Veiculo removido com sucesso");
        }
    }
}
