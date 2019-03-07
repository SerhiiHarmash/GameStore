using GameStore.Contracts.Interfaces.DAL;
using GameStore.Contracts.Interfaces.DAL.Mongo;
using GameStore.Models.Entities;
using Ninject;
using System;
using AutoMapper;

namespace GameStore.DAL.Mongo.Migrator
{
    public class PublisherMigrator : IMigrator<Publisher>
    {
        private readonly IRepository<Publisher> _sqlPublisherRepository;
        private readonly IRepository<Publisher> _mongoPublisherRepository;

        public PublisherMigrator([Named("SQL")] IRepository<Publisher> sqlPublisherRepository,
            [Named("Mongo")] IRepository<Publisher> mongoPublisherRepository)
        {
            _sqlPublisherRepository = sqlPublisherRepository;
            _mongoPublisherRepository = mongoPublisherRepository;
        }

        public Publisher Synchronize(Publisher item)
        {      
            if (!_mongoPublisherRepository.Any(x => x.Id == item.Id)
                && !_sqlPublisherRepository.Any(x => x.Id == item.Id))
            {
                return item;
            }

            if (_mongoPublisherRepository.Any(x => x.Id == item.Id)
                && !_sqlPublisherRepository.Any(x => x.Id == item.Id))
            {
                item.ValidUntil = DateTime.UtcNow.AddDays(7);
                item.Games = null;
                _sqlPublisherRepository.Add(item);
            }

            if ((_sqlPublisherRepository.Any(x => x.Id == item.Id && x.ValidUntil > DateTime.UtcNow))
                || (_sqlPublisherRepository.Any(x => x.Id == item.Id && x.ValidUntil == null)))
            {
                item = _sqlPublisherRepository.GetSingle(x => x.Id == item.Id);
                //item.Games = null; //remove
            }
            
            if (_sqlPublisherRepository.Any(x => x.Id == item.Id) && item.ValidUntil < DateTime.UtcNow)
            {
                var mongoPublisher = _mongoPublisherRepository.GetSingle(x => x.Id == item.Id);
                mongoPublisher.ValidUntil = DateTime.UtcNow.AddDays(7);
                mongoPublisher.Games = null;

                var sqlPublisher = _sqlPublisherRepository.GetSingle(x => x.Id == item.Id);

                Mapper.Map(mongoPublisher, sqlPublisher);

                _sqlPublisherRepository.Update(mongoPublisher);

                item = mongoPublisher;
            }

            return item;
        }
    }
}
