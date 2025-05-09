using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FaiscaSync.Models;
using Microsoft.AspNetCore.Authorization;

namespace FaiscaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoVeiculosController : ControllerBase
    {
        private readonly FsContext _context;

        public EstadoVeiculosController(FsContext context)
        {
            _context = context;
        }

        // GET: api/EstadoVeiculos
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoVeiculo>>> GetEstadoVeiculos()
        {
            return await _context.EstadoVeiculos.ToListAsync();
        }

        // GET: api/EstadoVeiculos/5
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoVeiculo>> GetEstadoVeiculo(int id)
        {
            var estadoVeiculo = await _context.EstadoVeiculos.FindAsync(id);

            if (estadoVeiculo == null)
            {
                return NotFound();
            }

            return estadoVeiculo;
        }

        // PUT: api/EstadoVeiculos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadoVeiculo(int id, EstadoVeiculo estadoVeiculo)
        {
            if (id != estadoVeiculo.IdEstadoVeiculo)
            {
                return BadRequest();
            }

            _context.Entry(estadoVeiculo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoVeiculoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EstadoVeiculos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<ActionResult<EstadoVeiculo>> PostEstadoVeiculo(EstadoVeiculo estadoVeiculo)
        {
            _context.EstadoVeiculos.Add(estadoVeiculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstadoVeiculo", new { id = estadoVeiculo.IdEstadoVeiculo }, estadoVeiculo);
        }

        // DELETE: api/EstadoVeiculos/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadoVeiculo(int id)
        {
            var estadoVeiculo = await _context.EstadoVeiculos.FindAsync(id);
            if (estadoVeiculo == null)
            {
                return NotFound();
            }

            _context.EstadoVeiculos.Remove(estadoVeiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstadoVeiculoExists(int id)
        {
            return _context.EstadoVeiculos.Any(e => e.IdEstadoVeiculo == id);
        }
    }
}
