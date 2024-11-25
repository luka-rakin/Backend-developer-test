using AutoMapper;
using VehicleManager.DTO;
using VehicleManager.Enums;
using VehicleManager.Models;
using VehicleManager.Repository;

namespace VehicleManager.Service
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly IVehicleModelRepository _vehicleModelRepository;
        private readonly IVehicleMakeRepository _vehicleMakeRepository;
        private readonly IMapper _mapper;
        public VehicleModelService(IVehicleModelRepository vehicleModelRepository, IVehicleMakeRepository vehicleMakeRepository, IMapper mapper)
        {
            _vehicleModelRepository = vehicleModelRepository;
            _vehicleMakeRepository = vehicleMakeRepository;
            _mapper = mapper;
        }

        public async Task<int> Add(CreateModelRequest request)
        {
            var vehicleMake = await _vehicleMakeRepository.GetById(request.VehicleMakeId);

            if(vehicleMake == null)
            {
                throw new Exception($"Vehicle make with id {request.VehicleMakeId} does not exist.");
            }

            VehicleModel vehicleModel = _mapper.Map<VehicleModel>(request);
            vehicleModel.VehicleMake = vehicleMake;

            return await _vehicleModelRepository.Add(vehicleModel);
        }

        public async Task<bool> Delete(int id)
        {
            return await _vehicleModelRepository.Delete(id);
        }

        public async Task<List<VehicleModelDto>> GetAll()
        {
            var vehicleModels = await _vehicleModelRepository.GetAll();
            return _mapper.Map<List<VehicleModelDto>>(vehicleModels);
        }

        public async Task<VehicleModelDto> GetById(int id)
        {
            var vehicleModel = await _vehicleModelRepository.GetById(id);
            if(vehicleModel == null)
            {
                throw new BadHttpRequestException("Vehicle model with given id does not exist.");
            }
            return _mapper.Map<VehicleModelDto>(vehicleModel);
        }

        public async Task<PagedResult<VehicleModelDto>> GetPaged(int pageNumber, int pageSize, ModelSortOptions sortOptions, int? makeId = null)
        {
            var result = await _vehicleModelRepository.GetPaged(pageNumber, pageSize, sortOptions, makeId);
            List<VehicleModelDto> vehicleModelDtos = _mapper.Map<List<VehicleModelDto>>(result.Items);


            return new PagedResult<VehicleModelDto>
            {
                Items = vehicleModelDtos,
                TotalCount = result.TotalCount,
                TotalPages = result.TotalPages,
                CurrentPage = result.CurrentPage,
                PageSize = result.PageSize
            };
        }



        public async Task Update(int id, VehicleModelDto vehicleModelDto)
        {
            if(!await _vehicleModelRepository.Update(id, _mapper.Map<VehicleModel>(vehicleModelDto)))
            {
                throw new Exception("Update of Vehicle model failed.");
            }
        }
    }
}
