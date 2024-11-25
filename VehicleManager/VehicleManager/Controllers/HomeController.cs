using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleManager.DTO;
using VehicleManager.Enums;
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
