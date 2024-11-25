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


        public async Task<IActionResult> VehicleMake(int pageNumber = 1, int pageSize = 5, string sortBy = "NameAsc")
        {
            if(Enum.TryParse(sortBy, out MakeSortOptions makeSortOption))
            {
                var pagedVehicleMakes = await _vehicleMakeService.GetPaged(pageNumber, pageSize, makeSortOption);
                return View(pagedVehicleMakes);
            }
            else
            {
                throw new Exception("Unsuported sorting value");
            }
            
        }

        public async Task<IActionResult> VehicleModel(int pageNumber = 1, int pageSize = 5, string sortBy = "NameAsc", int? makeId = null)
        {
            if (Enum.TryParse(sortBy, out ModelSortOptions modelSortOption))
            {
                var pagedVehicleModels = await _vehicleModelService.GetPaged(pageNumber, pageSize, modelSortOption, makeId);
                var vehicleMakes = await _vehicleMakeService.GetAll();

                ViewBag.VehicleMakes = new SelectList(vehicleMakes, "Id", "Name");
                return View(pagedVehicleModels);
            }
            else
            {
                throw new Exception("Unsuported sorting value");
            }
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

        public async Task<IActionResult> DeleteVehicleMake(int id)
        {
            var result = await _vehicleMakeService.Delete(id);
            if (!result)
            {
                TempData["ErrorMessage"] = $"You cannot remove a vehicle make that has an associated vehicle model.";
            }
            else
            {
                TempData["SuccessMessage"] = "Vehicle make deleted successfully!";
            }

            return RedirectToAction("VehicleMake");

        }

        public async Task<IActionResult> DeleteVehicleModel(int id)
        {
            var result = await _vehicleModelService.Delete(id);
            if (!result)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting the vehicle model";
            }
            else
            {
                TempData["SuccessMessage"] = "Vehicle model deleted successfully!";
            }

            return RedirectToAction("VehicleModel");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
