using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FaiscaSync.DTO;
using FaiscaSync.Models;
using FaiscaSync.Services;

namespace FaiscaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClientesController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: api/Clientes
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var clientes = await _clienteService.GetClientesAsync();
            return Ok(clientes);
        }

        // GET: api/Clientes/5
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _clienteService.GetClienteByIdAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // PUT: api/Clientes/5
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, [FromBody] ClienteDTO clienteDTO)
        {
            var cliente = new Cliente
            {
                IdCliente = id,
                Nome = clienteDTO.Nome,
                Datanasc = clienteDTO.Datanasc,
                Nif = clienteDTO.Nif,
                Contato = clienteDTO.Contato,
                IdMorada = clienteDTO.IdMorada
            };

            var updated = await _clienteService.UpdateClienteAsync(id, cliente);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        // POST: api/Clientes
        [Authorize(Roles = "Administrador, Financeiro, Funcionario")]
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente([FromBody] CriarClienteDTO dto)
        {

            var created = await _clienteService.CriarClienteCompletoAsync(dto);

            return CreatedAtAction(nameof(GetCliente), new { id = created.IdCliente }, created);
        }

        // DELETE: api/Clientes/5
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var deleted = await _clienteService.DeleteClienteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
