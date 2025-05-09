using FaiscaSync.Models;
using FaiscaSync.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace FaiscaSync.Tests
{
    [TestClass]
    public class CargoServiceTests
    {
        private DbContextOptions<FsContext> _options;

        public CargoServiceTests()
        {
            // Configuração do banco de dados em memória para os testes
            _options = new DbContextOptionsBuilder<FsContext>()
                .UseInMemoryDatabase(databaseName: "fs")
                .Options;
        }

        [TestMethod]
        public async Task CreateAsync_DeveCriarCargoComSucesso()
        {
            // Arrange
            var context = new FsContext(_options);  // Corrigido para garantir que o contexto seja instanciado corretamente
            var cargoService = new CargoServices(context);

            var novoCargo = new Cargo
            {
                IdCargo = 7,
                Nomecargo = "Cargo de Teste"
            };

            // Act
            var resultado = await cargoService.CreateAsync(novoCargo);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual("Cargo de Teste", resultado.Nomecargo);
            Assert.AreEqual(1, context.Cargos.Count()); // Verifica se o cargo foi realmente adicionado
        }

        [TestMethod]
        public async Task UpdateAsync_DeveAtualizarCargoComSucesso()
        {
            // Arrange
            var context = new FsContext(_options);
            context.Cargos.Add(new Cargo { IdCargo = 7, Nomecargo = "Cargo Original" });
            await context.SaveChangesAsync();

            var cargoService = new CargoServices(context);

            var cargoAtualizado = new Cargo
            {
                IdCargo = 7,
                Nomecargo = "Cargo Atualizado"
            };

            // Act
            var resultado = await cargoService.UpdateAsync(7, cargoAtualizado);

            // Assert
            Assert.IsTrue(resultado);
            var cargo = await context.Cargos.FindAsync(7);
            Assert.AreEqual("Cargo Atualizado", cargo.Nomecargo);
        }

        [TestMethod]
        public async Task DeleteAsync_DeveApagarCargoComSucesso()
        {
            // Arrange
            var context = new FsContext(_options);
            context.Cargos.Add(new Cargo { IdCargo = 7, Nomecargo = "Cargo para apagar" });
            await context.SaveChangesAsync();

            var cargoService = new CargoServices(context);

            // Act
            var resultado = await cargoService.DeleteAsync(7);

            // Assert
            Assert.IsTrue(resultado);
            var cargo = await context.Cargos.FindAsync(7);
            Assert.IsNull(cargo);
        }
    }
}
