using System;
using GameStore.Contracts.Interfaces.DAL.Mongo;
using GameStore.Models.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using GameStore.DAL.Context;

namespace GameStore.DAL.Mongo.Filler
{
    public class GameFiller : IFiller<Game>
    {
        private readonly GameStoreMongoContext _context;

        public GameFiller(GameStoreMongoContext context)
        {
            _context = context;
        }

        public ICollection<Game> FillMany(ICollection<Game> entity)
        {
            foreach (var x in entity)
            {
                FillSingle(x);
            }

            return entity;
        }

        public Game FillSingle(Game entity)
        {
            var publishersCollection = _context.GetCollection<Publisher>();
            var publisherFilter = Builders<Publisher>.Filter.Where(x => x.SupplierId == entity.SupplierId);
            entity.Publisher = publishersCollection.Find(publisherFilter).ToList()[0];

            var genreCollection = _context.GetCollection<Genre>();
            var genreFilter = Builders<Genre>.Filter.Where(x => x.CategoryId == entity.CategoryId);
            entity.Genres = genreCollection.Find(genreFilter).ToList();

            entity.Key = entity.Id;
            entity.ReleaseDate = DateTime.UtcNow;
        
            entity.PlatformTypes = new List<PlatformType>();
            entity.Comments = new List<Comment>();

            return entity;
        }
    }
}
