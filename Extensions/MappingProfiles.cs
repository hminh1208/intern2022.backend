using AutoMapper;
using WebApi.Models.Category;
using WebApi.Models.CategoryCities;
using WebApi.Models.Cities;
using WebApi.Models.Gendermanagements;
using WebApi.Models.Languages;

namespace WebApi.Extensions
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<City, CityResponseDto>();
            CreateMap<Category, CategoryResponseDto>();
            CreateMap<CategoryCity, CategoryCityResponseDto>();
            CreateMap<Gendermanagement, GenderResponseDto>();
            CreateMap<Language, LanguageResponseDto>();
        }
    }
}
