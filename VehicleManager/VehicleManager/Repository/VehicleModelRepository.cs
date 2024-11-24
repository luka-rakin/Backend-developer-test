using Microsoft.EntityFrameworkCore;
using VehicleManager.DTO;
using VehicleManager.Models;

namespace VehicleManager.Repository
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private readonly AppDbContext _context;
        public VehicleModelRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(VehicleModel vehicleModel)
        {
            _context.vehicleModels.Add(vehicleModel);
            await _context.SaveChangesAsync();
            return vehicleModel.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var vehicleModel = await _context.vehicleModels.FirstOrDefaultAsync(v => v.Id == id);
            if(vehicleModel == null)
            {
                return false;
            }

            _context.vehicleModels.Remove(vehicleModel);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<VehicleModel>> GetAll()
        {
            return await _context.vehicleModels.Include(v => v.VehicleMake).ToListAsync();
        }

        public async Task<VehicleModel> GetById(int id)
        {
            return await _context.vehicleModels.Include(v => v.VehicleMake).FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<PagedResult<VehicleModel>> GetPaged(int pageNumber, int pageSize)
        {
            int totalCount = _context.vehicleMakes.Count();

            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            var vehicleModels = await _context.vehicleModels
                .Include(v => v.VehicleMake)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PagedResult<VehicleModel>
            {
                Items = vehicleModels,
                TotalPages = totalPages,
                TotalCount = totalCount,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }


        
        //public Task<bool> Update(int id, VehicleModel VehicleModel)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
