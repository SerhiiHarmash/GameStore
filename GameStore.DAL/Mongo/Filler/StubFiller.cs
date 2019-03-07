using GameStore.Contracts.Interfaces.DAL.Mongo;
using GameStore.Models.Entities;
using System.Collections.Generic;

namespace GameStore.DAL.Mongo.Filler
{
    public class StubFiller<T> : IFiller<T> where T : BaseEntity
    {
        public T FillSingle(T entity)
        {
            return entity;
        }

        public ICollection<T> FillMany(ICollection<T> entity)
        {
            return entity;
        }
    }
}
