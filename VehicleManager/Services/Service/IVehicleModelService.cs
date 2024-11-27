
using VehicleManager.Services.Dtos;
using VehicleManager.Services.Enums;
using VehicleManager.Services.Generics;
using VehicleManager.Services.Models;

namespace VehicleManager.Services.Service
{
    public interface IVehicleModelService
    {
        public Task<int> Add(CreateModelRequest request);
        public Task<VehicleModel> GetById(int id);
        public Task<List<VehicleModel>> GetAll();
        public Task<PagedResult<VehicleModel>> GetPaged(int pageNumber, int pageSize, ModelSortOptions sortOptions, int? makeId);
        public Task Update(int id, EditModelRequest request);
        public Task<bool> Delete(int id);
    }
}
