using GameStore.Contracts.Interfaces.DAL;
using GameStore.Contracts.Interfaces.DAL.Mongo;
using GameStore.Contracts.Interfaces.Logger;
using GameStore.Contracts.Interfaces.Services;
using GameStore.DAL;
using GameStore.DAL.Context;
using GameStore.DAL.Logger;
using GameStore.DAL.Mongo.Filler;
using GameStore.DAL.Mongo.Migrator;
using GameStore.DAL.Repository;
using GameStore.Models.Entities;
using GameStore.Services.Services;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using Ninject.Web.Common;

namespace GameStore.CR
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<GameStoreContext>().ToSelf().InRequestScope();
            Bind<GameStoreMongoContext>().ToSelf().InRequestScope();
            Bind<IRepositoryFactory>().ToFactory();

            Bind(typeof(ILogger<>)).To(typeof(Logger<>));

            Bind(typeof(IRepository<>)).To(typeof(SQLRepository<>)).WhenInjectedInto(typeof(IRepository<>)).Named("SQL");
            Bind(typeof(IRepository<>)).To(typeof(MongoRepository<>)).WhenInjectedInto(typeof(IRepository<>)).Named("Mongo");
            Bind(typeof(IRepository<>)).To(typeof(AdapterRepository<>));

            Bind<IFiller<Game>>().To<GameFiller>();
            Bind<IFiller<Genre>>().To<GenreFiller>();
            Bind<IFiller<Publisher>>().To<PublisherFiller>();
            Bind<IFiller<Order>>().To<OrderFiller>();
            Bind<IFiller<PlatformType>>().To<StubFiller<PlatformType>>();
            Bind<IFiller<Comment>>().To<StubFiller<Comment>>();
            Bind<IFiller<Ban>>().To<StubFiller<Ban>>();
            Bind<IFiller<OrderDetails>>().To<StubFiller<OrderDetails>>();
            Bind<IFiller<Shipper>>().To<StubFiller<Shipper>>();


            Bind<IMigrator<Game>>().To<GameMigrator>();
            Bind<IMigrator<Genre>>().To<GenreMigrator>();
            Bind<IMigrator<Publisher>>().To<PublisherMigrator>();
            Bind<IMigrator<Order>>().To<StubMigrator<Order>>();
            Bind<IMigrator<OrderDetails>>().To<StubMigrator<OrderDetails>>();
            Bind<IMigrator<PlatformType>>().To<StubMigrator<PlatformType>>();
            Bind<IMigrator<Comment>>().To<StubMigrator<Comment>>();
            Bind<IMigrator<Ban>>().To<StubMigrator<Ban>>();
            Bind<IMigrator<Shipper>>().To<StubMigrator<Shipper>>();

            Bind(typeof(IRepository<>)).To(typeof(SQLRepository<>)).WhenInjectedInto(typeof(IMigrator<>)).Named("SQL");
            Bind(typeof(IRepository<>)).To(typeof(MongoRepository<>)).WhenInjectedInto(typeof(IMigrator<>)).Named("Mongo");

            Bind<IMigrator<Publisher>>().To<PublisherMigrator>().WhenInjectedInto<GameMigrator>();
            Bind<IMigrator<Genre>>().To<GenreMigrator>().WhenInjectedInto<GameMigrator>();

            Bind<IGameService>().To<GameService>();
            Bind<ICommentService>().To<CommentService>();
            Bind<IGenreService>().To<GenreService>();
            Bind<IPlatformTypeService>().To<PlatformTypeService>();
            Bind<IPublisherService>().To<PublisherService>();
            Bind<IOrderService>().To<OrderService>();
            Bind<IBanService>().To<BanService>();
            Bind<IShipperService>().To<ShipperService>();
        }
    }
}
