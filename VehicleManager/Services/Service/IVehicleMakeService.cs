
using VehicleManager.Services.Dtos;
using VehicleManager.Services.Enums;
using VehicleManager.Services.Generics;
using VehicleManager.Services.Models;

namespace VehicleManager.Services.Service
{
    public interface IVehicleMakeService
    {
        public Task<int> Add(CreateMakeRequest request);
        public Task<VehicleMake> GetById(int id);
        public Task<List<VehicleMake>> GetAll();
        public Task<PagedResult<VehicleMake>> GetPaged(int pageNumber, int pageSize, MakeSortOptions sortOption);
        public Task Update(int id, EditMakeRequest request);
        public Task<bool> Delete(int id);
    }
}
