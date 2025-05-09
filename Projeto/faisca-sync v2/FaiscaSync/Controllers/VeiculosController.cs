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
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
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
        [HttpPost("criar")]
        public async Task<ActionResult<Veiculo>> CriarVeiculoCompleto([FromBody] VeiculoDTO  dto)
        {
            var funcionarioIdClaim = User.Claims.FirstOrDefault(c => c.Type == "IdFuncionario");
            if (funcionarioIdClaim == null)
            {
                return Unauthorized("IdFuncionario não encontrado no token.");
            }

            int idFuncionario = int.Parse(funcionarioIdClaim.Value);

            var created = await _veiculoService.CriarVeiculoCompletoAsync(dto, idFuncionario);
            return CreatedAtAction(nameof(GetVeiculo), new { id = created.IdVeiculo }, created);
        }


        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}/aprovar")]
        public async Task<IActionResult> AprovarVeiculo(int id)
        {
            var result = await _veiculoService.AprovarVeiculoAsync(id);
            if (!result)
                return NotFound(new { message = "Veículo não encontrado ou erro ao aprovar." });

            return Ok(new { message = "Veículo aprovado e agora está disponível." });
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
        public async Task<ActionResult<IEnumerable<VeiculoPesquisaResultadoDTO>>> PesquisaAvancada([FromBody] PesquisaVeiculoDTO filtro)
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

        [Authorize(Roles = "Administrador")]
        [HttpGet("por-aprovar")]
        public async Task<ActionResult<IEnumerable<Veiculo>>> GetVeiculosPorAprovar()
        {
            var veiculos = await _veiculoService.GetVeiculosPorAprovar();
            return Ok(veiculos);
        }
    }
}

