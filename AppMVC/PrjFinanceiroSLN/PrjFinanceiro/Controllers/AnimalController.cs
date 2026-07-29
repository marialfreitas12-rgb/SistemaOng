using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    public class AnimalController : Controller
    {
        private readonly AppDbContext _context;

        public AnimalController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Animal.ToList();
            ViewBag.nomeanimal = "Animais Disponíveis";
            
            return View(lista); // Passa a lista para a View
        }



        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(string nome, string raca, string porte ,int idanimal,string status,string sexo,int idade,int dataresgate )
        {
            // Criamos o objeto manualmente com os dados que vieram do formulário
            var novoAnimal = new Animal
            {
                Nome = nome,
                Raça  = raca,
                Porte = porte,
                IdAnimal = idanimal,
                Status = status,
                Sexo = sexo,
                Dataresgate = dataresgate
            };

            if (!string.IsNullOrEmpty(nome))
            {
                _context.Animal.Add(novoAnimal);
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
            var animais = _context.Animal.FirstOrDefault(a => a.IdAnimal == id);

            if (animais == null)
            {
                return NotFound();
            }

            return View(animais); // Passa o objeto para a View preencher os campos
        }

        // POST: Agencia/Editar
        [HttpPost]
        public IActionResult Editar(string nome, string raca, string porte, int idanimal, string status, string sexo, int idade, int dataresgate)
        {
            // Busca o registro existente no banco
            var animalNoSite = _context.Animal.FirstOrDefault(a => a.IdAnimal == idanimal);

            if (animalNoSite != null)
            {
                // Atualiza os atributos manualmente
                animalNoSite.Nome = nome;
                animalNoSite.Sexo = sexo;
                animalNoSite.Status = status;
                animalNoSite.Porte = porte;
                animalNoSite.Raça = raca;
                animalNoSite.IdAnimal = idanimal;
                animalNoSite.Idade = idade;
                animalNoSite.Dataresgate = dataresgate;
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
            var animais = _context.Animal.FirstOrDefault(a => a.IdAnimal == id);

            if (animais == null)
            {
                return NotFound();
            }

            return View(animais);
        }

        // POST: Agencia/ExcluirConfirmado
        [HttpPost]
        public IActionResult ExcluirConfirmado(int codigo)
        {
            var animais = _context.Animal.FirstOrDefault(a => a.IdAnimal == codigo);

            if (animais != null)
            {
                _context.Animal.Remove(animais);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ExcluirConfirmadoModal(int codigo)
        {
            var animais= _context.Animal.FirstOrDefault(a => a.IdAnimal == codigo);

            if (animais != null)
            {
                _context.Animal.Remove(animais);
                _context.SaveChanges();
                return Json(new { success = true, message = "Excluído com sucesso!" });
            }

            return Json(new { success = false, message = "Erro ao excluir." });
        }




    }
}
