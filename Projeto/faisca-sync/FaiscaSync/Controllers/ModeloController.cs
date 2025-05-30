﻿using System;
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
    public class ModeloController : ControllerBase
    {
        private readonly IModeloService _modeloService;

        public ModeloController(IModeloService modeloService)
        {
            _modeloService = modeloService;
        }

        // GET: api/Modelo
        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpGet("mostrar-todos-modelos")]
        public async Task<ActionResult<IEnumerable<Modelo>>> GetModelos()
        {
            var modelo = await _modeloService.ObterTodosAsync();
            return Ok(modelo);
        }

        // GET: api/Modelo/5
        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpGet("mostrar-modelo-{id}")]
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
        [Authorize(Roles = "Administrador")]
        [HttpPut("atualizar-modelo-{id}")]
        public async Task<IActionResult> PutModelo(int id, [FromBody] Modelo modelo)
        {
            if (id != modelo.IdModelo)
                return BadRequest("ID no URL e ID no objeto não coincidem.");

            var updated = await _modeloService.AtualizarAsync(modelo);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/Modelo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador")]
        [HttpPost("criar-modelo")]
        public async Task<ActionResult<Modelo>> PostModelo([FromBody]Modelo modelo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _modeloService.CriarAsync(modelo);
            return CreatedAtAction(nameof(GetModelo), new { id = modelo.IdModelo }, modelo);
        }

        // DELETE: api/Modelo/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("apagar-modelo-{id}")]
        public async Task<IActionResult> DeleteModelo(int id)
        {
            var deleted = await _modeloService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

    }
}
