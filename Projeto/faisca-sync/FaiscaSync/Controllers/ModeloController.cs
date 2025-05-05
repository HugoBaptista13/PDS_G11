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
    public class ModeloController : ControllerBase
    {
        private readonly IModeloService _modeloService;

        public ModeloController(IModeloService modeloService)
        {
            _modeloService = modeloService;
        }

        // GET: api/Modelo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Modelo>>> GetModelos()
        {
            var modelo = await _modeloService.ObterTodosAsync();
            return Ok(modelo);
        }

        // GET: api/Modelo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Modelo>> GetModelo(int id)
        {
            var modelo = await _modeloService.ObterPorIdAsync(id);

            if (modelo == null)
            {
                return NotFound();
            }

            return Ok(modelo);
        }

        // PUT: api/Modelo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModelo(int id, [FromBody] ModeloDTO modeloDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _modeloService.AtualizarAsync(id, modeloDto);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/Modelo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Modelo>> PostModelo([FromBody]ModeloDTO modeloDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var modelo = new Modelo
            {
                Nomemodelo = modeloDto.NomeModelo,
                IdMarca = modeloDto.IdMarca
            };

            await _modeloService.CriarAsync(modeloDto);
            return CreatedAtAction(nameof(GetModelo), new { id = modelo.IdModelo }, modelo);
        }

        // DELETE: api/Modelo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModelo(int id)
        {
            var deleted = await _modeloService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

        private async Task<ResultadoOperacao> ModeloExists(int id)
        {
            var modelo = await _modeloService.ObterPorIdAsync(id);
            return modelo != null
                ? ResultadoOperacao.Ok("Modelo encontrado.")
        : ResultadoOperacao.Falha("Modelo não encontrado.");
        }
    }
}
