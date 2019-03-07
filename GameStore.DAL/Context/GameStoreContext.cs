using GameStore.Models.Entities;
using System.Data.Entity;
using System.Linq;

namespace GameStore.DAL.Context
{
    public class GameStoreContext : DbContext
    {
        public GameStoreContext() : base("GameStore")
        {
           Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<PlatformType> PlatformTypes { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Ban> Bans { get; set; }

        public DbSet<Shipper> Shippers { get; set; }

        public override int SaveChanges()
        {
            var modifiedEntities = ChangeTracker.Entries().Where(p => p.State == EntityState.Added).ToList();
            foreach (var item in modifiedEntities)
            {
                var f = item.Entity;
            }

            return base.SaveChanges();
        }
    }
}
