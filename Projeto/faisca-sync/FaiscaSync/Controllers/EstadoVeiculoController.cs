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

namespace FaiscaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoVeiculoController : ControllerBase
    {
        private readonly IEstadoVeiculoService _estadoVeiculoService;

        public EstadoVeiculoController(IEstadoVeiculoService estadoVeiculoService)
        {
            _estadoVeiculoService = estadoVeiculoService;
        }

        // GET: api/EstadoVeiculo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoVeiculo>>> GetEstadoVeiculos()
        {
            var estadoVeiculo = await _estadoVeiculoService.ObterTodosAsync();
            return Ok(estadoVeiculo);
        }

        // GET: api/EstadoVeiculo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoVeiculo>> GetEstadoVeiculo(int id)
        {
            var estadoVeiculo = await _estadoVeiculoService.ObterPorIdAsync(id);

            if (estadoVeiculo == null)
            {
                return NotFound();
            }

            return Ok(estadoVeiculo);
        }

        // PUT: api/EstadoVeiculo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadoVeiculo(int id, [FromBody] EstadoVeiculoDTO estadoVeiculoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _estadoVeiculoService.AtualizarAsync(id, estadoVeiculoDto);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/EstadoVeiculo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EstadoVeiculo>> PostEstadoVeiculo([FromBody]EstadoVeiculoDTO estadoVeiculoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var estadoVeiculo = new EstadoVeiculo
            {
                Descricaoestadoveiculo = estadoVeiculoDto.Estado
            };

            await _estadoVeiculoService.CriarAsync(estadoVeiculoDto);
            return CreatedAtAction(nameof(GetEstadoVeiculo), new { id = estadoVeiculo.IdEstadoVeiculo }, estadoVeiculo);
        }

        // DELETE: api/EstadoVeiculo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadoVeiculo(int id)
        {
            var deleted = await _estadoVeiculoService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

        private async Task<ResultadoOperacao> EstadoVeiculoExists(int id)
        {
            var estadoVeiculo = await _estadoVeiculoService.ObterPorIdAsync(id);
            return estadoVeiculo != null
                ? ResultadoOperacao.Ok("Estado Veiculo encontrado.")
        : ResultadoOperacao.Falha("Estado Veiculo não encontrado.");
        }
    }
}
