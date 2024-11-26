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
                    //TempData["ErrorMessage"] = $"Error: Unsuported sorting value";
                    //return View("~/Views/Shared/ErrorPage.cshtml");

                    return BadRequest(new { Message = "Unsupported sorting value." });
                }
            }
            catch (Exception ex) {
                //TempData["ErrorMessage"] = $"An unexpected error occured.";
                //return View("~/Views/Shared/ErrorPage.cshtml");

                return StatusCode(500, new { Message = "An unexpected error occured." });
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
                //TempData["ErrorMessage"] = $"An unexpected error occured.";
                //return View("~/Views/Shared/ErrorPage.cshtml");

                return StatusCode(500, new { Message = "An unexpected error occured." });
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
                    //return BadRequest(new { Message = "You cannot remove a vehicle make that has associated vehicle models." });
                }
                else
                {
                    TempData["SuccessMessage"] = "Vehicle make deleted successfully!";
                }

                return RedirectToAction("VehicleMake");
            }
            catch(Exception ex)
            {
                //TempData["ErrorMessage"] = $"An unexpected error occured.";
                //return View("~/Views/Shared/ErrorPage.cshtml");

                return StatusCode(500, new { Message = "An unexpected error occured." });
            }
            

        }

        public async Task<IActionResult> EditVehicleMakeForm(int id)
        {
            try
            {
                var vehicleMake = await _vehicleMakeService.GetById(id);
                return View(vehicleMake);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occured." });
            }
            
        }

        public async Task<IActionResult> EditVehicleMakeSubmit(VehicleMakeDto vehicleMakeDto)
        {
            try
            {
                await _vehicleMakeService.Update(vehicleMakeDto.Id, vehicleMakeDto);
                return RedirectToAction("VehicleMake");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occured." });
            }
            
        }
    }
}
