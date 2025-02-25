using HexagonTest.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HexagonTest.API.Data.Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly ApplicationDbContext _context;

        public PessoaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pessoa>> GetAllAsync()
        {
            return await _context.Pessoas.ToListAsync();
        }

        public async Task<Pessoa> GetByIdAsync(int id)
        {
            return await _context.Pessoas.FindAsync(id);
        }

        public async Task<Pessoa> CreateAsync(Pessoa pessoa)
        {
            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();
            return pessoa;
        }

        public async Task<Pessoa> UpdateAsync(Pessoa pessoa)
        {
            _context.Entry(pessoa).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return pessoa;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null)
                return false;

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsByCpfAsync(string cpf, int? id = null)
        {
            if (id.HasValue)
            {
                return await _context.Pessoas.AnyAsync(p => p.CPF == cpf && p.Id != id);
            }
            return await _context.Pessoas.AnyAsync(p => p.CPF == cpf);
        }
    }
}