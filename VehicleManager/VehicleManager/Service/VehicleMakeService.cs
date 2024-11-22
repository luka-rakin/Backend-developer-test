using VehicleManager.DTO;
using VehicleManager.Models;
using VehicleManager.Repository;

namespace VehicleManager.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly IVehicleMakeRepository _vehicleMakeRepository;

        public VehicleMakeService(IVehicleMakeRepository vehicleMakeRepository)
        {
            _vehicleMakeRepository = vehicleMakeRepository;
        }
        public async Task<int> Add(VehicleMake vehicleMake)
        {
            return await _vehicleMakeRepository.Add(vehicleMake);
        }

        public async Task Delete(int id)
        {
            if(!await _vehicleMakeRepository.Delete(id))
            {
                throw new Exception($"Deleting Vehicle make with id {id} failed");
            }
        }

        public Task<List<VehicleMake>> GetAll()
        {
            return _vehicleMakeRepository.GetAll();
        }

        public async Task<VehicleMake> GetById(int id)
        {
            var vehicleMake = await _vehicleMakeRepository.GetById(id);
            if(vehicleMake == null)
            {
                throw new Exception($"Vehicle with id {id} does not exist.");
            }

            return vehicleMake;
        }

        public async Task<PagedResult<VehicleMake>> GetPaged(int pageNumber, int pageSize)
        {
            return await _vehicleMakeRepository.GetPaged(pageNumber, pageSize);
        }

        public async Task Update(int id, VehicleMake vehicleMake)
        {
            if(!await _vehicleMakeRepository.Update(id, vehicleMake))
            {
                throw new Exception("Update of Vehicle mark failed.");
            }
        }
    }
}
