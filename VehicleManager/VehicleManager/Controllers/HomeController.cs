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


        public async Task<IActionResult> VehicleMake()
        {
            var allVehicleMakes = await _vehicleMakeService.GetAll();
            return View(allVehicleMakes);
        }

        public IActionResult VehicleModel()
        {
            return View();
        }

        public IActionResult VehicleMakeForm()
        {
            return View();
        }

        public async Task<IActionResult> AddVehicleMake(VehicleMakeDto model)
        {
            await _vehicleMakeService.Add(model);
            return RedirectToAction("VehicleMake");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
