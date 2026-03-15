using App.Repositories.Service;
using App.Repositories.User;
using App.Repositories.Vehicles;
using App.Services.Servicess;
using App.Services.Servicess.Create;
using App.Services.Servicess.Update;
using App.Services.User.Create;
using App.Services.Vehicles;
using App.Services.Vehicles.Create;
using App.Services.Vehicles.Update;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            CreateMap<Vehicle,VehicleDto>().ReverseMap();
            CreateMap<CreateVehicleRequest, Vehicle>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateVehicleRequest, Vehicle>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<ServiceRecord, ServiceRecordsDto>().ReverseMap();
            CreateMap<CreateServiceRecordRequest,ServiceRecord>().ForMember(dest=> dest.Id, opt => opt.Ignore());
            CreateMap<UpdateServiceRecordRequest, ServiceRecord>().ForMember(dest=>dest.Id, opt => opt.Ignore());
            CreateMap<CreateUserRequest, Users>().ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.LastLogin, opt => opt.Ignore());





            //} CreateMap<UpdateVehicleRequest, Vehicle>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
        }
    }
}
