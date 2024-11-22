using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
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

        public async Task<bool> Delete(int id)
        {
            var vehicleMake = await _context.vehicleMakes.FindAsync(id);

            if(vehicleMake == null)
            {
                return false;
            }

            _context.vehicleMakes.Remove(vehicleMake);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<VehicleMake>> GetAll()
        {
            return await _context.vehicleMakes.ToListAsync();
        }

        public async Task<VehicleMake> GetById(int id)
        {
            return await _context.vehicleMakes.FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<bool> Update(int id, VehicleMake vehicleMake)
        {
            var existingVehicleMake = await _context.vehicleMakes.FirstOrDefaultAsync(v => v.Id == id);

            if(existingVehicleMake == null)
            {
                return false;
            }

            existingVehicleMake.Name = vehicleMake.Name;
            existingVehicleMake.Abrv = vehicleMake.Abrv;

            _context.vehicleMakes.Update(existingVehicleMake);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
