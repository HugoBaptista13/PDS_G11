using System.Net.Http.Json;
using FaiscaSync.DTO;
using FaiscaSync.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FluentAssertions;

namespace FaiscaSync.Tests
{
    public class VeiculosControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public VeiculosControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PostVeiculo_ShouldCreateVeiculo_WhenDataIsValid()
        {
            var veiculoDto = new VeiculoDTO
            {
                Matricula = "TEST-1234",
                Chassi = "CHASSITESTE123",
                AnoFabrico = 2020,
                Cor = "Vermelho",
                Quilometragem = 10000,
                PrecoVenda = 15000,
                Fornecedor = "Fornecedor Teste",
                ValorPago = 10000,
                DataAquisicao = DateTime.Now,
                OrigemVeiculo = "Importado",
                TipoMotor = "1.6",
                Potencia = "120cv",
                Combustivel = "Gasolina",
                DescricaoMarca = "Renault",
                NomeModelo = "Clio"
            };

            var response = await _client.PostAsJsonAsync("/api/Veiculos", veiculoDto);

            response.EnsureSuccessStatusCode();
            var veiculoCriado = await response.Content.ReadFromJsonAsync<Veiculo>();
            veiculoCriado.Should().NotBeNull();
            veiculoCriado!.Matricula.Should().Be("TEST-1234");
        }
    }
}
