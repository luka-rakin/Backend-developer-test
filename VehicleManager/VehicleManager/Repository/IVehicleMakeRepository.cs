using VehicleManager.DTO;
using VehicleManager.Models;

namespace VehicleManager.Repository
{
    public interface IVehicleMakeRepository
    {
        public Task<int> Add(VehicleMake vehicleMake);
        public Task<VehicleMake> GetById(int id);
        public Task<List<VehicleMake>> GetAll();
        public Task<PagedResult<VehicleMake>> GetPaged(int pageNumber, int pageSize);
        public Task<bool> Update(int id, VehicleMake vehicleMake);
        public Task<bool> Delete(int id);
    }
}
