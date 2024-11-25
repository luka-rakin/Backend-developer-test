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
        private readonly IVehicleModelService _vehicleModelService;

        public HomeController(ILogger<HomeController> logger, IVehicleMakeService vehicleMakeService, IVehicleModelService vehicleModelService)
        {
            _logger = logger;
            _vehicleMakeService = vehicleMakeService;
            _vehicleModelService = vehicleModelService;
        }


        public async Task<IActionResult> VehicleMake()
        {
            var allVehicleMakes = await _vehicleMakeService.GetAll();
            return View(allVehicleMakes);
        }

        public async Task<IActionResult> VehicleModel()
        {
            var vehicleModels = await _vehicleModelService.GetAll();
            return View(vehicleModels);
        }

        public IActionResult VehicleMakeForm()
        {
            return View();
        }

        public async Task<IActionResult> VehicleModelForm()
        {
            var model = await _vehicleMakeService.GetAll();
            return View(model);
        }

        public async Task<IActionResult> AddVehicleModel(CreateModelRequest request)
        {
            await _vehicleModelService.Add(request);
            return RedirectToAction("VehicleModel");
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
