using VehicleManager.Services.Dtos;
using VehicleManager.Services.Enums;
using VehicleManager.Services.Generics;
using VehicleManager.Services.Models;

namespace VehicleManager.Services.Repository
{
    public interface IVehicleMakeRepository
    {
        public Task<int> Add(VehicleMake vehicleMake);
        public Task<VehicleMake> GetById(int id);
        public Task<List<VehicleMake>> GetAll();
        public Task<PagedResult<VehicleMake>> GetPaged(int pageNumber, int pageSize, MakeSortOptions sortOption);
        public Task<bool> Update(int id, EditMakeRequest request);
        public Task<bool> Delete(int id);
    }
}
