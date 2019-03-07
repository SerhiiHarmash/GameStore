using AutoMapper;
using GameStore.Contracts.Interfaces.DAL;
using GameStore.Contracts.Interfaces.DAL.Mongo;
using GameStore.Models.Entities;
using Ninject;
using System;
using System.Collections.Generic;

namespace GameStore.DAL.Mongo.Migrator
{
    public class GameMigrator : IMigrator<Game>
    {
        private readonly IRepository<Game> _sqlGameRepository;
        private readonly IRepository<Game> _mongoGameRepository;
        private readonly IMigrator<Genre> _genreMigrator;
        private readonly IMigrator<Publisher> _publisherMigrator;

        public GameMigrator([Named("SQL")] IRepository<Game> sqlGameRepository,
            [Named("Mongo")] IRepository<Game> mongoGameRepository,
            IMigrator<Publisher> publisherMigrator,
            IMigrator<Genre> genreMigrator)
        {
            _sqlGameRepository = sqlGameRepository;
            _mongoGameRepository = mongoGameRepository;
            _publisherMigrator = publisherMigrator;
            _genreMigrator = genreMigrator;
        }

        public Game Synchronize(Game item)
        {       
            if (!_mongoGameRepository.Any(x => x.Id == item.Id)
                && !_sqlGameRepository.Any(x => x.Id == item.Id))
            {
                return item;
            }

            if (_mongoGameRepository.Any(x => x.Id == item.Id) 
                && !_sqlGameRepository.Any(x => x.Id == item.Id))
            {                item.Genres = SynchronizeGenres(item.Genres);
                item.Publisher = _publisherMigrator.Synchronize(item.Publisher);
                item.ValidUntil = DateTime.UtcNow.AddDays(7);
                _sqlGameRepository.Add(item);
            }


            if ((_sqlGameRepository.Any(x => x.Id == item.Id && x.ValidUntil > DateTime.UtcNow))
               || (_sqlGameRepository.Any(x => x.Id == item.Id && x.ValidUntil == null)))
            {
                item = _sqlGameRepository.GetSingle(x => x.Id == item.Id);
                item.Genres = SynchronizeGenres(item.Genres);
                item.Publisher = _publisherMigrator.Synchronize(item.Publisher);
            }

            if (_sqlGameRepository.Any(x => x.Id == item.Id && DateTime.UtcNow > x.ValidUntil))
            {
                var mongoGame = _mongoGameRepository.GetSingle(x => x.Id == item.Id);
                mongoGame.ValidUntil = DateTime.UtcNow.AddDays(7);

                var sqlGame = _sqlGameRepository.GetSingle(x => x.Id == item.Id);

                var updatedGame = Mapper.Map(mongoGame, sqlGame);

                
                updatedGame.Genres = SynchronizeGenres(mongoGame.Genres);
                updatedGame.Publisher = _publisherMigrator.Synchronize(mongoGame.Publisher);
                updatedGame.PublisherId = updatedGame.Publisher.Id;

                //item = sqlGame;
                //item.Genres = SynchronizeGenres(mongoGame.Genres);
                //item.Publisher = _publisherMigrator.Synchronize(mongoGame.Publisher);
                //item.PublisherId = item.Publisher.Id;                   

                _sqlGameRepository.Update(updatedGame);

                return updatedGame;
            }

            return item;
        }

        private ICollection<Genre> SynchronizeGenres(IEnumerable<Genre> genres)
        {
            var oldGenres = new List<Genre>();
            oldGenres.AddRange(genres);

            var synchronizedGenres = new List<Genre>();

            foreach (var genre in oldGenres)
            {       
                synchronizedGenres.Add(_genreMigrator.Synchronize(genre));
            }

            return synchronizedGenres;        
        }           
    }
}
