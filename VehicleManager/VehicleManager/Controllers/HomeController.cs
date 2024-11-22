using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VehicleManager.DTO;
using VehicleManager.Models;
using VehicleManager.Repository;
using VehicleManager.Service;

namespace VehicleManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleMakeService _vehicleMakeService;

        public HomeController(ILogger<HomeController> logger, IVehicleMakeService vehicleMakeService)
        {
            _logger = logger;
            _vehicleMakeService = vehicleMakeService;
        }

        public async Task<IActionResult> Index()
        {
            VehicleMakeDto vehicleMake = new VehicleMakeDto
            {
                Name = "Volkswagen",
                Abrv = "VW"
            };

            await _vehicleMakeService.Add(vehicleMake);

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
