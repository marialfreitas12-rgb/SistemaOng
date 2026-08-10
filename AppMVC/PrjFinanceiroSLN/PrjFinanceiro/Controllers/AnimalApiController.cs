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
    public class AnimalApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnimalApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("lista")]
        public IActionResult ListarTodos()
        {
            var animais = _context.Animal.ToList();
            return Ok(animais); // Status HTTP 200 OK com o JSON da lista
        }

        [HttpGet("ativas")]
        public IActionResult ListarTodos2()
        {
            var animais = _context.Animal.ToList();
            return Ok(animais); // Status HTTP 200 OK com o JSON da lista
        }


        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var animais = _context.Animal.FirstOrDefault(a => a.IdAnimal == id);

            if (animais == null)
            {
                return NotFound(new { message = $"Animal com código {id} não encontrada." }); // HTTP 404
            }

            return Ok(animais); // HTTP 200 OK
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] Animal dto)
        {
            // O [ApiController] já faz essa validação automaticamente, mas demonstrar em código reforça o aprendizado
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // HTTP 400 Bad Request
            }

            var novoAnimal = new Animal
            {
                Nome = dto.Nome,
                Sexo = dto.Sexo,
                Status = dto.Status,
                Porte = dto.Porte,
                Raça = dto.Raça,
                IdAnimal = dto.IdAnimal,
                Idade = dto.Idade,
                Dataresgate = dto.Dataresgate,

            };

            _context.Animal.Add(novoAnimal);
            _context.SaveChanges();

            // Padrão REST excelente: Retorna Status 201 Created, popula o cabeçalho 'Location' com a URL de consulta 
            // e entrega o objeto recém-criado com a ID gerada pelo banco de dados.
            return CreatedAtAction(nameof(BuscarPorId), new { id = novoAnimal.IdAnimal }, novoAnimal);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Animal dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var animalNoSite = _context.Animal.FirstOrDefault(a => a.IdAnimal == id);

            if (animalNoSite == null)
            {
                return NotFound(new { message = $"Animal de código {id} inexistente para atualização." });
            }

            // Mapeando dados do DTO para a entidade monitorada pelo EF
            animalNoSite.Nome = dto.Nome;
            animalNoSite.Sexo = dto.Sexo;
            animalNoSite.Status = dto.Status;
            animalNoSite.Porte = dto.Porte;
            animalNoSite.Raça = dto.Raça;
            animalNoSite.IdAnimal = dto.IdAnimal;
            animalNoSite.Idade = dto.Idade;
            animalNoSite.Dataresgate = dto.Dataresgate;
            

            _context.SaveChanges();

            return NoContent(); // HTTP 204 No Content (Sucesso padrão REST para atualizações)
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var animais = _context.Animal.FirstOrDefault(a => a.IdAnimal == id);

            if (animais == null)
            {
                return NotFound(new { message = $"Animal de código {id} não encontrado para exclusão." });
            }

            _context.Animal.Remove(animais);
            _context.SaveChanges();

            return Ok(new { success = true, message = "Animal excluída com sucesso diretamente pela API!" }); // HTTP 200
        }
    }
}
