using AutoMapper;
using Garage3.Core;
using Garage3.ViewModels;

namespace Garage3.AutoMapper
{
    public class MapperProfile : Profile
    {

        public MapperProfile() 
        {
            CreateMap<Member, MemberIndexViewModel>()
                .ForMember(
                    dest => dest.NrOfVehicles,
                    from => from.MapFrom(m => m.Vehicles.Count));
            CreateMap<Member, MemberDetailsViewModel>()
                .ForMember(
                    dest => dest.NrOfVehicles,
                    from => from.MapFrom(m => m.Vehicles.Count));
            CreateMap<Vehicle, VehicleIndexViewModel>()
                //.ForMember(dest => dest.IsParked, 
                //    act => act.Condition(src => (src.IsParked == true)))                
                .ForMember(
                    dest => dest.ParkedTime,
                    from => from.MapFrom(m => m.ArrivalTime.Subtract(DateTime.Now)));



            CreateMap<Vehicle, VehicleDetailsViewModel>();
           
            
            //ParkedTime = (DateTime.Now - v.Time),
        }


    }
}
