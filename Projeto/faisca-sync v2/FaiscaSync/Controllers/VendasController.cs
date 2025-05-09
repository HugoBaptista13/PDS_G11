using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FaiscaSync.DTO;
using FaiscaSync.Models;
using FaiscaSync.Services;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly VendaServices _service;
        private readonly FsContext _context;


        public VendasController(VendaServices service, FsContext context)
        {
            _service = service;
            _context = context;
        }

        // GET: api/Vendas
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venda>>> GetVendas()
        {
            var vendas = await _service.GetAllAsync();
            return Ok(vendas);
        }
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet("nao-faturadas")]
        public async Task<ActionResult<IEnumerable<Venda>>> GetVendasNaoFaturadas()
        {
            var vendasNaoFaturadas = await _context.Vendas
                .Where(v => !_context.Faturas.Any(f => f.IdVendas == v.IdVendas))
                .ToListAsync();

            return Ok(vendasNaoFaturadas);
        }

        // GET: api/Vendas/5
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Venda>> GetVenda(int id)
        {
            var venda = await _service.GetByIdAsync(id);

            if (venda == null)
            {
                return NotFound();
            }

            return Ok(venda);
        }

        // PUT: api/Vendas/5
        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenda(int id, [FromBody] VendasDTO vendaDto)
        {
            var venda = new Venda
            {
                IdVendas = id,
                Datavenda = vendaDto.DataVenda,
                Valorvenda = vendaDto.ValorVenda,
                IdVeiculo = vendaDto.IdVeiculo,
                IdCliente = vendaDto.IdCliente,
                IdFuncionario = vendaDto.IdFuncionario
            };

            var updated = await _service.UpdateAsync(id, venda);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // POST: api/Vendas
        [Authorize(Roles = "Administrador, Financeiro")]
        [HttpPost]
        public async Task<ActionResult<Venda>> PostVenda([FromBody] VendasDTO vendaDto)
        {
            var venda = new Venda
            {
                Datavenda = vendaDto.DataVenda,
                Valorvenda = vendaDto.ValorVenda,
                IdVeiculo = vendaDto.IdVeiculo,
                IdCliente = vendaDto.IdCliente,
                IdFuncionario = vendaDto.IdFuncionario
            };

            var created = await _service.CreateAsync(vendaDto);

            return CreatedAtAction(nameof(GetVenda), new { id = created.IdVendas }, created);
        }

        // DELETE: api/Vendas/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenda(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
