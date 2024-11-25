using VehicleManager.DTO;

namespace VehicleManager.Service
{
    public interface IVehicleModelService
    {
        public Task<int> Add(CreateModelRequest request);
        public Task<VehicleModelDto> GetById(int id);
        public Task<List<VehicleModelDto>> GetAll();
        public Task<PagedResult<VehicleModelDto>> GetPaged(int pageNumber, int pageSize);
        //public Task Update(int id, VehicleModelDto vehicleModelDto);
        public Task Delete(int id);
    }
}
