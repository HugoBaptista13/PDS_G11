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
    public class EstadoVeiculoController : ControllerBase
    {
        private readonly IEstadoVeiucloService _estadoVeiucloService;

        public EstadoVeiculoController(IEstadoVeiucloService estadoVeiucloService)
        {
            _estadoVeiucloService = estadoVeiucloService;
        }

        // GET: api/EstadoVeiculo
        [Authorize(Roles = "Administrador")]
        [HttpGet("mostrar-estados-veiculo")]
        public async Task<ActionResult<IEnumerable<EstadoVeiculo>>> GetEstadoVeiculos()
        {
            var estadoVeiculo = await _estadoVeiucloService.ObterTodosAsync();
            return Ok(estadoVeiculo);
        }

        // GET: api/EstadoVeiculo/5
        [Authorize(Roles = "Administrador")]
        [HttpGet("mostrar-estado-veiculo-{id}")]
        public async Task<ActionResult<EstadoVeiculo>> GetEstadoVeiculo(int id)
        {
            var estadoVeiculo = await _estadoVeiucloService.ObterPorIdAsync(id);

            if (estadoVeiculo == null)
            {
                return NotFound();
            }

            return Ok(estadoVeiculo);
        }

        // PUT: api/EstadoVeiculo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador, Financeiro, Funcionário")]
        [HttpPut("atualizar-estado-veiculo-{id}")]
        public async Task<IActionResult> PutEstadoVeiculo(int id, [FromBody] EstadoVeiculo estadoVeiculo)
        {
            if (id != estadoVeiculo.IdEstadoVeiculo)
                return BadRequest("ID no URL e ID no objeto não coincidem.");

            var updated = await _estadoVeiucloService.AtualizarAsync(estadoVeiculo);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/EstadoVeiculo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador")]
        [HttpPost("criar-estado-veiculo")]
        public async Task<ActionResult<EstadoVeiculo>> PostEstadoVeiculo([FromBody]EstadoVeiculo estadoVeiculo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _estadoVeiucloService.CriarAsync(estadoVeiculo);
            return CreatedAtAction(nameof(GetEstadoVeiculo), new { id = estadoVeiculo.IdEstadoVeiculo }, estadoVeiculo);
        }

        // DELETE: api/EstadoVeiculo/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("apagar-estado-veiuclo{id}")]
        public async Task<IActionResult> DeleteEstadoVeiculo(int id)
        {
            var deleted = await _estadoVeiucloService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

    }
}
