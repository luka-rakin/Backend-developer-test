﻿using Microsoft.EntityFrameworkCore;
using VehicleManager.Services;
using VehicleManager.Services.Dtos;
using VehicleManager.Services.Enums;
using VehicleManager.Services.Generics;
using VehicleManager.Services.Models;


namespace VehicleManager.Services.Repository
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

        public async Task<PagedResult<VehicleModel>> GetPaged(int pageNumber, int pageSize, ModelSortOptions sortOption, int? makeId = null)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            IQueryable<VehicleModel> query = _context.vehicleModels.Include(v => v.VehicleMake);

            if (makeId.HasValue)
            {
                query = query.Where(v => v.VehicleMakeId == makeId.Value);
            }

            query = sortOption switch
            {
                ModelSortOptions.NameDesc => query.OrderByDescending(v => v.Name),
                ModelSortOptions.AbrvAsc => query.OrderBy(v => v.Abrv),
                ModelSortOptions.AbrvDesc => query.OrderByDescending(v => v.Abrv),
                ModelSortOptions.MakeAsc => query.OrderBy(v => v.VehicleMake.Name),
                ModelSortOptions.MakeDesc => query.OrderByDescending(v => v.VehicleMake.Name),
                _ => query.OrderBy(v => v.Name)
            };

            int totalCount = await query.CountAsync();

            var vehicleModels = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<VehicleModel>
            {
                Items = vehicleModels,
                TotalCount = totalCount,
            };
        }



        public async Task<bool> Update(int id, EditModelRequest request)
        {
            var existingVehicleModel = await _context.vehicleModels.Include(vm => vm.VehicleMake).FirstOrDefaultAsync(vm => vm.Id == id);

            if (existingVehicleModel == null)
            {
                return false;
            }

            existingVehicleModel.Name = request.Name;
            existingVehicleModel.Abrv = request.Abrv;

            _context.vehicleModels.Update(existingVehicleModel);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
