using System;
using GameStore.Contracts.Interfaces.Logger;
using GameStore.DAL.Context;
using GameStore.Models.Entities;
using GameStore.Models.Enums;
using Newtonsoft.Json;

namespace GameStore.DAL.Logger
{
    public class Logger<T> : ILogger<T> where T : BaseEntity
    {
        private readonly GameStoreMongoContext _context;
      
        public Logger(GameStoreMongoContext context)
        {
            _context = context;       
        }

        public void WriteCreateActionLog(T entity)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var log = new Log
            {
                EntityAction = EntityAction.Create,
                EntityType = typeof(T),
                Date = DateTime.UtcNow,
                NewVersion = JsonConvert.SerializeObject(entity, settings)              
            };

            WriteLog(log);
        }

        public void WriteDeleteActionLog(T entity)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

           
                var log = new Log
                {
                    EntityAction = EntityAction.Create,
                    EntityType = typeof(T),
                    Date = DateTime.UtcNow,
                    NewVersion = JsonConvert.SerializeObject(entity, settings)
                };
            

            WriteLog(log);
        }

        public void WriteUpdateActionLog(T oldEntity, T newEntity)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };


            var log = new Log
            {
                EntityAction = EntityAction.Update,
                EntityType = typeof(T),
                Date = DateTime.UtcNow,
                LastVersion = JsonConvert.SerializeObject(oldEntity, settings),
                NewVersion = JsonConvert.SerializeObject(newEntity, settings)
            };

            WriteLog(log);
        }

        private void WriteLog(Log log)
        {
            var collection = _context.GetCollection<Log>();

            collection.InsertOne(log);
        }
    }
}
