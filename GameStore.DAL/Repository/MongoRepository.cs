using GameStore.Contracts.Interfaces.DAL;
using GameStore.Contracts.Interfaces.DAL.Mongo;
using GameStore.Models.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.DAL.Context;

namespace GameStore.DAL.Repository
{
    public class MongoRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly GameStoreMongoContext _mongoContext;
        private readonly IMongoCollection<T> _collection;
        private readonly IFiller<T> _filler;

        public MongoRepository(GameStoreMongoContext mongoContext, IFiller<T> filler)
        {
            _mongoContext = mongoContext;
            _collection = _mongoContext.GetCollection<T>();
            _filler = filler;
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> predicate = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> sortingFunc = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<T, object>>[] includes)
        {
            if (_collection == null)
            {
                return null;
            }

            var items = _collection.Find(new BsonDocument()).ToList();

            if (_filler != null && items != null)
            {
                items = _filler.FillMany(items).ToList();
            }
           
            if (predicate != null)
            {
                items = items.AsQueryable().Where(predicate).ToList();
            }

            return items;
        }

        public T GetSingle(Expression<Func<T, bool>> predicate = null,
            params Expression<Func<T, object>>[] includes)
        {
            var items = _collection.Find(new BsonDocument()).ToList();

            var item = predicate != null ? items.AsQueryable().FirstOrDefault(predicate) : items.FirstOrDefault();

            if (_filler != null && item != null)
            {
                item = _filler.FillSingle(item);
            }

            return item;
        }

        public bool Any(Expression<Func<T, bool>> predicate = null)
        {
            var items = _collection.Find(new BsonDocument()).ToList();

            if (predicate != null)
            {
                items = items.AsQueryable().Where(predicate).ToList();
            }

            var exist = items?.Count != 0;

            return exist;
        }

        public int Count(Expression<Func<T, bool>> predicate = null)
        {
            var items = _collection.Find(new BsonDocument()).ToList();
            items = items.Where(x => x.ValidUntil == null).ToList();

            if (_filler != null && items.Count != 0)
            {
                items = _filler.FillMany(items).ToList();
            }

            if (predicate != null)
            {
                items = items.AsQueryable().Where(predicate).ToList();
            }

            return items.Count;
        }

        public void Add(T entity)
        {
            _collection.InsertOne(entity);
        }

        public void Delete(T entity)
        {
            var filter = Builders<T>.Filter.Where(x => x.Id == entity.Id);
            _collection.DeleteOne(filter);
        }

        public void Update(T entity)
        {
            var filter = Builders<T>.Filter.Where(x => x.Id == entity.Id);

            _collection.ReplaceOne(filter, entity);
        }
    }
}
