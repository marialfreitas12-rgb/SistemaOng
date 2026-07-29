using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrjFinanceiro.Models;
using System.Diagnostics;

namespace PrjFinanceiro.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //TODO
        public IActionResult Index()
        {
            _logger.LogInformation("Acesse a página inicial.");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
