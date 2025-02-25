using HexagonTest.API.Data.Repository;
using HexagonTest.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace HexagonTest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaRepository _repository;
        private readonly ILogger<PessoaController> _logger;

        public PessoaController(IPessoaRepository repository, ILogger<PessoaController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoa>>> GetAll()
        {
            try
            {
                var pessoas = await _repository.GetAllAsync();
                return Ok(pessoas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todas as pessoas");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> GetById(int id)
        {
            try
            {
                var pessoa = await _repository.GetByIdAsync(id);
                if (pessoa == null)
                    return NotFound($"Pessoa com ID {id} não encontrada");

                return Ok(pessoa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar pessoa com ID {id}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Pessoa>> Create([FromBody] Pessoa pessoa)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Verifica se o CPF já existe
                if (await _repository.ExistsByCpfAsync(pessoa.CPF))
                    return BadRequest("CPF já cadastrado");

                var novaPessoa = await _repository.CreateAsync(pessoa);
                return CreatedAtAction(nameof(GetById), new { id = novaPessoa.Id }, novaPessoa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar pessoa");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Pessoa>> Update(int id, [FromBody] Pessoa pessoa)
        {
            try
            {
                if (id != pessoa.Id)
                    return BadRequest("ID da pessoa não corresponde ao ID da rota");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Verifica se o CPF já existe para outro registro
                if (await _repository.ExistsByCpfAsync(pessoa.CPF, id))
                    return BadRequest("CPF já cadastrado para outra pessoa");

                var existingPessoa = await _repository.GetByIdAsync(id);
                if (existingPessoa == null)
                    return NotFound($"Pessoa com ID {id} não encontrada");

                var updatedPessoa = await _repository.UpdateAsync(pessoa);
                return Ok(updatedPessoa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar pessoa com ID {id}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var pessoa = await _repository.GetByIdAsync(id);
                if (pessoa == null)
                    return NotFound($"Pessoa com ID {id} não encontrada");

                var result = await _repository.DeleteAsync(id);
                if (result)
                    return NoContent();
                else
                    return BadRequest("Não foi possível excluir a pessoa");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao excluir pessoa com ID {id}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
}