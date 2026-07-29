using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.Models;
using System;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly AppDbContext _context;

        public FuncionarioController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Funcionario.ToList();
            ViewBag.nomesenai = "SENAI";

            return View(lista); // Passa a lista para a View
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(string nome, string cidade, string estadoUF, string data, string cpf, string telefone)
        {
            // Criamos o objeto manualmente com os dados que vieram do formulário
            var novoFuncionario = new Funcionario
            {
                Nome = nome,
                DataNascimento = Convert.ToDateTime(data),
                Cidade = cidade,
                EstadoUF = estadoUF,
                CPF = cpf,
                Telefone = telefone
            };

            if (!string.IsNullOrEmpty(nome))
            {
                _context.Funcionario.Add(novoFuncionario);
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
            var funcionario = _context.Funcionario.FirstOrDefault(a => a.Codigo == id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario); // Passa o objeto para a View preencher os campos
        }

        // POST: Agencia/Editar
        [HttpPost]
        public IActionResult Editar(int codigo, string nome, string cidade, string estadoUF, string data, string cpf, string telefone)
        {
            // Busca o registro existente no banco
            var funcionarioNoBanco = _context.Funcionario.FirstOrDefault(a => a.Codigo == codigo);

            if (funcionarioNoBanco != null)
            {
                // Atualiza os atributos manualmente
                funcionarioNoBanco.Nome = nome;
                funcionarioNoBanco.DataNascimento = Convert.ToDateTime(data);
                funcionarioNoBanco.Cidade = cidade;
                funcionarioNoBanco.EstadoUF = estadoUF;
                funcionarioNoBanco.CPF = cpf;
                funcionarioNoBanco.Telefone = telefone;

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
            var funcionario = _context.Funcionario.FirstOrDefault(a => a.Codigo == id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Agencia/ExcluirConfirmado
        [HttpPost]
        public IActionResult ExcluirConfirmado(int codigo)
        {
            var funcionario = _context.Funcionario.FirstOrDefault(a => a.Codigo == codigo);

            if (funcionario != null)
            {
                _context.Funcionario.Remove(funcionario);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }



    }
}
