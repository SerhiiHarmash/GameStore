using AutoMapper;
using GameStore.Contracts.Interfaces.DAL;
using GameStore.Contracts.Interfaces.DAL.Mongo;
using GameStore.Models.Entities;
using Ninject;
using System;

namespace GameStore.DAL.Mongo.Migrator
{
    public class GenreMigrator : IMigrator<Genre>
    {
        private readonly IRepository<Genre> _sqlGenreRepository;
        private readonly IRepository<Genre> _mongoGenreRepository;

        public GenreMigrator([Named("SQL")] IRepository<Genre> genreRepository,
            [Named("Mongo")] IRepository<Genre> mongoGenreRepository)
        {
            _sqlGenreRepository = genreRepository;
            _mongoGenreRepository = mongoGenreRepository;
        }

        public Genre Synchronize(Genre item)
        {
            if (!_mongoGenreRepository.Any(x => x.Id == item.Id)
                && !_sqlGenreRepository.Any(x => x.Id == item.Id))
            {
                return item;
            }

            if (_mongoGenreRepository.Any(x => x.Id == item.Id)
                && !_sqlGenreRepository.Any(x => x.Id == item.Id))
            {
                item.ValidUntil = DateTime.UtcNow.AddDays(7);
                item.Games = null;
                _sqlGenreRepository.Add(item);
            }

            if ((_sqlGenreRepository.Any(x => x.Id == item.Id && x.ValidUntil > DateTime.UtcNow))
                || (_sqlGenreRepository.Any(x => x.Id == item.Id && x.ValidUntil == null)))
            {
                item = _sqlGenreRepository.GetSingle(x => x.Id == item.Id);
               // item.Games = null; // possibly will should add the line
            }

            if (_sqlGenreRepository.Any(x => x.Id == item.Id) && item.ValidUntil < DateTime.UtcNow)
            {
                var mongoGenre = _mongoGenreRepository.GetSingle(x => x.Id == item.Id);
                mongoGenre.ValidUntil = DateTime.UtcNow.AddDays(7);
                mongoGenre.Games = null;

                var sqlGenre = _sqlGenreRepository.GetSingle(x => x.Id == item.Id);

                Mapper.Map(mongoGenre, sqlGenre);

                _sqlGenreRepository.Update(sqlGenre);

                item = sqlGenre;
            }

            return item;
        }
    }
}
