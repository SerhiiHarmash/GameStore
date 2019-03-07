using GameStore.Contracts.Interfaces.DAL.Mongo;
using GameStore.Models.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using GameStore.DAL.Context;

namespace GameStore.DAL.Mongo.Filler
{
    public class GenreFiller : IFiller<Genre>
    {
        private readonly GameStoreMongoContext _context;

        public GenreFiller(GameStoreMongoContext context)
        {
            _context = context;
        }

        public ICollection<Genre> FillMany(ICollection<Genre> entity)
        {
            foreach (var x in entity)
            {
                FillSingle(x);
            }

            return entity;
        }

        public Genre FillSingle(Genre entity)
        {
            var gameCollection = _context.GetCollection<Game>();
            var gameFilter = Builders<Game>.Filter.Where(x => x.CategoryId == entity.CategoryId);
            entity.Games = gameCollection.Find(gameFilter).ToList();
            entity.IsDeleted = false;

            return entity;
        }
    }
}
