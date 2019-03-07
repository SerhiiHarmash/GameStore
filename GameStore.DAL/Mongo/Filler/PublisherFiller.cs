using GameStore.Contracts.Interfaces.DAL.Mongo;
using GameStore.Models.Entities;
using System.Collections.Generic;
using GameStore.DAL.Context;
using MongoDB.Driver;

namespace GameStore.DAL.Mongo.Filler
{
    public class PublisherFiller : IFiller<Publisher>
    {
        private readonly GameStoreMongoContext _context;

        public PublisherFiller(GameStoreMongoContext context)
        {
            _context = context;
        }

        public ICollection<Publisher> FillMany(ICollection<Publisher> entity)
        {
            foreach (var x in entity)
            {
                FillSingle(x);
            }

            return entity;
        }

        public Publisher FillSingle(Publisher entity)
        {
            var gameCollection = _context.GetCollection<Game>();
            var gameFilter = Builders<Game>.Filter.Where(x => x.SupplierId == entity.SupplierId);
            entity.Games = gameCollection.Find(gameFilter).ToList();
            entity.IsDeleted = false;

            return entity;
        }
    }
}
