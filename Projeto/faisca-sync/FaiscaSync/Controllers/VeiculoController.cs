using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FaiscaSync.Models;
using FaiscaSync.Services.Interface;
using FaiscaSync.Services;
using FaiscaSync.DTO;
using Microsoft.AspNetCore.Authorization;


namespace FaiscaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoService _veiculoService;

        public VeiculoController(IVeiculoService veiculoService)
        {
            _veiculoService = veiculoService;
        }

        // GET: api/Veiculo
        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpGet("mostrar-veiculos")]
        public async Task<ActionResult<IEnumerable<Veiculo>>> GetVeiculos()
        {
            var veiculo = await _veiculoService.ObterTodosAsync();
            return Ok(veiculo);
        }

        [AllowAnonymous]
        [HttpGet("pesquisa-avancada")]
        public async Task<ActionResult<IEnumerable<Veiculo>>> PesquisaAvancada([FromBody] PesquisaVeiculoDTO filtro)
        {
            var resultado = await _veiculoService.PesquisaAvancadaAsync(filtro);
            return Ok(resultado);
        }

        [AllowAnonymous]
        [HttpGet("adicionado-recente")]
        public async Task<IActionResult> GetVeiculosEmDestaque()
        {
            var veiculos = await _veiculoService.ObterVeiculosEmDestaqueAsync();
            return Ok(veiculos);
        }
        [AllowAnonymous]
        [HttpGet("catalogo")]
        public async Task<ActionResult<IEnumerable<Veiculo>>> GetVeiculosDisponiveis()
        {
            var veiculos = await _veiculoService.ObterVeiculosDisponiveisAsync();
            return Ok(veiculos);
        }


        // GET: api/Veiculo/5
        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpGet("mostrar-veiculo-{id}")]
        public async Task<ActionResult<Veiculo>> GetVeiculo(int id)
        {
            var veiculo = await _veiculoService.ObterPorIdAsync(id);

            if (veiculo == null)
            {
                return NotFound();
            }

            return Ok(veiculo);
        }

        // PUT: api/Veiculo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpPut("atualizar-veiculo-{id}")]
        public async Task<IActionResult> PutVeiculo(int id, [FromBody]Veiculo veiculo)
        {
            if (id != veiculo.IdVeiculo)
                return BadRequest("ID no URL e ID no objeto não coincidem.");

            var updated = await _veiculoService.AtualizarAsync(veiculo);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/Veiculo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpPost("criar-veiculo")]
        public async Task<ActionResult<Veiculo>> PostVeiculo([FromBody]Veiculo veiculo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _veiculoService.CriarAsync(veiculo);
            return CreatedAtAction(nameof(GetVeiculo), new { id = veiculo.IdVeiculo }, veiculo);
        }

        // DELETE: api/Veiculo/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("apagar-veiculo-{id}")]
        public async Task<IActionResult> DeleteVeiculo(int id)
        {
            var deleted = await _veiculoService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

    }
}
