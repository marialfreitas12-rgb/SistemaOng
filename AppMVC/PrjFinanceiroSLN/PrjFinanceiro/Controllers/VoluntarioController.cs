using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.Models;
using System;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    public class VoluntarioController : Controller
    {
        private readonly AppDbContext _context;

        public VoluntarioController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Voluntario.ToList();
            ViewBag.nomesenai = "Voluntarios";

            return View(lista); // Passa a lista para a View
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(string nome, string disponibilidade, string cpf, string email, string telefone, int idvoluntario)
        {
            // Criamos o objeto manualmente com os dados que vieram do formulário
            var novoVoluntario = new Voluntario
            {
                Nome = nome,
                Disponibilidade = disponibilidade,
                Email= email,
                CPF = cpf,
                Telefone = telefone,
                Idvoluntario= idvoluntario
            };

            if (!string.IsNullOrEmpty(nome))
            {
                _context.Voluntario.Add(novoVoluntario);
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
            var voluntario = _context.Voluntario.FirstOrDefault(a => a.Idvoluntario == id);

            if (voluntario == null)
            {
                return NotFound();
            }

            return View(voluntario); // Passa o objeto para a View preencher os campos
        }

        // POST: Agencia/Editar
        [HttpPost]
        public IActionResult Editar(int Idvoluntario, string nome, string email, string cpf, string telefone, string disponibilidade)
        {
            // Busca o registro existente no banco
            var voluntarionoSite = _context.Voluntario.FirstOrDefault(a => a.Idvoluntario == Idvoluntario);

            if (voluntarionoSite != null)
            {
                // Atualiza os atributos manualmente
                voluntarionoSite.Nome = nome;
                voluntarionoSite.Idvoluntario = Idvoluntario;
                voluntarionoSite.Email = email;
                voluntarionoSite.Disponibilidade = disponibilidade;
                voluntarionoSite.CPF = cpf;
                voluntarionoSite.Telefone = telefone;

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
            var voluntario = _context.Voluntario.FirstOrDefault(a => a.Idvoluntario == id);

            if (voluntario == null)
            {
                return NotFound();
            }

            return View(voluntario);
        }

        // POST: Agencia/ExcluirConfirmado
        [HttpPost]
        public IActionResult ExcluirConfirmado(int Idvoluntario)
        {
            var voluntario = _context.Voluntario.FirstOrDefault(a => a.Idvoluntario== Idvoluntario);

            if (voluntario != null)
            {
                _context.Voluntario.Remove(voluntario);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }



    }
}
