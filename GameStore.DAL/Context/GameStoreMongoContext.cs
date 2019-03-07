using System;
using System.Collections.Generic;
using GameStore.DAL.Mongo;
using GameStore.Models.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace GameStore.DAL.Context
{
    public class GameStoreMongoContext
    {
        private readonly IMongoDatabase _database;

        private readonly Dictionary<Type, string> _collectionName =
            new Dictionary<Type, string>()
            {
                {typeof(Shipper), "shippers"},
                {typeof(Game), "products"},
                {typeof(Order), "orders"},
                {typeof(OrderDetails), "order-details"},
                {typeof(Genre), "categories"},
                {typeof(Publisher), "suppliers"},
                {typeof(Log), "log"}
            };

        public GameStoreMongoContext()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);          
            _database = client.GetDatabase("Northwind");

            if (!BsonClassMap.IsClassMapRegistered(typeof(Game)))
            {
                MongoClassesMapperInitializator.Initialize();
            }         
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            if (!_collectionName.ContainsKey(typeof(T)))
            {
                return null;
            }

            var collection = _database.GetCollection<T>(_collectionName[typeof(T)]);
            return collection;
        }     
    }
}
