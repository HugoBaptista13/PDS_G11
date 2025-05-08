using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using FaiscaSync.Controllers;
using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using FaiscaSync.DTO;

namespace FaiscaSync.Tests.Unitarios.Controllers
{
    [Trait("Tipo", "Unitario")]
    public class VeiculoControllerTests
    {
        private readonly Mock<IVeiculoService> _mockVeiculoService;
        private readonly VeiculoController _controller;

        public VeiculoControllerTests()
        {
            _mockVeiculoService = new Mock<IVeiculoService>();
            _controller = new VeiculoController(_mockVeiculoService.Object);
        }

        [Fact]
        public async Task GetVeiculo_DeveRetornarVeiculoQuandoExistir()
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


            _mockVeiculoService.Setup(s => s.CriarAsync(It.IsAny<Veiculo>())).Returns(Task.CompletedTask);

            // Act
            var resultado = await _controller.PostVeiculo(veiculoMock);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(resultado.Result);
            var veiculoCriado = Assert.IsType<Veiculo>(createdResult.Value);
            Assert.Equal(1, veiculoCriado.IdVeiculo);
        }

        [Fact]
        public async Task PostVeiculo_DeveCriarNovoVeiculo()
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

            _mockVeiculoService.Setup(s => s.CriarAsync(veiculoMock)).Returns(Task.CompletedTask);

            // Act
            var resultado = await _controller.PostVeiculo(veiculoMock);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(resultado.Result);
            var veiculoCriado = Assert.IsType<Veiculo>(createdResult.Value);
            Assert.Equal(1, veiculoCriado.IdVeiculo);
        }
    }
}
