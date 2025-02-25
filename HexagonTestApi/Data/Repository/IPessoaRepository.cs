using HexagonTest.API.Models;

namespace HexagonTest.API.Data.Repository
{
    public interface IPessoaRepository
    {
        Task<IEnumerable<Pessoa>> GetAllAsync();
        Task<Pessoa> GetByIdAsync(int id);
        Task<Pessoa> CreateAsync(Pessoa pessoa);
        Task<Pessoa> UpdateAsync(Pessoa pessoa);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsByCpfAsync(string cpf, int? id = null);
    }
}