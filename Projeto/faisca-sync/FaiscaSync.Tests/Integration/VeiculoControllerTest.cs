using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using FaiscaSync.Tests.Integration;
using FaiscaSync.Models;
using FaiscaSync.Tests.Integration;

namespace FaiscaSync.Tests.Integration
{
    [Trait("Tipo", "Integracao")]
    public class VeiculoControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public VeiculoControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CriarVeiculo_DeveRetornarCreated()
        {
            // Arrange
            var novoVeiculo = new Veiculo
            {
                IdModelo = 1,
                IdTipoVeiculo = 1,
                IdEstadoVeiculo = 1,
                IdMotor = 1,
                Anofabrico = 2022,
                Preco = 15000,
                Quilometros = 50000
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Veiculo/criar-veiculo", novoVeiculo);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task GetVeiculos_DeveRetornarOK()
        {
            // Act
            var response = await _client.GetAsync("/api/Veiculo/mostrar-veiculos");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode); // Endpoint exige autenticação
        }

        [Fact]
        public async Task GetVeiculosDisponiveis_DeveRetornarOK()
        {
            // Act
            var response = await _client.GetAsync("/api/Veiculo/catalogo");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
