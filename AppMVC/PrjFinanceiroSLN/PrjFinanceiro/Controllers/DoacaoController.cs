using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    public class DoacaoController : Controller
    {
        private readonly AppDbContext _context;

        public DoacaoController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Doacao.ToList();

            ViewBag.nomedoacao = "Doações";

            return View(lista);
        }

        // GET: Doacao/Criar
        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        // POST: Doacao/Criar
        [HttpPost]
        public IActionResult Criar(
            int iddoacao,
            int datadoacao,
            int idpessoa,
            string status,
            int idanimal)
        {
            // Criamos o objeto manualmente com os dados
            // que vieram do formulário
            var novaDoacao = new Doacao
            {
                IdDoacao = iddoacao,
                DataDoacao = datadoacao,
                Status = status,
                IdAnimal = idanimal,
                IdPessoa = idpessoa,
            };

            if (!string.IsNullOrEmpty(status))
            {
                _context.Doacao.Add(novaDoacao);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Doacao/Editar/5
        [HttpGet]
        public IActionResult Editar(int id)
        {
            // Busca a doação pelo ID
            var doacao = _context.Doacao
                .FirstOrDefault(d => d.IdDoacao == id);

            if (doacao == null)
            {
                return NotFound();
            }

            return View(doacao);
        }

        // POST: Doacao/Editar
        [HttpPost]
        public IActionResult Editar(
            int iddoacao,
            int datadoacao,
            string status,
            int idanimal,
            int idpessoa)
        {
            // Busca o registro existente no banco
            var doacaoNoSite = _context.Doacao
                .FirstOrDefault(d => d.IdDoacao == iddoacao);

            if (doacaoNoSite != null)
            {
                // Atualiza os atributos manualmente
                doacaoNoSite.IdDoacao = iddoacao;
                doacaoNoSite.DataDoacao = datadoacao;
                doacaoNoSite.Status = status;
                doacaoNoSite.IdAnimal = idanimal;
                doacaoNoSite.IdPessoa = idpessoa; 

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Doacao/Excluir/5
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            // Busca a doação para mostrar ao usuário
            // o que ele está prestes a apagar
            var doacao = _context.Doacao
                .FirstOrDefault(d => d.IdDoacao == id);

            if (doacao == null)
            {
                return NotFound();
            }

            return View(doacao);
        }

        // POST: Doacao/ExcluirConfirmado
        [HttpPost]
        public IActionResult ExcluirConfirmado(int codigo)
        {
            var doacao = _context.Doacao
                .FirstOrDefault(d => d.IdDoacao == codigo);

            if (doacao != null)
            {
                _context.Doacao.Remove(doacao);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // POST: Doacao/ExcluirConfirmadoModal
        [HttpPost]
        public IActionResult ExcluirConfirmadoModal(int codigo)
        {
            var doacao = _context.Doacao
                .FirstOrDefault(d => d.IdDoacao == codigo);

            if (doacao != null)
            {
                _context.Doacao.Remove(doacao);
                _context.SaveChanges();

                return Json(new
                {
                    success = true,
                    message = "Excluído com sucesso!"
                });
            }

            return Json(new
            {
                success = false,
                message = "Erro ao excluir."
            });
        }
    }
}
