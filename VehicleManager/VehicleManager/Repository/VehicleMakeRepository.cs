using VehicleManager.Models;

namespace VehicleManager.Repository
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        private readonly AppDbContext _context;

        public VehicleMakeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(VehicleMake vehicleMake)
        {
            _context.vehicleMakes.Add(vehicleMake);
            await _context.SaveChangesAsync();
            return vehicleMake.Id;
        }
    }
}
