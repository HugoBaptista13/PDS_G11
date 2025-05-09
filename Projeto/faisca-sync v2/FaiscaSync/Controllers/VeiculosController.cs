using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FaiscaSync.Models;
using FaiscaSync.DTO;
using FaiscaSync.Services;
using Microsoft.AspNetCore.Authorization;

namespace FaiscaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly VeiculoServices _veiculoService;

        public VeiculosController(VeiculoServices veiculoService)
        {
            _veiculoService = veiculoService;
        }

        // GET: api/Veiculos
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Veiculo>>> GetVeiculos()
        {
            var veiculos = await _veiculoService.GetVeiculos();
            return Ok(veiculos);
        }

        // GET: api/Veiculos/5
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Veiculo>> GetVeiculo(int id)
        {
            try
            {
                var veiculo = await _veiculoService.GetVeiculo(id);
                return Ok(veiculo);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // PUT: api/Veiculos/5
        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVeiculo(int id, Veiculo veiculo)
        {
            if (id != veiculo.IdVeiculo)
            {
                return BadRequest();
            }

            var updated = await _veiculoService.UpdateAsync(id, veiculo);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // POST: api/Veiculos
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpPost]
        public async Task<ActionResult<Veiculo>> PostVeiculo(Veiculo veiculo)
        {
            var created = await _veiculoService.CreateAsync(veiculo);
            return CreatedAtAction(nameof(GetVeiculo), new { id = created.IdVeiculo }, created);
        }

        // DELETE: api/Veiculos/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVeiculo(int id)
        {
            var deleted = await _veiculoService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("pesquisa-avancada")]
        public async Task<ActionResult<IEnumerable<Veiculo>>> PesquisaAvancada([FromBody] PesquisaVeiculoDTO filtro)
        {
            var resultado = await _veiculoService.PesquisaAvancadaAsync(filtro);
            return Ok(resultado);
        }

        [AllowAnonymous]
        [HttpGet("adicionados-recentes")]
        public async Task<IActionResult> GetVeiculosEmDestaque()
        {
            var veiculos = await _veiculoService.GetVeiculosDestaque();
            return Ok(veiculos);
        }

        [AllowAnonymous]
        [HttpGet("catalogo")]
        public async Task<ActionResult<IEnumerable<Veiculo>>> GetVeiculosDisponiveis()
        {
            var veiculos = await _veiculoService.GetVeiculosDisponiveis();
            return Ok(veiculos);
        }
    }
}

