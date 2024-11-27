using VehicleManager.Services.Dtos;
using VehicleManager.Services.Enums;
using VehicleManager.Services.Generics;
using VehicleManager.Services.Models;
using VehicleManager.Services.Repository;

namespace VehicleManager.Services.Service
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly IVehicleModelRepository _vehicleModelRepository;
        private readonly IVehicleMakeRepository _vehicleMakeRepository;

        public VehicleModelService(IVehicleModelRepository vehicleModelRepository, IVehicleMakeRepository vehicleMakeRepository)
        {
            _vehicleModelRepository = vehicleModelRepository;
            _vehicleMakeRepository = vehicleMakeRepository;
        }

        public async Task<int> Add(CreateModelRequest request)
        {
            var vehicleMake = await _vehicleMakeRepository.GetById(request.VehicleMakeId);

            if(vehicleMake == null)
            {
                throw new Exception($"Vehicle make with id {request.VehicleMakeId} does not exist.");
            }

            VehicleModel vehicleModel = new VehicleModel
            {
                Name = request.Name,
                Abrv = request.Abrv,
                VehicleMake = vehicleMake,
            };

            return await _vehicleModelRepository.Add(vehicleModel);
        }

        public async Task<bool> Delete(int id)
        {
            return await _vehicleModelRepository.Delete(id);
        }

        public async Task<List<VehicleModel>> GetAll()
        {
            var vehicleModels = await _vehicleModelRepository.GetAll();
            return vehicleModels;
        }

        public async Task<VehicleModel> GetById(int id)
        {
            var vehicleModel = await _vehicleModelRepository.GetById(id);
            if(vehicleModel == null)
            {
                throw new KeyNotFoundException("Vehicle model with given id does not exist.");
            }
            return vehicleModel;
        }

        public async Task<PagedResult<VehicleModel>> GetPaged(int pageNumber, int pageSize, ModelSortOptions sortOptions, int? makeId = null)
        {
            var result = await _vehicleModelRepository.GetPaged(pageNumber, pageSize, sortOptions, makeId);
            return result;
        }



        public async Task Update(int id, EditModelRequest request)
        {
            if(!await _vehicleModelRepository.Update(id, request))
            {
                throw new Exception("Update of Vehicle model failed.");
            }
        }
    }
}
