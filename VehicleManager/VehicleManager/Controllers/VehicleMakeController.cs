using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleManager.DTO;
using VehicleManager.Services.Dtos;
using VehicleManager.Services.Enums;
using VehicleManager.Services.Service;
using VehicleManager.Models;
using System.Globalization;

namespace VehicleManager.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly ILogger<VehicleMakeController> _logger;
        private readonly IVehicleMakeService _vehicleMakeService;
        private readonly IMapper _mapper;

        public VehicleMakeController(ILogger<VehicleMakeController> logger, IVehicleMakeService vehicleMakeService, IMapper mapper)
        {
            _logger = logger;
            _vehicleMakeService = vehicleMakeService;
            _mapper = mapper;
        }
        //public async Task<IActionResult> VehicleMake(int pageNumber = 1, int pageSize = 5, string sortBy = "NameAsc")
        //{
        //    try
        //    {
        //        if (Enum.TryParse(sortBy, out MakeSortOptions makeSortOption))
        //        {
        //            var pagedVehicleMakes = await _vehicleMakeService.GetPaged(pageNumber, pageSize, makeSortOption);

        //            var vehicleMakeViewModels = _mapper.Map<List<VehicleMakeViewModel>>(pagedVehicleMakes.Items);

        //            PagedResultViewModel<VehicleMakeViewModel> pagedResultViewModel = new PagedResultViewModel<VehicleMakeViewModel>
        //            {
        //                Items = vehicleMakeViewModels,
        //                TotalCount = pagedVehicleMakes.TotalCount,
        //                CurrentPage = pageNumber,
        //                PageSize = pageSize,
        //                TotalPages = (int)Math.Ceiling(pagedVehicleMakes.TotalCount / (double)pageSize),
        //            };

        //            return View(pagedResultViewModel);
        //        }
        //        else
        //        {
        //            //TempData["ErrorMessage"] = $"Error: Unsuported sorting value";
        //            //return View("~/Views/Shared/ErrorPage.cshtml");

        //            return BadRequest(new { Message = "Unsupported sorting value." });
        //        }
        //    }
        //    catch (Exception ex) {
        //        //TempData["ErrorMessage"] = $"An unexpected error occured.";
        //        //return View("~/Views/Shared/ErrorPage.cshtml");

        //        return StatusCode(500, new { Message = "An unexpected error occured." });
        //    }


        //}

        public async Task<IActionResult> VehicleMake(MakeDisplayViewModel viewModel)
        {
            try
            {
                if (TempData["CurrentPage"] != null)
                {
                    viewModel.PagedResultViewModel.CurrentPage = (int)TempData["CurrentPage"];
                    TempData["CurrentPage"] = null;
                }
                    
                
                if (TempData["PageSize"] != null)
                {
                    viewModel.PagedResultViewModel.PageSize = (int)TempData["PageSize"];
                    TempData["PageSize"] = null;
                }
                if (TempData["SortBy"] != null)
                {
                    viewModel.SortBy = TempData["SortBy"].ToString();
                    TempData["SortBy"] = null;
                }


                if (Enum.TryParse(viewModel.SortBy, out MakeSortOptions makeSortOption))
                {
                    var pagedVehicleMakes = await _vehicleMakeService.GetPaged(viewModel.PagedResultViewModel.CurrentPage, viewModel.PagedResultViewModel.PageSize, makeSortOption);

                    var vehicleMakeViewModels = _mapper.Map<List<VehicleMakeViewModel>>(pagedVehicleMakes.Items);

                    PagedResultViewModel<VehicleMakeViewModel> pagedResultViewModel = new PagedResultViewModel<VehicleMakeViewModel>
                    {
                        Items = vehicleMakeViewModels,
                        TotalCount = pagedVehicleMakes.TotalCount,
                        CurrentPage = viewModel.PagedResultViewModel.CurrentPage,
                        PageSize = viewModel.PagedResultViewModel.PageSize,
                        TotalPages = (int)Math.Ceiling(pagedVehicleMakes.TotalCount / (double)viewModel.PagedResultViewModel.PageSize),
                    };

                    viewModel.PagedResultViewModel = pagedResultViewModel;

                    return View(viewModel);
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

        public IActionResult VehicleMakeForm()
        {
            return View();
        }

        public async Task<IActionResult> AddVehicleMake(CreateMakeViewModel viewModel)
        {
            try
            {
                await _vehicleMakeService.Add(_mapper.Map<CreateMakeRequest>(viewModel));
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
                return View(_mapper.Map<VehicleMakeViewModel>(vehicleMake));
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

        public async Task<IActionResult> EditVehicleMakeSubmit(VehicleMakeViewModel viewModel)
        {
            try
            {
                await _vehicleMakeService.Update(viewModel.Id, _mapper.Map<EditMakeRequest>(viewModel));
                return RedirectToAction("VehicleMake");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occured." });
            }
            
        }

        public async Task<IActionResult> ChangePage(int pageNumber, int pageSize, string sortBy)
        {
            TempData["CurrentPage"] = pageNumber;
            TempData["PageSize"] = pageSize;
            TempData["SortBy"] = sortBy;

            return RedirectToAction("VehicleMake");

        }


    }
}
