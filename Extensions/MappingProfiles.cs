using AutoMapper;
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
            CreateMap<Gendermanagement, GenderResponseDto>();
            CreateMap<Language, LanguageResponseDto>();
        }
    }
}
