using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleManager.DTO;
using VehicleManager.Enums;
using VehicleManager.Models;
using VehicleManager.Service;

namespace VehicleManager.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly ILogger<VehicleModelController> _logger;
        private readonly IVehicleModelService _vehicleModelService;
        private readonly IVehicleMakeService _vehicleMakeService;

        public VehicleModelController(ILogger<VehicleModelController> logger, IVehicleModelService vehicleModelService, IVehicleMakeService vehicleMakeService)
        {
            _logger = logger;
            _vehicleModelService = vehicleModelService;
            _vehicleMakeService = vehicleMakeService;
        }

        public async Task<IActionResult> VehicleModel(int pageNumber = 1, int pageSize = 5, string sortBy = "NameAsc", int? makeId = null)
        {

            try
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
                    //TempData["ErrorMessage"] = $"Error: Unsuported sorting value";
                    //return View("~/Views/Shared/ErrorPage.cshtml");

                    return BadRequest(new { Message = "Unsupported sorting value." });
                }
            }
            catch (Exception ex)
            {
                //TempData["ErrorMessage"] = $"An unexpected error occured.";
                //return View("~/Views/Shared/ErrorPage.cshtml");

                return StatusCode(500, new { Message = "An unexpected error occured." });
            }
            
        }

        public async Task<IActionResult> VehicleModelForm()
        {
            try
            {
                var model = await _vehicleMakeService.GetAll();
                return View(model);
            }
            catch (Exception ex)
            {
                //TempData["ErrorMessage"] = $"An unexpected error occured.";
                //return View("~/Views/Shared/ErrorPage.cshtml");

                return StatusCode(500, new { Message = "An unexpected error occured." });
            }

            
        }

        public async Task<IActionResult> AddVehicleModel(CreateModelRequest request)
        {
            try
            {
                await _vehicleModelService.Add(request);
                return RedirectToAction("VehicleModel");
            }
            catch (Exception ex)
            {
                //TempData["ErrorMessage"] = $"An unexpected error occured.";
                //return View("~/Views/Shared/ErrorPage.cshtml");

                return StatusCode(500, new { Message = "An unexpected error occured." });
            }

            
        }

        public async Task<IActionResult> DeleteVehicleModel(int id)
        {
            try
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
            catch (Exception ex)
            {
                //TempData["ErrorMessage"] = $"An unexpected error occured.";
                //return View("~/Views/Shared/ErrorPage.cshtml");

                return StatusCode(500, new { Message = "An unexpected error occured." });
            }
            
        }

        public async Task<IActionResult> EditVehicleModelForm(int id)
        {
            try
            {
                var vehicleModel = await _vehicleModelService.GetById(id);
                return View(vehicleModel);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occured." });
            }
            
        }

        public async Task<IActionResult> EditVehicleModelSubmit(VehicleModelDto vehicleModelDto)
        {
            try
            {
                await _vehicleModelService.Update(vehicleModelDto.Id, vehicleModelDto);
                return RedirectToAction("VehicleModel");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occured." });
            }
            
        }
    }
}
