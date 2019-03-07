using GameStore.Contracts.Interfaces.DAL;
using GameStore.Contracts.Interfaces.DAL.Mongo;
using GameStore.Models.Entities;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Repository
{
    public class AdapterRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IRepository<T> _mongoRepository;
        private readonly IRepository<T> _sqlRepository;
        private readonly IMigrator<T> _migrator;

        public AdapterRepository([Named("Mongo")] IRepository<T> mongoRepository,
            [Named("SQL")] IRepository<T> sqlRepository,
            IMigrator<T> migrator)
        {
            _mongoRepository = mongoRepository;
            _sqlRepository = sqlRepository;
            _migrator = migrator;
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> sortingFunc = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<T, object>>[] includes)
        {
            var items = _sqlRepository.GetMany(predicate, null, null, null, includes).ToList();

            var ids = items.Select(s => s.Id).ToList();
            
            var mongoItems = _mongoRepository.GetMany(predicate);

            if (mongoItems != null)
            {
                mongoItems = mongoItems.Where(m => !ids.Contains(m.Id)).ToList();
                items.AddRange(mongoItems);
            }

            List<T> sortedEntities;

            if (skip != null && take != null)
            {
                sortedEntities = sortingFunc != null ? sortingFunc(items.AsQueryable()).Skip((int)skip).Take((int)take).ToList() :
                    items.AsQueryable().Skip((int)skip).Take((int)take).ToList();
                return sortedEntities;
            }

            sortedEntities = sortingFunc != null ? sortingFunc(items.AsQueryable()).ToList() : items.ToList();

            return sortedEntities;
        }

        public T GetSingle(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes)
        {
            var item = _sqlRepository.GetSingle(predicate, includes);

            if ((item != null && item.ValidUntil > DateTime.UtcNow)
                || (item != null && item.ValidUntil == null))
            {
                return item;
            }

            item = _mongoRepository.GetSingle(predicate, null);

            if (item != null)
            {
                item = _migrator.Synchronize(item);
                return item;
            }

            return null;
        }

        public bool Any(Expression<Func<T, bool>> predicate = null)
        {
            var exist = _sqlRepository.Any(predicate);

            if (!exist)
            {
                exist = _mongoRepository.Any(predicate);
            }

            return exist;
        }

        public int Count(Expression<Func<T, bool>> predicate = null)
        {
            var items = _sqlRepository.GetMany(predicate).ToList();

            var ids = items.Select(s => s.Id).ToList();

            var mongoItems = _mongoRepository.GetMany(predicate);

            if (mongoItems != null)
            {
                mongoItems = mongoItems.Where(m => !ids.Contains(m.Id)).ToList();
                items.AddRange(mongoItems);
            }

            return items.Count();
        }

        public void Add(T entity)
        {
            _sqlRepository.Add(entity);
        }

        public void Delete(T entity)
        {
            if (_sqlRepository.Any(x => x.Id == entity.Id))
            {
                _sqlRepository.Delete(entity);
            }
        }

        public void Update(T entity)
        {
            _sqlRepository.Update(entity);
        }
    }
}
