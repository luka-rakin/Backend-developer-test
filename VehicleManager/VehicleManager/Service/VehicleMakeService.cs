using AutoMapper;
using VehicleManager.DTO;
using VehicleManager.Models;
using VehicleManager.Repository;

namespace VehicleManager.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly IVehicleMakeRepository _vehicleMakeRepository;
        private readonly IMapper _mapper;

        public VehicleMakeService(IVehicleMakeRepository vehicleMakeRepository, IMapper mapper)
        {
            _vehicleMakeRepository = vehicleMakeRepository;
            _mapper = mapper;
        }
        public async Task<int> Add(VehicleMakeDto vehicleMakeDto)
        {
            return await _vehicleMakeRepository.Add(_mapper.Map<VehicleMake>(vehicleMakeDto));
        }

        public async Task Delete(int id)
        {
            if(!await _vehicleMakeRepository.Delete(id))
            {
                throw new Exception($"Deleting Vehicle make with id {id} failed");
            }
        }

        public async Task<List<VehicleMakeDto>> GetAll()
        {
            var vehicleMakes = await _vehicleMakeRepository.GetAll();
            return _mapper.Map<List<VehicleMakeDto>>(vehicleMakes);
        }

        public async Task<PagedResult<VehicleMakeDto>> GetPaged(int pageNumber, int pageSize)
        {

            var result = await _vehicleMakeRepository.GetPaged(pageNumber, pageSize);
            List<VehicleMakeDto> vehicleMarkDtos = _mapper.Map<List<VehicleMakeDto>>(result.Items);

            return new PagedResult<VehicleMakeDto>
            {
                Items = vehicleMarkDtos,
                TotalCount = result.TotalCount,
                TotalPages = result.TotalPages,
                CurrentPage = result.CurrentPage,
                PageSize = result.PageSize
            };
            
        }

        public async Task<VehicleMakeDto> GetById(int id)
        {
            var vehicleMake = await _vehicleMakeRepository.GetById(id);
            if(vehicleMake == null)
            {
                throw new Exception($"Vehicle make with id {id} does not exist.");
            }

            return _mapper.Map<VehicleMakeDto>(vehicleMake);
        }

        public async Task Update(int id, VehicleMakeDto vehicleMakeDto)
        {
            if(!await _vehicleMakeRepository.Update(id, _mapper.Map<VehicleMake>(vehicleMakeDto)))
            {
                throw new Exception("Update of Vehicle mark failed.");
            }
        }
    }
}
