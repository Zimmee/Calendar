
using AutoMapper;
using Calendar.Dto;
using Calendar.Models;

namespace Calendar
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<Calendar.Models.Calendar, CalendarDto>().ReverseMap();
            CreateMap<Event, EventDto>().ReverseMap();
        }
    }
}
