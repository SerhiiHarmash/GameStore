using AutoMapper;
using GameStore.Models.Entities;
using GameStore.Models.Filter;
using GameStore.WEB.Models.ViewModel;
using GameStore.WEB.Models.ViewModel.Game.Filtration;
using System.Linq;
using GameStore.WEB.Models.ViewModel.Order.Filtration;

namespace GameStore.WEB.Infrastructure
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Game, GameViewModel>()
                .ForMember(
                    model => model.Genres,
                    opt => opt.MapFrom(
                        game => game.Genres
                            .Select(genre => genre.Name)
                            .ToList()))
                .ForMember(
                    model => model.PlatformTypes,
                    opt => opt.MapFrom(
                        game => game.PlatformTypes
                            .Select(platformType => platformType.Type)
                            .ToList()))
                .ForMember(
                    model => model.Publisher,
                    opt => opt.MapFrom(
                        game => game.Publisher.CompanyName))
                .ForMember(
                    m => m.Comments,
                    opt => opt.MapFrom(
                        game => game.Comments
                            .Select(comment => comment.Body)
                            .ToList()));

        

            CreateMap<OrderFilterCriteriaViewModel, OrderFilterCriteria>();

            CreateMap<Order, OrderDetailsViewModel>();

            CreateMap<Comment, CommentViewModel>().ReverseMap();
               


            CreateMap<Publisher, PublisherViewModel>().ReverseMap();

            CreateMap<FilterCriteriaViewModel, GameFilterCriteria>().ReverseMap();

            CreateMap<Game, GameCardViewModel>();

            CreateMap<OrderDetails, OrderDetailsViewModel>()
                .ForMember(model => model.ProductKey,
                    opt => opt.MapFrom(orderGame => orderGame.Game.Key))
                .ForMember(model => model.ProductId,
                    opt => opt.MapFrom(orderGame => orderGame.GameId))
                .ForMember(model => model.ProductName,
                    opt => opt.MapFrom(orderGame => orderGame.Game.Name));

            CreateMap<Order, OrderViewModel>()
                .ForMember(model => model.OrdersDetails,
                    opt => opt.MapFrom(order => order.OrderDetails));

            CreateMap<Order, TerminalViewModel>();
            CreateMap<Order, TerminalViewModel>()
                .ForMember(model => model.OrderId,
                    opt => opt.MapFrom(order => order.Id));

            CreateMap<GameViewModel, Game>()
                .ForMember(model => model.Genres, opt => opt.Ignore())
                .ForMember(model => model.PlatformTypes, opt => opt.Ignore())
                .ForMember(model => model.Publisher, opt => opt.Ignore());
        }
    }
}