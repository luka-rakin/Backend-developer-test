using AutoMapper;
using VehicleManager.Services.Dtos;
using VehicleManager.Services.Models;
using VehicleManager.Models;

namespace VehicleManager.Profiles
{
    public class VehicleMakeProfile : Profile
    {
        public VehicleMakeProfile() 
        {
            CreateMap<CreateMakeRequest, VehicleMake>();
            CreateMap<VehicleMake, VehicleMakeViewModel>();
            CreateMap<CreateMakeViewModel, CreateMakeRequest>();
            CreateMap<VehicleMakeViewModel, EditMakeRequest>();

            
        }
    }
}
