using AutoMapper;
using WebApi.Models.Cities;
using WebApi.Models.Gendermanagements;

namespace WebApi.Extensions
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<City, CityResponseDto>();
            CreateMap<Gendermanagement, GenderResponseDto>();
        }
    }
}
