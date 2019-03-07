using GameStore.Models.Entities;
using System.Collections.Generic;

namespace GameStore.Contracts.Interfaces.DAL.Mongo
{
    public interface IFiller<T> where T : BaseEntity
    {
        T FillSingle(T entity);

        ICollection<T> FillMany(ICollection<T> entity);
    }
}
