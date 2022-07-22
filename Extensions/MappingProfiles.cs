using AutoMapper;
using WebApi.Models.Cities;

namespace WebApi.Extensions
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<City, CityResponseDto>();
        }
    }
}
