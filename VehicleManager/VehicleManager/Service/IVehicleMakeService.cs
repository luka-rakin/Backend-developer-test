using VehicleManager.DTO;
using VehicleManager.Models;

namespace VehicleManager.Service
{
    public interface IVehicleMakeService
    {
        public Task<int> Add(VehicleMakeDto vehicleMakeDto);
        public Task<VehicleMakeDto> GetById(int id);
        public Task<List<VehicleMakeDto>> GetAll();
        public Task<PagedResult<VehicleMakeDto>> GetPaged(int pageNumber, int pageSize);
        public Task Update(int id, VehicleMakeDto vehicleMakeDto);
        public Task Delete(int id);
    }
}
