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
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaService _marcaService;

        public MarcaController(IMarcaService marcaService)
        {
            _marcaService = marcaService;
        }

        // GET: api/Marca
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Marca>>> GetMarcas()
        {
            var marca = await _marcaService.ObterTodosAsync();
            return Ok(marca);
        }

        // GET: api/Marca/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Marca>> GetMarca(int id)
        {
            var marca = await _marcaService.ObterPorIdAsync(id);

            if (marca == null)
            {
                return NotFound();
            }

            return Ok(marca);
        }

        // PUT: api/Marca/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMarca(int id, [FromBody]MarcaDTO marcaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _marcaService.AtualizarAsync(id, marcaDto);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/Marca
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Marca>> PostMarca([FromBody]MarcaDTO marcaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var marca = new Marca
            {
                Descricaomarca = marcaDto.Marca
            };

            await _marcaService.CriarAsync(marcaDto);
            return CreatedAtAction(nameof(GetMarca), new { id = marca.IdMarca }, marca);
        }

        // DELETE: api/Marca/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarca(int id)
        {
            var deleted = await _marcaService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

        private async Task<ResultadoOperacao> MarcaExists(int id)
        {
            var marca = await _marcaService.ObterPorIdAsync(id);
            return marca != null
                ? ResultadoOperacao.Ok("Marca encontrada.")
        : ResultadoOperacao.Falha("Marca não encontrada.");
        }
    }
}
