using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FaiscaSync.DTO;
using FaiscaSync.Models;
using FaiscaSync.Services;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaturasController : ControllerBase
    {
        private readonly FaturaServices _faturaService;

        public FaturasController(FaturaServices faturaService)
        {
            _faturaService = faturaService;
        }

        // GET: api/Faturas
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fatura>>> GetFaturas()
        {
            var faturas = await _faturaService.GetAllAsync();
            return Ok(faturas);
        }

        // GET: api/Faturas/5
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Fatura>> GetFatura(int id)
        {
            var fatura = await _faturaService.GetByIdAsync(id);

            if (fatura == null)
            {
                return NotFound();
            }

            return Ok(fatura);
        }

        // PUT: api/Faturas/5
        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFatura(int id, [FromBody] FaturaDTO faturaDto)
        {
            var existing = await _faturaService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            // Atualiza os campos da fatura com os dados do DTO
            existing.Dataemissao = faturaDto.DataEmissao;
            existing.Valorfatura = faturaDto.ValorFatura;
            existing.TipoPagamento = faturaDto.TipoPagamento;
            existing.IdCliente = faturaDto.IdCliente;

            if (faturaDto.IsVenda)
            {
                existing.IdVendas = faturaDto.IdVendas;
                existing.IdManutencao = null;
            }
            else
            {
                existing.IdManutencao = faturaDto.IdManutencao;
                existing.IdVendas = null; 
            }

            await _faturaService.UpdateAsync(id, existing);  // Só esta linha grava.


            return NoContent();
        }

        // POST: api/Faturas
        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpPost]
        public async Task<ActionResult<Fatura>> PostFatura([FromBody] FaturaDTO faturaDto)
        {
            var created = await _faturaService.CreateAsync(faturaDto);
            return CreatedAtAction(nameof(GetFatura), new { id = created.IdFatura }, created);
        }

        // DELETE: api/Faturas/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFatura(int id)
        {
            var deleted = await _faturaService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
