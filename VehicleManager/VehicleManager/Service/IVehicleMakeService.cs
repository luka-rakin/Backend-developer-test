using VehicleManager.Models;

namespace VehicleManager.Service
{
    public interface IVehicleMakeService
    {
        public Task<int> Add(VehicleMake vehicleMake);
        public Task<VehicleMake> GetById(int id);
        public Task<List<VehicleMake>> GetAll();
        public Task Update(int id, VehicleMake vehicleMake);
        public Task Delete(int id);
    }
}
