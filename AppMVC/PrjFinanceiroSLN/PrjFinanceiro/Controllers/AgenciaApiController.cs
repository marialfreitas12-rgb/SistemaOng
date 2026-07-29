using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.DTOs;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgenciaApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AgenciaApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("lista")]
        public IActionResult ListarTodos()
        {
            var agencias = _context.Agencia.ToList();
            return Ok(agencias); // Status HTTP 200 OK com o JSON da lista
        }

        [HttpGet("ativas")]
        public IActionResult ListarTodos2()
        {
            var agencias = _context.Agencia.ToList();
            return Ok(agencias); // Status HTTP 200 OK com o JSON da lista
        }


        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var agencia = _context.Agencia.FirstOrDefault(a => a.Codigo == id);

            if (agencia == null)
            {
                return NotFound(new { message = $"Agência com código {id} não encontrada." }); // HTTP 404
            }

            return Ok(agencia); // HTTP 200 OK
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] AgenciaCreateDto dto)
        {
            // O [ApiController] já faz essa validação automaticamente, mas demonstrar em código reforça o aprendizado
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // HTTP 400 Bad Request
            }

            var novaAgencia = new Agencia
            {
                Nome = dto.Nome,
                Cidade = dto.Cidade,
                EstadoUF = dto.EstadoUF
            };

            _context.Agencia.Add(novaAgencia);
            _context.SaveChanges();

            // Padrão REST excelente: Retorna Status 201 Created, popula o cabeçalho 'Location' com a URL de consulta 
            // e entrega o objeto recém-criado com a ID gerada pelo banco de dados.
            return CreatedAtAction(nameof(BuscarPorId), new { id = novaAgencia.Codigo }, novaAgencia);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] AgenciaCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var agenciaNoBanco = _context.Agencia.FirstOrDefault(a => a.Codigo == id);

            if (agenciaNoBanco == null)
            {
                return NotFound(new { message = $"Agência de código {id} inexistente para atualização." });
            }

            // Mapeando dados do DTO para a entidade monitorada pelo EF
            agenciaNoBanco.Nome = dto.Nome;
            agenciaNoBanco.Cidade = dto.Cidade;
            agenciaNoBanco.EstadoUF = dto.EstadoUF;

            _context.SaveChanges();

            return NoContent(); // HTTP 204 No Content (Sucesso padrão REST para atualizações)
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var agencia = _context.Agencia.FirstOrDefault(a => a.Codigo == id);

            if (agencia == null)
            {
                return NotFound(new { message = $"Agência de código {id} não encontrada para exclusão." });
            }

            _context.Agencia.Remove(agencia);
            _context.SaveChanges();

            return Ok(new { success = true, message = "Agência excluída com sucesso diretamente pela API!" }); // HTTP 200
        }
    }
}
