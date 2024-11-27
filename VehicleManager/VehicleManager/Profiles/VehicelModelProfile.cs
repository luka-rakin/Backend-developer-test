using AutoMapper;
using VehicleManager.DTO;
using VehicleManager.Services.Dtos;
using VehicleManager.Services.Models;
using VehicleManager.Models;

namespace VehicleManager.Profiles
{
    public class VehicelModelProfile : Profile
    {

        public VehicelModelProfile()
        {
            CreateMap<VehicleModel, VehicleModelDto>()
                .ForMember(dest => dest.Make, src => src.MapFrom(x => x.VehicleMake.Name));

            CreateMap<CreateModelRequest, VehicleModel>();

            CreateMap<VehicleModel, VehicleModelViewModel>()
                .ForMember(dest => dest.Make, src => src.MapFrom(x => x.VehicleMake.Name));

            CreateMap<CreateModelViewModel, CreateModelRequest>();
            CreateMap<EditModelViewModel, EditModelRequest>();
            CreateMap<VehicleModelViewModel, EditModelRequest>();

        }
    }
}
