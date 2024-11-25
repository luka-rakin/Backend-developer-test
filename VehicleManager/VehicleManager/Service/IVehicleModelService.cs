using VehicleManager.DTO;
using VehicleManager.Enums;

namespace VehicleManager.Service
{
    public interface IVehicleModelService
    {
        public Task<int> Add(CreateModelRequest request);
        public Task<VehicleModelDto> GetById(int id);
        public Task<List<VehicleModelDto>> GetAll();
        public Task<PagedResult<VehicleModelDto>> GetPaged(int pageNumber, int pageSize, ModelSortOptions sortOptions, int? makeId);
        public Task Update(int id, VehicleModelDto vehicleModelDto);
        public Task<bool> Delete(int id);
    }
}
