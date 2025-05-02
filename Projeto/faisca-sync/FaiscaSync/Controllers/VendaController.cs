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

namespace FaiscaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        private readonly IVendaService _vendaService;

        public VendaController(IVendaService vendaService)
        {
            _vendaService = vendaService;
        }

        // GET: api/Aquisicaos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venda>>> GetVendas()
        {
            var venda = await _vendaService.ObterTodosAsync();
            return Ok(venda);
        }

        // GET: api/Aquisicaos/5 -- obter login por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Venda>> Getvenda(int id)
        {
            var venda = await _vendaService.ObterPorIdAsync(id);

            if (venda == null)
            {
                return NotFound();
            }

            return Ok(venda);
        }

        // PUT: api/Aquisicaos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVenda(int id, [FromBody] Venda venda)
        {
            if (id != venda.IdVendas)
                return BadRequest("ID no URL e ID no objeto não coincidem.");

            var updated = await _vendaService.AtualizarAsync(venda);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }
        // POST: 
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]

        public async Task<IActionResult> PostVenda([FromBody] Venda venda)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _vendaService.CriarAsync(venda);
            return CreatedAtAction(nameof(Getvenda), new { id = venda.IdVendas }, venda);
        }


        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenda(int id)
        {
            var deleted = await _vendaService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }


        private async Task<ResultadoOperacao> VendaExists(int id)
        {
            var venda = await _vendaService.ObterPorIdAsync(id);
            return venda != null
                ? ResultadoOperacao.Ok("Venda encontrada.")
        : ResultadoOperacao.Falha("Venda não encontrada.");
        }
    }
}
