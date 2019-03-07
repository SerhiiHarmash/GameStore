using GameStore.Models.Entities;

namespace GameStore.Contracts.Interfaces.Logger
{
    public interface ILogger<T> where T : BaseEntity
    {
        void WriteCreateActionLog(T entity);

        void WriteUpdateActionLog(T oldEntity, T newEntity);

        void WriteDeleteActionLog(T entity);
    }
}
