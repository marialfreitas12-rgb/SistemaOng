using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    public class EstadoController : Controller
    {
        private readonly AppDbContext _context;

        public EstadoController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Estado.ToList();
            ViewBag.nomesenai = "SENAI";
            
            return View(lista); // Passa a lista para a View
        }



        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(string NomeEstado, string sigla)
        {
            // Criamos o objeto manualmente com os dados que vieram do formulário
            var novaEstado = new Estado
            {
                NomeEstado = NomeEstado,
                Sigla = sigla
                
            };

            if (!string.IsNullOrEmpty(NomeEstado))
            {
                _context.Estado.Add(novaEstado);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Estado/Editar/5
        [HttpGet]
        public IActionResult Editar(int id)
        {
            // Busca a agência pelo código (ID)
            var agencia = _context.Estado.FirstOrDefault(a => a.Codigo == id);

            if (agencia == null)
            {
                return NotFound();
            }

            return View(agencia); // Passa o objeto para a View preencher os campos
        }

        // POST: Estado/Editar
        [HttpPost]
        public IActionResult Editar(int codigo, string NomeEstado, string sigla)
        {
            // Busca o registro existente no banco
            var  table = _context.Estado.FirstOrDefault(a => a.Codigo == codigo);

            if (table != null)
            {
                // Atualiza os atributos manualmente
                table.NomeEstado = NomeEstado;
                table.Sigla = sigla;
                

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
        // GET: Estado/Excluir/5
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            // Busca a agência para mostrar ao usuário o que ele está prestes a apagar
            var agencia = _context.Estado.FirstOrDefault(a => a.Codigo == id);

            if (agencia == null)
            {
                return NotFound();
            }

            return View(agencia);
        }

        // POST: Estado/ExcluirConfirmado
        [HttpPost]
        public IActionResult ExcluirConfirmado(int codigo)
        {
            var agencia = _context.Estado.FirstOrDefault(a => a.Codigo == codigo);

            if (agencia != null)
            {
                _context.Estado.Remove(agencia);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ExcluirConfirmadoModal(int codigo)
        {
            var agencia = _context.Estado.FirstOrDefault(a => a.Codigo == codigo);

            if (agencia != null)
            {
                _context.Estado.Remove(agencia);
                _context.SaveChanges();
                return Json(new { success = true, message = "Excluído com sucesso!" });
            }

            return Json(new { success = false, message = "Erro ao excluir." });
        }




    }
}
