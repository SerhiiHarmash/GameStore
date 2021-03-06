﻿using GameStore.Contracts.Interfaces.DAL;
using GameStore.DAL.Context;
using GameStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Repository
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly GameStoreContext _context;
        private readonly DbSet<T> _dbSet;


        public SQLRepository(GameStoreContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public int Count(Expression<Func<T, bool>> predicate = null)
        {
            return predicate != null ? _dbSet.Count(predicate) : _dbSet.Count();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IEnumerable<T> GetMany(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> sortingFunc = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet as IQueryable<T>;

            if (includes != null && includes.Length > 0)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            var selectedEntities = predicate != null ? query.Where(predicate) : query;

            return selectedEntities.ToList();

            //var selectedEntities = predicate != null ? _dbSet.Where(predicate) : _dbSet;

            //List<T> sortedEntities;

            //if (skip != null && take != null)
            //{
            //    sortedEntities = sortingFunc != null ? sortingFunc(selectedEntities).Skip((int)skip).Take((int)take).ToList() :
            //        selectedEntities.Skip((int)skip).Take((int)take).ToList();
            //    return sortedEntities;
            //}

            //sortedEntities = sortingFunc != null ? sortingFunc(selectedEntities).ToList() :
            //    selectedEntities.ToList();

            //return sortedEntities;
        }

        public T GetSingle(Expression<Func<T, bool>> predicate = null,
            params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet as IQueryable<T>;

            if (includes != null && includes.Length > 0)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            var item = predicate != null ? query.FirstOrDefault(predicate) : query.FirstOrDefault();

            return item;
        }

        public bool Any(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? _dbSet.Any() : _dbSet.Any(predicate);
        }

        public void Update(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
