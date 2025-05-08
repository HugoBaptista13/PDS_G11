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
using Microsoft.AspNetCore.Authorization;

namespace FaiscaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoVeiculoController : ControllerBase
    {
        private readonly ITipoVeiculoService _tipoVeiculoService;

        public TipoVeiculoController(ITipoVeiculoService tipoVeiculoService)
        {
            _tipoVeiculoService = tipoVeiculoService;
        }

        // GET: api/TipoVeiculo
        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpGet("mostar-tipos-veiculos")]
        public async Task<ActionResult<IEnumerable<TipoVeiculo>>> GetTipoVeiculos()
        {
            var tipoVeiculo = await _tipoVeiculoService.ObterTodosAsync();
            return Ok(tipoVeiculo);
        }

        // GET: api/TipoVeiculo/5
        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpGet("mostrar-tipo-veiculo-{id}")]
        public async Task<ActionResult<TipoVeiculo>> GetTipoVeiculo(int id)
        {
            var tipoVeiculo = await _tipoVeiculoService.ObterPorIdAsync(id);

            if (tipoVeiculo == null)
            {
                return NotFound();
            }

            return Ok(tipoVeiculo);
        }

        // PUT: api/TipoVeiculo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpPut("atualizar-tipo-pagamento{id}")]
        public async Task<IActionResult> PutTipoVeiculo(int id, [FromBody]TipoVeiculo tipoVeiculo)
        {
            if (id != tipoVeiculo.IdTipoVeiculo)
                return BadRequest("ID no URL e ID no objeto não coincidem.");

            var updated = await _tipoVeiculoService.AtualizarAsync(tipoVeiculo);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/TipoVeiculo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpPost("criar-tipo-pagamento")]
        public async Task<ActionResult<TipoVeiculo>> PostTipoVeiculo([FromBody]TipoVeiculo tipoVeiculo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _tipoVeiculoService.CriarAsync(tipoVeiculo);
            return CreatedAtAction(nameof(GetTipoVeiculo), new { id = tipoVeiculo.IdTipoVeiculo }, tipoVeiculo);
        }

        // DELETE: api/TipoVeiculo/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("apagar-tipo-pagamento{id}")]
        public async Task<IActionResult> DeleteTipoVeiculo(int id)
        {
            var deleted = await _tipoVeiculoService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

    }
}
