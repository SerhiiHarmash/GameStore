using AutoMapper;
using GameStore.Models.Entities;

namespace GameStore.DAL.MapperCofig
{
    public class DALMapperProfile : Profile
    {
        public DALMapperProfile()
        {
            CreateMap<Genre, Genre>().ReverseMap();
            CreateMap<Publisher, Publisher>().ReverseMap();
        }      
    }   
}
