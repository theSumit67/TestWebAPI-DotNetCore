using AutoMapper;
using TestWebAPI.Models;
using TestWebAPI.Models.DTOs;

namespace TestWebAPI.Mappings
{
    class TestWebMappings : Profile
    {
        public TestWebMappings()
        {
            CreateMap<NationalPark, NationalParkDTO>().ReverseMap();
        }
    }
}