using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    public class AgenciaController : Controller
    {
        private readonly AppDbContext _context;

        public AgenciaController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Agencia.ToList();
            ViewBag.nomesenai = "SENAI";
            
            return View(lista); // Passa a lista para a View
        }



        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(string nome, string cidade, string estadoUF)
        {
            // Criamos o objeto manualmente com os dados que vieram do formulário
            var novaAgencia = new Agencia
            {
                Nome = nome,
                Cidade = cidade,
                EstadoUF = estadoUF
            };

            if (!string.IsNullOrEmpty(nome))
            {
                _context.Agencia.Add(novaAgencia);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Agencia/Editar/5
        [HttpGet]
        public IActionResult Editar(int id)
        {
            // Busca a agência pelo código (ID)
            var agencia = _context.Agencia.FirstOrDefault(a => a.Codigo == id);

            if (agencia == null)
            {
                return NotFound();
            }

            return View(agencia); // Passa o objeto para a View preencher os campos
        }

        // POST: Agencia/Editar
        [HttpPost]
        public IActionResult Editar(int codigo, string nome, string cidade, string estadoUF)
        {
            // Busca o registro existente no banco
            var agenciaNoBanco = _context.Agencia.FirstOrDefault(a => a.Codigo == codigo);

            if (agenciaNoBanco != null)
            {
                // Atualiza os atributos manualmente
                agenciaNoBanco.Nome = nome;
                agenciaNoBanco.Cidade = cidade;
                agenciaNoBanco.EstadoUF = estadoUF;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
        // GET: Agencia/Excluir/5
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            // Busca a agência para mostrar ao usuário o que ele está prestes a apagar
            var agencia = _context.Agencia.FirstOrDefault(a => a.Codigo == id);

            if (agencia == null)
            {
                return NotFound();
            }

            return View(agencia);
        }

        // POST: Agencia/ExcluirConfirmado
        [HttpPost]
        public IActionResult ExcluirConfirmado(int codigo)
        {
            var agencia = _context.Agencia.FirstOrDefault(a => a.Codigo == codigo);

            if (agencia != null)
            {
                _context.Agencia.Remove(agencia);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ExcluirConfirmadoModal(int codigo)
        {
            var agencia = _context.Agencia.FirstOrDefault(a => a.Codigo == codigo);

            if (agencia != null)
            {
                _context.Agencia.Remove(agencia);
                _context.SaveChanges();
                return Json(new { success = true, message = "Excluído com sucesso!" });
            }

            return Json(new { success = false, message = "Erro ao excluir." });
        }




    }
}
