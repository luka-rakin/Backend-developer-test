using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using VehicleManager.DTO;
using VehicleManager.Enums;
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
            bool hasRelatedVehicleModels = await _context.vehicleModels.AnyAsync(vm => vm.VehicleMakeId == id);

            if (hasRelatedVehicleModels)
            {
                return false;
            }

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
            return await _context.vehicleMakes
                .OrderBy(v => v.Name)
                .ToListAsync();
        }

        public async Task<VehicleMake> GetById(int id)
        {
            return await _context.vehicleMakes.FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<PagedResult<VehicleMake>> GetPaged(int pageNumber, int pageSize, MakeSortOptions sortOption)
        {
            if(pageNumber < 1)
            {
                pageNumber = 1;
            }

            IQueryable<VehicleMake> query = _context.vehicleMakes;

            query = sortOption switch
            {
                MakeSortOptions.NameDesc => query.OrderByDescending(v => v.Name),
                MakeSortOptions.AbrvAsc => query.OrderBy(v => v.Abrv),
                MakeSortOptions.AbrvDesc => query.OrderByDescending(v => v.Abrv),
                _ => query.OrderBy(v => v.Name)
            };

            int totalCount = await query.CountAsync();

            var vehicleMakes = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PagedResult<VehicleMake>
            {
                Items = vehicleMakes,
                TotalPages = totalPages,
                TotalCount = totalCount,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };       
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
