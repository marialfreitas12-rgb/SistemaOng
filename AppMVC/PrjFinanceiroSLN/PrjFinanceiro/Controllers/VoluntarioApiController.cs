using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PrjFinanceiro.DTOs;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoluntarioApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VoluntarioApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("lista")]
        public IActionResult ListarTodos()
        {
            var voluntario = _context.Voluntario.ToList();
            return Ok(voluntario); // Status HTTP 200 OK com o JSON da lista
        }

        [HttpGet("ativas")]
        public IActionResult ListarTodos2()
        {
            var voluntario = _context.Animal.ToList();
            return Ok(voluntario); // Status HTTP 200 OK com o JSON da lista
        }


        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var voluntario= _context.Voluntario.FirstOrDefault(a => a.Idvoluntario == id);

            if (voluntario== null)
            {
                return NotFound(new { message = $"Voluntario com código {id} não encontrado." }); // HTTP 404
            }

            return Ok(voluntario); // HTTP 200 OK
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] Voluntario dto)
        {
            // O [ApiController] já faz essa validação automaticamente, mas demonstrar em código reforça o aprendizado
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // HTTP 400 Bad Request
            }

            var novoVoluntario = new Voluntario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Telefone = dto.Telefone,
                CPF = dto.CPF,
                Disponibilidade = dto.Disponibilidade,
                Idvoluntario = dto.Idvoluntario,

            };

            _context.Voluntario.Add(novoVoluntario);
            _context.SaveChanges();

            // Padrão REST excelente: Retorna Status 201 Created, popula o cabeçalho 'Location' com a URL de consulta 
            // e entrega o objeto recém-criado com a ID gerada pelo banco de dados.
            return CreatedAtAction(nameof(BuscarPorId), new { id = novoVoluntario.Idvoluntario }, novoVoluntario);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Voluntario dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var voluntarioNoSite = _context.Voluntario.FirstOrDefault(a => a.Idvoluntario == id);

            if (voluntarioNoSite == null)
            {
                return NotFound(new { message = $"Voluntário de código {id} inexistente para atualização." });
            }

            // Mapeando dados do DTO para a entidade monitorada pelo EF
            voluntarioNoSite.Nome = dto.Nome;
            voluntarioNoSite.CPF = dto.CPF;
            voluntarioNoSite.Email = dto.Email;
            voluntarioNoSite.Disponibilidade = dto.Disponibilidade;
            voluntarioNoSite.Telefone = dto.Telefone;
            voluntarioNoSite.Idvoluntario = dto.Idvoluntario;
           

            _context.SaveChanges();

            return NoContent(); // HTTP 204 No Content (Sucesso padrão REST para atualizações)
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var voluntario = _context.Voluntario.FirstOrDefault(a => a.Idvoluntario == id);

            if (voluntario == null)
            {
                return NotFound(new { message = $"Voluntário de código {id} não encontrado para exclusão." });
            }

            _context.Voluntario.Remove(voluntario);
            _context.SaveChanges();

            return Ok(new { success = true, message = "Voluntário excluído com sucesso diretamente pela API!" }); // HTTP 200
        }
    }
}