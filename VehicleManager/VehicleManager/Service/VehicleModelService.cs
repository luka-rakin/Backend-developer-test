using AutoMapper;
using VehicleManager.DTO;
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

        public async Task Delete(int id)
        {
            var result = await _vehicleModelRepository.Delete(id);
            if(!result)
            {
                throw new Exception("An error occurred while deleting the vehicle model");
            }
        }

        public async Task<List<VehicleModelDto>> GetAll()
        {
            var vehicleModels = await _vehicleModelRepository.GetAll();
            return _mapper.Map<List<VehicleModelDto>>(vehicleModels);
        }

        public async Task<VehicleModelDto> GetById(int id)
        {
            var vehicleModel = await _vehicleModelRepository.GetById(id);
            return _mapper.Map<VehicleModelDto>(vehicleModel);
        }

        public async Task<PagedResult<VehicleModelDto>> GetPaged(int pageNumber, int pageSize)
        {
            var result = await _vehicleModelRepository.GetPaged(pageNumber, pageSize);
            List<VehicleModelDto> vehicleModelDtos = _mapper.Map<List<VehicleModelDto>>(result);


            return new PagedResult<VehicleModelDto>
            {
                Items = vehicleModelDtos,
                TotalCount = result.TotalCount,
                TotalPages = result.TotalPages,
                CurrentPage = result.CurrentPage,
                PageSize = result.PageSize
            };
        }


        
        //public Task Update(int id, VehicleModelDto vehicleModelDto)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
