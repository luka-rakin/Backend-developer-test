
using VehicleManager.Services.Dtos;
using VehicleManager.Services.Enums;
using VehicleManager.Services.Generics;
using VehicleManager.Services.Models;

namespace VehicleManager.Services.Repository
{
    public interface IVehicleModelRepository
    {
        public Task<int> Add(VehicleModel vehicleModel);
        public Task<VehicleModel> GetById(int id);
        public Task<List<VehicleModel>> GetAll();
        public Task<PagedResult<VehicleModel>> GetPaged(int pageNumber, int pageSize, ModelSortOptions sortOption, int? makeId);
        public Task<bool> Update(int id, EditModelRequest request);
        public Task<bool> Delete(int id);
    }
}
