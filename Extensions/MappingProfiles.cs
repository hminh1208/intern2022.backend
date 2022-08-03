using AutoMapper;
using WebApi.Models.Category;
using WebApi.Models.Cities;
using WebApi.Models.Gendermanagements;

namespace WebApi.Extensions
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<City, CityResponseDto>();
            CreateMap<Category, CategoryResponseDto>();
            CreateMap<Gendermanagement, GenderResponseDto>();
        }
    }
}
