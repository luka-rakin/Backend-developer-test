using AutoMapper;
using VehicleManager.DTO;
using VehicleManager.Models;

namespace VehicleManager.Profiles
{
    public class VehicelModelProfile : Profile
    {

        public VehicelModelProfile()
        {
            CreateMap<VehicleModel, VehicleModelDto>()
                .ForMember(dest => dest.VehicleMake, src => src.MapFrom(x => x.VehicleMake.Name))
                .ReverseMap()
                .ForPath(dest => dest.VehicleMake, opt => opt.Ignore());
        }
    }
}
