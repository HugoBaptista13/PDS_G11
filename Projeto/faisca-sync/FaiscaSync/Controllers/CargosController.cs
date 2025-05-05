using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaiscaSync.DTO;
using FaiscaSync.Models;
using FaiscaSync.Services;
using FaiscaSync.Services.Interface;
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cargo>>> GetCargos()
        {
           var cargo =  await _cargoService.ObterTodosAsync();
            return Ok(cargo);
        }

        // GET: api/Cargos/5
        [HttpGet("{id}")]
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCargo(int id, [FromBody] CargoDTO cargoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _cargoService.AtualizarAsync(id, cargoDto);

            if (!resultado.Sucesso)
                return NotFound(new { mensagem = resultado.Mensagem });

            return Ok(new { mensagem = resultado.Mensagem });
        }

        // POST: api/Cargos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostCargo([FromBody]CargoDTO cargoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cargo = new Cargo
            {
                Nomecargo = cargoDto.Nomecargo
            };
            await _cargoService.CriarAsync(cargoDto);
            return CreatedAtAction(nameof(GetCargo), new { id = cargo.IdCargo }, cargo);
        }

        // DELETE: api/Cargos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCargo(int id)
        {
            var deleted = await _cargoService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });

        }

        private async Task<ResultadoOperacao> CargoExists(int id)
        {
            var cargo = await _cargoService.ObterPorIdAsync(id);
            return cargo != null
                ? ResultadoOperacao.Ok("Cargo encontrado.")
        : ResultadoOperacao.Falha("Cargo não encontrado.");
        }
    }
}
