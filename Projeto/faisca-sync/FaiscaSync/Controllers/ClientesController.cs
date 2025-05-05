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
using FaiscaSync.DTO;

namespace FaiscaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var cliente = await _clienteService.ObterTodosAsync();
            return Ok(cliente);
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _clienteService.ObterPorIdAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id,[FromBody] ClienteDTO clienteDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _clienteService.AtualizarAsync(id, clienteDto);

            if (!updated.Sucesso)
                return NotFound(new { mensagem = updated.Mensagem });

            return Ok(new { mensagem = updated.Mensagem });
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostCliente([FromBody] ClienteDTO clienteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Converter o DTO para a entidade Cliente
            var cliente = new Cliente
            {
                Nome = clienteDto.Nome,
                Datanasc = clienteDto.Datanasc,
                Nif = clienteDto.Nif,
                IdMorada = clienteDto.IdMorada
            };

            await _clienteService.CriarAsync(clienteDto);
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.IdCliente }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var deleted = await _clienteService.RemoverAsync(id);
            if (!deleted.Sucesso)
            {
                return NotFound(new { mensagem = deleted.Mensagem });
            }

            return NotFound(new { mensagem = deleted.Mensagem });
        }

        private async Task<ResultadoOperacao> ClienteExists(int id)
        {
            var cliente = await _clienteService.ObterPorIdAsync(id);
            return cliente != null
                ? ResultadoOperacao.Ok("Cliente encontrado.")
        : ResultadoOperacao.Falha("Cliente não encontrado.");
        }
    }
}
