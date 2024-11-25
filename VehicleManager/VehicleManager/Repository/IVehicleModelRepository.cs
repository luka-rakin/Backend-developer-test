﻿using VehicleManager.DTO;
using VehicleManager.Models;

namespace VehicleManager.Repository
{
    public interface IVehicleModelRepository
    {
        public Task<int> Add(VehicleModel vehicleModel);
        public Task<VehicleModel> GetById(int id);
        public Task<List<VehicleModel>> GetAll();
        public Task<PagedResult<VehicleModel>> GetPaged(int pageNumber, int pageSize);
        //public Task<bool> Update(int id, VehicleModel VehicleModel);
        public Task<bool> Delete(int id);
    }
}