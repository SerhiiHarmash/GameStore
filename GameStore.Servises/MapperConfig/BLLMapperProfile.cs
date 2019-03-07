using AutoMapper;
using GameStore.Models.Entities;

namespace GameStore.Services.MapperConfig
{
    public class BLLMapperProfile : Profile
    {
        public BLLMapperProfile()
        {
            CreateMap<Game, Game>().ReverseMap()
                .ForMember(x => x.Comments, opt => opt.Ignore());
                //.ForMember(x => x.Genres, opt => opt.MapFrom(game => game.Genres))
                //.ForMember(x => x.PlatformTypes, opt => opt.MapFrom(game => game.PlatformTypes))
                //.ForMember(x => x.ReleaseDate, opt => opt.MapFrom(game => game.ReleaseDate));
        }
    }
}