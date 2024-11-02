using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SeniorConnect.Domain.Commands;
using SeniorConnect.Domain.Models;

namespace SeniorConnect.Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MigrationCommand _migrationCommand = new();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("test")] 
        public IActionResult GetTestJson()
        {
            _migrationCommand.MigrateDatabase([]);
            var response = new { test = "The database works!!" }; 
            return Json(response);
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