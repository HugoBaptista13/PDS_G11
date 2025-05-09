using System.Net.Http.Json;
using FaiscaSync.DTO;
using FaiscaSync.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FluentAssertions;

namespace FaiscaSync.Tests
{
    public class ClientesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ClientesControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PostCliente_ShouldCreateCliente_WhenDataIsValid()
        {
            // Arrange
            var novoCliente = new ClienteDTO
            {
                Nome = "Cliente Teste",
                Datanasc = new DateTime(1990, 1, 1),
                Nif = "123456789",
                Contato = "912345678",
                IdMorada = 1 // ⚠️ Certifica-te que existe uma morada com ID=1 para o teste ou adapta isto.
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Clientes", novoCliente);

            // Assert
            response.EnsureSuccessStatusCode(); // 200-299
            var clienteCriado = await response.Content.ReadFromJsonAsync<Cliente>();
            clienteCriado.Should().NotBeNull();
            clienteCriado!.Nome.Should().Be("Cliente Teste");
        }
    }
}
