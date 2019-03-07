using GameStore.Contracts.Interfaces.DAL.Mongo;
using GameStore.Models.Entities;

namespace GameStore.DAL.Mongo.Migrator
{
    public class StubMigrator<T> : IMigrator<T> where T : BaseEntity
    {
        public T Synchronize(T item)
        {
            return item;
        }
    }
}
