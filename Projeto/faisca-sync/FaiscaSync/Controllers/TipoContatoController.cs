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
    public class TipoContatoController : ControllerBase
    {
        private readonly ITipoContatoService _tipoContactoService;

        public TipoContatoController(ITipoContatoService tipoContatoService)
        {
            _tipoContactoService = tipoContatoService;
        }

        // GET: api/TipoContato
        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpGet("mostrar-tipos-contacto")]
        public async Task<ActionResult<IEnumerable<TipoContato>>> GetTipoContatos()
        {
            var tipoContato = await _tipoContactoService.ObterTodosAsync();
            return Ok(tipoContato);
        }

        // GET: api/TipoContato/5
        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpGet("mostrar-tipo-contacto-{id}")]
        public async Task<ActionResult<TipoContato>> GetTipoContato(int id)
        {
            var tipoContato = await _tipoContactoService.ObterPorIdAsync(id);

            if (tipoContato == null)
            {
                return NotFound();
            }

            return Ok(tipoContato);
        }

        // PUT: api/TipoContato/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador")]
        [HttpPut("atualizar-tipo-contacto-{id}")]
        public async Task<IActionResult> PutTipoContato(int id,[FromBody] TipoContato tipoContato)
        {
            if (id != tipoContato.IdTipoContato)
                return BadRequest("ID no URL e ID no objeto não coincidem.");

            var updated = await _tipoContactoService.AtualizarAsync(tipoContato);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/TipoContato
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador")]
        [HttpPost("criar-tipo-contacto")]
        public async Task<ActionResult<TipoContato>> PostTipoContato([FromBody]TipoContato tipoContato)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await _tipoContactoService.CriarAsync(tipoContato);
            return CreatedAtAction(nameof(GetTipoContato), new { id = tipoContato.IdTipoContato }, tipoContato);
        }

        // DELETE: api/TipoContato/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("apagar-tipo-contacto-{id}")]
        public async Task<IActionResult> DeleteTipoContato(int id)
        {
            var deleted = await _tipoContactoService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

    }
}
