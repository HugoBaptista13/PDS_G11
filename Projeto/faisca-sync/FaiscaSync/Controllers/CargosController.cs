using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaiscaSync.Models;
using FaiscaSync.Services;
using FaiscaSync.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargosController : ControllerBase
    {
        private readonly ICargoService _cargoService;

        public CargosController(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        // GET: api/Cargos
        [Authorize(Roles = "Administrador")]
        [HttpGet("mostrar-todos-cargos")]
        public async Task<ActionResult<IEnumerable<Cargo>>> GetCargos()
        {
           var cargo =  await _cargoService.ObterTodosAsync();
            return Ok(cargo);
        }

        // GET: api/Cargos/5
        [Authorize(Roles = "Administrador")]
        [HttpGet("mostrar-cargo-{id}")]
        public async Task<ActionResult<Cargo>> GetCargo(int id)
        {
            var cargo = await _cargoService.ObterPorIdAsync(id);

            if (cargo == null)
            {
                return NotFound();
            }

            return Ok(cargo);
        }

        // PUT: api/Cargos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador")]
        [HttpPut("atualizar-cargo-{id}")]
        public async Task<IActionResult> PutCargo(int id, [FromBody] Cargo cargo)
        {
            if (id != cargo.IdCargo)
                return BadRequest("ID no URL e ID no objeto não coincidem.");

            var updated = await _cargoService.AtualizarAsync(cargo);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/Cargos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador")]
        [HttpPost ("criar-cargo")]
        public async Task<IActionResult> PostCargo([FromBody]Cargo cargo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _cargoService.CriarAsync(cargo);
            return CreatedAtAction(nameof(GetCargo), new { id = cargo.IdCargo }, cargo);
        }

        // DELETE: api/Cargos/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("apagar-cargo-{id}")]
        public async Task<IActionResult> DeleteCargo(int id)
        {
            var deleted = await _cargoService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });

        }
    }
}
