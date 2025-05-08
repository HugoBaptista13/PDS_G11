using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using FaiscaSync.Models;
using FaiscaSync.Services;
using Microsoft.Extensions.Configuration;

namespace FaiscaSync.Tests.Unitarios.Services
{
    [Trait("Tipo", "Unitario")]
    public class VeiculoServiceTests
    {
        private readonly Mock<FsContext> _mockContext;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly VeiculoService _service;

        public VeiculoServiceTests()
        {
            _mockContext = new Mock<FsContext>();
            _mockConfiguration = new Mock<IConfiguration>();
            _service = new VeiculoService(_mockContext.Object, _mockConfiguration.Object);
        }

        [Fact]
        public async Task CriarAsync_DeveAdicionarNovoVeiculo()
        {
            // Arrange
            var veiculoMock = new Veiculo
            {
                IdVeiculo = 1,
                Matricula = "ABC-1234",
                Chassi = "XYZ7890123456",
                Anofabrico = 2022,
                Cor = "Preto",
                Quilometros = 15000.50m,
                Preco = 25000.75m,
                IdMotor = 2,
                IdEstadoVeiculo = 1,
                IdTipoVeiculo = 3,
                IdModelo = 5,
                IdAquisicao = 4
            };
            var dbSetMock = new Mock<DbSet<Veiculo>>();
            _mockContext.Setup(c => c.Veiculos).Returns(dbSetMock.Object);
            _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1); // Simula o sucesso

            // Act
            await _service.CriarAsync(veiculoMock);

            // Assert
            dbSetMock.Verify(d => d.Add(It.IsAny<Veiculo>()), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task RemoverAsync_DeveRemoverVeiculoSeExistir()
        {
            // Arrange
            var veiculoMock = new Veiculo
            {
                IdVeiculo = 1,
                Matricula = "ABC-1234",
                Chassi = "XYZ7890123456",
                Anofabrico = 2022,
                Cor = "Preto",
                Quilometros = 15000.50m,
                Preco = 25000.75m,
                IdMotor = 2,
                IdEstadoVeiculo = 1,
                IdTipoVeiculo = 3,
                IdModelo = 5,
                IdAquisicao = 4
            };

            var dbSetMock = new Mock<DbSet<Veiculo>>();

            _mockContext.Setup(c => c.Veiculos).Returns(dbSetMock.Object);
            _mockContext.Setup(c => c.Veiculos.FindAsync(1)).ReturnsAsync(veiculoMock);
            _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1); // Corrigido!

            // Act
            var resultado = await _service.RemoverAsync(1);

            // Assert
            dbSetMock.Verify(d => d.Remove(It.IsAny<Veiculo>()), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.True(resultado.Sucesso);
            Assert.Equal("Veiculo removido com sucesso", resultado.Mensagem);
        }
    }
}
