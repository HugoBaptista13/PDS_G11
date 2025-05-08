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
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaService _marcaService;

        public MarcaController(IMarcaService marcaService)
        {
            _marcaService = marcaService;
        }

        // GET: api/Marca
        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpGet("mostrar-todas-marcas")]
        public async Task<ActionResult<IEnumerable<Marca>>> GetMarcas()
        {
            var marca = await _marcaService.ObterTodosAsync();
            return Ok(marca);
        }

        // GET: api/Marca/5
        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpGet("mostrar-marca-{id}")]
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
        [Authorize(Roles = "Administrador")]
        [HttpPut("atualizar-marca-{id}")]
        public async Task<IActionResult> PutMarca(int id, [FromBody]Marca marca)
        {
            if (id !=  marca.IdMarca)
                return BadRequest("ID no URL e ID no objeto não coincidem.");

            var updated = await _marcaService.AtualizarAsync(marca);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/Marca
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador")]
        [HttpPost("criar-marca")]
        public async Task<ActionResult<Marca>> PostMarca([FromBody]Marca marca)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _marcaService.CriarAsync(marca);
            return CreatedAtAction(nameof(GetMarca), new { id = marca.IdMarca }, marca);
        }

        // DELETE: api/Marca/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("apagar-marca-{id}")]
        public async Task<IActionResult> DeleteMarca(int id)
        {
            var deleted = await _marcaService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

    }
}
