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
    public class TipoVeiculosController : ControllerBase
    {
        private readonly FsContext _context;

        public TipoVeiculosController(FsContext context)
        {
            _context = context;
        }

        // GET: api/TipoVeiculos
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoVeiculo>>> GetTipoVeiculos()
        {
            return await _context.TipoVeiculos.ToListAsync();
        }

        // GET: api/TipoVeiculos/5
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoVeiculo>> GetTipoVeiculo(int id)
        {
            var tipoVeiculo = await _context.TipoVeiculos.FindAsync(id);

            if (tipoVeiculo == null)
            {
                return NotFound();
            }

            return tipoVeiculo;
        }

        // PUT: api/TipoVeiculos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoVeiculo(int id, TipoVeiculo tipoVeiculo)
        {
            if (id != tipoVeiculo.IdTipoVeiculo)
            {
                return BadRequest();
            }

            _context.Entry(tipoVeiculo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoVeiculoExists(id))
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

        // POST: api/TipoVeiculos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<ActionResult<TipoVeiculo>> PostTipoVeiculo(TipoVeiculo tipoVeiculo)
        {
            _context.TipoVeiculos.Add(tipoVeiculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoVeiculo", new { id = tipoVeiculo.IdTipoVeiculo }, tipoVeiculo);
        }

        // DELETE: api/TipoVeiculos/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoVeiculo(int id)
        {
            var tipoVeiculo = await _context.TipoVeiculos.FindAsync(id);
            if (tipoVeiculo == null)
            {
                return NotFound();
            }

            _context.TipoVeiculos.Remove(tipoVeiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoVeiculoExists(int id)
        {
            return _context.TipoVeiculos.Any(e => e.IdTipoVeiculo == id);
        }
    }
}
