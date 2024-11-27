
using VehicleManager.Services.Dtos;
using VehicleManager.Services.Enums;
using VehicleManager.Services.Generics;
using VehicleManager.Services.Models;
using VehicleManager.Services.Repository;

namespace VehicleManager.Services.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly IVehicleMakeRepository _vehicleMakeRepository;

        public VehicleMakeService(IVehicleMakeRepository vehicleMakeRepository)
        {
            _vehicleMakeRepository = vehicleMakeRepository;
        }
        public async Task<int> Add(CreateMakeRequest request)
        {
            VehicleMake vehicleMake = new VehicleMake 
            { 
                Name = request.Name,
                Abrv = request.Abrv
            };
            return await _vehicleMakeRepository.Add(vehicleMake);
        }

        public async Task<bool> Delete(int id)
        {
            if(!await _vehicleMakeRepository.Delete(id))
            {
                return false;
            }

            return true;
        }

        public async Task<List<VehicleMake>> GetAll()
        {
            var vehicleMakes = await _vehicleMakeRepository.GetAll();
            return vehicleMakes;
        }

        public async Task<PagedResult<VehicleMake>> GetPaged(int pageNumber, int pageSize, MakeSortOptions sortOption)
        {

            var result = await _vehicleMakeRepository.GetPaged(pageNumber, pageSize, sortOption);
            return result;
            
        }

        public async Task<VehicleMake> GetById(int id)
        {
            var vehicleMake = await _vehicleMakeRepository.GetById(id);
            if(vehicleMake == null)
            {
                throw new KeyNotFoundException($"Vehicle make with given id does not exist.");
            }

            return vehicleMake;
        }

        public async Task Update(int id, EditMakeRequest request)
        {
            if(!await _vehicleMakeRepository.Update(id, request))
            {
                throw new Exception("Update of Vehicle mark failed.");
            }
        }
    }
}
