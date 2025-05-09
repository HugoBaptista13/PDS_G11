using FaiscaSync.Models;
using Microsoft.EntityFrameworkCore;

namespace FaiscaSync.Services
{
    public class ManutencaoServices
    {
        private readonly FsContext _context;

        public ManutencaoServices(FsContext context)
        {
            _context = context;
        }

        // Lista todas as Manutenções
        public async Task<List<Manutencao>> GetAllAsync()
        {
            return await _context.Manutencaos.ToListAsync();
        }

        // Obtém uma Manutenção por ID
        public async Task<Manutencao?> GetByIdAsync(int id)
        {
            return await _context.Manutencaos.FindAsync(id);
        }

        // Cria uma nova Manutenção
        public async Task<Manutencao> CreateAsync(Manutencao manutencao)
        {
            _context.Manutencaos.Add(manutencao);
            await _context.SaveChangesAsync();
            return manutencao;
        }

        // Atualiza uma Manutenção existente
        public async Task<bool> UpdateAsync(int id, Manutencao manutencao)
        {
            var existing = await _context.Manutencaos.FindAsync(id);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(manutencao);
            await _context.SaveChangesAsync();
            return true;
        }

        // Remove uma Manutenção
        public async Task<bool> DeleteAsync(int id)
        {
            var manutencao = await _context.Manutencaos.FindAsync(id);
            if (manutencao == null)
                return false;

            _context.Manutencaos.Remove(manutencao);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
