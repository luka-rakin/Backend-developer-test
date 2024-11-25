using Microsoft.AspNetCore.Mvc;
using VehicleManager.DTO;
using VehicleManager.Enums;
using VehicleManager.Service;

namespace VehicleManager.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly ILogger<VehicleMakeController> _logger;
        private readonly IVehicleMakeService _vehicleMakeService;

        public VehicleMakeController(ILogger<VehicleMakeController> logger, IVehicleMakeService vehicleMakeService)
        {
            _logger = logger;
            _vehicleMakeService = vehicleMakeService;
        }
        public async Task<IActionResult> VehicleMake(int pageNumber = 1, int pageSize = 5, string sortBy = "NameAsc")
        {
            try
            {
                if (Enum.TryParse(sortBy, out MakeSortOptions makeSortOption))
                {
                    var pagedVehicleMakes = await _vehicleMakeService.GetPaged(pageNumber, pageSize, makeSortOption);
                    return View(pagedVehicleMakes);
                }
                else
                {
                    TempData["ErrorMessage"] = $"Error: Unsuported sorting value";
                    return View("~/Views/Shared/ErrorPage.cshtml");
                }
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"An unexpected error occured.";
                return View("~/Views/Shared/ErrorPage.cshtml");
            }
            

        }

        public IActionResult VehicleMakeForm()
        {
            return View();
        }

        public async Task<IActionResult> AddVehicleMake(VehicleMakeDto model)
        {
            try
            {
                await _vehicleMakeService.Add(model);
                return RedirectToAction("VehicleMake");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An unexpected error occured.";
                return View("~/Views/Shared/ErrorPage.cshtml");
            }
            
        }

        public async Task<IActionResult> DeleteVehicleMake(int id)
        {
            try
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
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = $"An unexpected error occured.";
                return View("~/Views/Shared/ErrorPage.cshtml");
            }
            

        }
    }
}
