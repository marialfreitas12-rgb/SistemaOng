using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoacaoApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DoacaoApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("lista")]
        public IActionResult ListarTodos()
        {
            var doacoes = _context.Doacao.ToList();

            return Ok(doacoes);
        }

        [HttpGet("ativas")]
        public IActionResult ListarTodos2()
        {
            var doacoes = _context.Doacao.ToList();

            return Ok(doacoes);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var doacao = _context.Doacao
                .FirstOrDefault(d => d.IdDoacao == id);

            if (doacao == null)
            {
                return NotFound(new
                {
                    message = $"Doação com código {id} não encontrada."
                });
            }

            return Ok(doacao);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] Doacao dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novaDoacao = new Doacao
            {
                IdDoacao = dto.IdDoacao,
                DataDoacao = dto.DataDoacao,
                Status = dto.Status,
                IdAnimal = dto.IdAnimal,
                 IdPessoa = dto.IdPessoa
            };

            _context.Doacao.Add(novaDoacao);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = novaDoacao.IdDoacao },
                novaDoacao
            );
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Doacao dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doacaoNoSite = _context.Doacao
                .FirstOrDefault(d => d.IdDoacao == id);

            if (doacaoNoSite == null)
            {
                return NotFound(new
                {
                    message = $"Doação de código {id} inexistente para atualização."
                });
            }

            doacaoNoSite.IdDoacao = dto.IdDoacao;
            doacaoNoSite.DataDoacao = dto.DataDoacao;
            doacaoNoSite.Status = dto.Status;
            doacaoNoSite.IdAnimal = dto.IdAnimal;
            doacaoNoSite.IdPessoa = dto.IdPessoa;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var doacao = _context.Doacao
                .FirstOrDefault(d => d.IdDoacao == id);

            if (doacao == null)
            {
                return NotFound(new
                {
                    message = $"Doação de código {id} não encontrada para exclusão."
                });
            }

            _context.Doacao.Remove(doacao);
            _context.SaveChanges();

            return Ok(new
            {
                success = true,
                message = "Doação excluída com sucesso diretamente pela API!"
            });
        }
    }
}