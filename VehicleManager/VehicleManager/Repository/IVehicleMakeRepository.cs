using VehicleManager.Models;

namespace VehicleManager.Repository
{
    public interface IVehicleMakeRepository
    {
        public Task<int> Add(VehicleMake vehicleMake);
    }
}
