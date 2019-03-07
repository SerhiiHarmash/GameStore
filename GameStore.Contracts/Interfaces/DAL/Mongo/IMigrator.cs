using GameStore.Models.Entities;

namespace GameStore.Contracts.Interfaces.DAL.Mongo
{
    public interface IMigrator<T> where T : BaseEntity
    {
         T Synchronize(T item);
    }
}
