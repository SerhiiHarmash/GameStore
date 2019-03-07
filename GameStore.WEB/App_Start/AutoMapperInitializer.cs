using AutoMapper;
using GameStore.DAL.MapperCofig;
using GameStore.Services.MapperConfig;
using GameStore.WEB.Infrastructure;

namespace GameStore.WEB.App_Start
{
    public class AutoMapperInitializer
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
                cfg.AddProfile<BLLMapperProfile>();
                cfg.AddProfile<DALMapperProfile>();
            });
        }
    }
}