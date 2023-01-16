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

        }


    }
}
