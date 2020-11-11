using AutoMapper;
using EONET.BL.Models;
using EONET.DAL.Entities;

namespace EONET.BL.MappingProfiles
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<Event, EventModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<Geometry, GeometryModel>().ReverseMap();
            CreateMap<Source, SourceModel>().ReverseMap();
        }
    }
}