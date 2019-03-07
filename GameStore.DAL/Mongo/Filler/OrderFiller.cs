using GameStore.Contracts.Interfaces.DAL.Mongo;
using GameStore.Models.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using GameStore.DAL.Context;

namespace GameStore.DAL.Mongo.Filler
{
    public class OrderFiller : IFiller<Order>
    {
        private readonly GameStoreMongoContext _context;

        public OrderFiller(GameStoreMongoContext context)
        {
            _context = context;
        }

        public ICollection<Order> FillMany(ICollection<Order> entity)
        {
            foreach (var x in entity)
            {
                FillSingle(x);
            }

            return entity;
        }

        public Order FillSingle(Order entity)
        {
            var orderDetailsCollection = _context.GetCollection<OrderDetails>();
            var orderDetailsFilter = Builders<OrderDetails>.Filter.Where(x => x.MongoOrderId == entity.OrderId);

            entity.OrderDetails = orderDetailsCollection.Find(orderDetailsFilter).ToList();

            var productsCollection = _context.GetCollection<Game>();

            foreach (var orderDetails in entity.OrderDetails)
            {
                var filter = Builders<Game>.Filter.Where(x => x.ProductId == orderDetails.ProductId);
                var result = productsCollection.Find(filter).ToList();
                orderDetails.Game = result[0];
            }

            return entity;
        }
    }
}
