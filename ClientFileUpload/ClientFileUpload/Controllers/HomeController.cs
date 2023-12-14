using ClientFileUpload.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace ClientFileUpload.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetOrders()
        {
            // Replace "PathToYourOrderJsonFile" with the actual path to your Order.json file
            var jsonFilePath = "JsonFile.json";

            // Read JSON file content
            var jsonData = System.IO.File.ReadAllText(jsonFilePath);

            // Deserialize JSON data to a list of Order objects
            var orders = JsonSerializer.Deserialize<List<Order>>(jsonData);

            // Return the JSON data
            return Json(orders);
        }
    }
}