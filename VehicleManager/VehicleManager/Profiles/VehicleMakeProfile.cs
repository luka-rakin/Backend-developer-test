﻿using AutoMapper;
using System.ComponentModel;
using VehicleManager.DTO;
using VehicleManager.Models;

namespace VehicleManager.Profiles
{
    public class VehicleMakeProfile : Profile
    {
        public VehicleMakeProfile() 
        {
            CreateMap<VehicleMake, VehicleMakeDto>()
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.Abrv, src => src.MapFrom(x => x.Abrv));

            CreateMap<VehicleMakeDto, VehicleMake>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => 0))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.Abrv, src => src.MapFrom(x => x.Abrv));

            
        }
    }
}