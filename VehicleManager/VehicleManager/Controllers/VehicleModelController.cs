using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleManager.Services.Dtos;
using VehicleManager.Services.Enums;
using VehicleManager.Services.Service;
using VehicleManager.Models;

namespace VehicleManager.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly ILogger<VehicleModelController> _logger;
        private readonly IMapper _mapper;
        private readonly IVehicleModelService _vehicleModelService;
        private readonly IVehicleMakeService _vehicleMakeService;

        public VehicleModelController(ILogger<VehicleModelController> logger, IVehicleModelService vehicleModelService, IVehicleMakeService vehicleMakeService, IMapper mapper)
        {
            _logger = logger;
            _vehicleModelService = vehicleModelService;
            _vehicleMakeService = vehicleMakeService;
            _mapper = mapper;
        }

        public async Task<IActionResult> VehicleModel(int pageNumber = 1, int pageSize = 5, string sortBy = "NameAsc", int? makeId = null)
        {

            try
            {
                if (Enum.TryParse(sortBy, out ModelSortOptions modelSortOption))
                {
                    var pagedVehicleModels = await _vehicleModelService.GetPaged(pageNumber, pageSize, modelSortOption, makeId);
                    var vehicleMakes = await _vehicleMakeService.GetAll();

                    var vehicleModelViewModels = _mapper.Map<List<VehicleModelViewModel>>(pagedVehicleModels.Items);

                    PagedResultViewModel<VehicleModelViewModel> pagedResultViewModel = new PagedResultViewModel<VehicleModelViewModel>
                    {
                        Items = vehicleModelViewModels,
                        TotalCount = pagedVehicleModels.TotalCount,
                        CurrentPage = pageNumber,
                        PageSize = pageSize,
                        TotalPages = (int)Math.Ceiling(pagedVehicleModels.TotalCount / (double)pageSize),
                    };

                    ViewBag.VehicleMakes = new SelectList(_mapper.Map<List<VehicleMakeViewModel>>(vehicleMakes), "Id", "Name");
                    return View(pagedResultViewModel);
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
                var vehicleMakes = await _vehicleMakeService.GetAll();
                var viewModel = new CreateModelViewModel
                {
                    VehicleMakes = _mapper.Map<List<VehicleMakeViewModel>>(vehicleMakes)
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                //TempData["ErrorMessage"] = $"An unexpected error occured.";
                //return View("~/Views/Shared/ErrorPage.cshtml");

                return StatusCode(500, new { Message = "An unexpected error occured." });
            }

            
        }

        public async Task<IActionResult> AddVehicleModel(CreateModelViewModel viewModel)
        {
            try
            {
                await _vehicleModelService.Add(_mapper.Map<CreateModelRequest>(viewModel));
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
                return View(_mapper.Map<VehicleModelViewModel>(vehicleModel));
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occured." });
            }
            
        }

        public async Task<IActionResult> EditVehicleModelSubmit(VehicleModelViewModel viewModel)
        {
            try
            {
                await _vehicleModelService.Update(viewModel.Id, _mapper.Map<EditModelRequest>(viewModel));
                return RedirectToAction("VehicleModel");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occured." });
            }
            
        }
    }
}
